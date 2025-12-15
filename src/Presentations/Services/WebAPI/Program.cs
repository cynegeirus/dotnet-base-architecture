using System.IO.Compression;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.DependencyResolvers.Autofac;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.HealthChecks;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();

builder.Services.AddControllers();
builder.Services.AddMemoryCache();
builder.Services.AddResponseCaching();
builder.Services.AddCustomHealthChecks();
builder.Services.AddProblemDetails();
builder.Services.Configure<BrotliCompressionProviderOptions>(o => o.Level = CompressionLevel.Fastest);
builder.Services.Configure<GzipCompressionProviderOptions>(o => o.Level = CompressionLevel.Fastest);
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Clear();
    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();
    options.MimeTypes = ["application/json", "text/plain", "text/json", "application/problem+json"];
});

builder.Services.AddCors(options => options.AddPolicy("Unlimited", policy => policy.SetIsOriginAllowed(_ => true).AllowAnyMethod().AllowAnyHeader()));
builder.Services.AddDependencyResolvers([new CoreModule()]);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()).ConfigureContainer<ContainerBuilder>(cb => cb.RegisterModule(new AutofacBusinessModule()));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidateActor = false,
        ValidateTokenReplay = false,
        ValidIssuer = tokenOptions?.Issuer,
        ValidAudience = tokenOptions?.Audience,
        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions?.SecurityKey),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    options.ForwardLimit = null;
    options.RequireHeaderSymmetry = false;
});

builder.WebHost.ConfigureKestrel(options =>
{
    options.AddServerHeader = false;
    options.Limits.MaxRequestBodySize = null;
    options.Limits.MaxRequestBufferSize = null;
    options.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(10);
    options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(10);
    options.Limits.Http2.InitialConnectionWindowSize = 1024 * 1024 * 32;
    options.Limits.Http2.InitialStreamWindowSize = 1024 * 1024 * 32;
});

var app = builder.Build();

app.UseForwardedHeaders();
app.UseRouting();
app.UseCors("Unlimited");
app.UseResponseCompression();
app.UseResponseCaching();
app.UseCustomHealthChecks();
app.UseAuthentication();
app.UseAuthorization();
app.UseTrafficAuditMiddleware();
app.UseGlobalExceptionMiddleware();
app.MapControllers();
app.Run();