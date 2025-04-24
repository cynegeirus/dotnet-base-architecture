using System.IO.Compression;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.DependencyResolvers.Autofac;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMemoryCache();
builder.Services.AddResponseCaching();
builder.Services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.SmallestSize);
builder.Services.Configure<BrotliCompressionProviderOptions>(options => options.Level = CompressionLevel.SmallestSize);
builder.Services.AddCors(c => c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin()));
builder.Services.AddDependencyResolvers([new CoreModule()]);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()).ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new AutofacBusinessModule()));

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v2025.1",
        Title = "Base Architecture",
        Description = "Base Architecture is a modular and scalable architecture built using .NET technologies. It is designed to serve as a starting point for enterprise-level applications, providing a clean and layered project structure that follows modern software development best practices."
    });
});

builder.Services.AddResponseCompression(options =>
{
    options.Providers.Add<GzipCompressionProvider>();
    options.Providers.Add<BrotliCompressionProvider>();
    options.EnableForHttps = true;
    options.MimeTypes =
    [
        "text/html", "text/css", "text/xml", "image/gif", "image/jpeg", "application/x-javascript",
        "application/atom+xml", "application/rss+xml", "application/json", "text/json", "text/mathml", "text/plain",
        "text/vnd.sun.j2me.app-descriptor", "text/vnd.wap.wml", "text/x-component", "image/png", "image/tiff",
        "image/vnd.wap.wbmp", "image/x-icon", "image/x-jng", "image/x-ms-bmp", "image/svg+xml", "image/webp",
        "application/java-archive", "application/mac-binhex40", "application/msword", "application/pdf",
        "application/postscript", "application/rtf", "application/vnd.ms-excel", "application/vnd.ms-powerpoint",
        "application/vnd.wap.wmlc", "application/vnd.google-earth.kml+xml", "application/vnd.google-earth.kmz",
        "application/x-7z-compressed", "application/x-cocoa", "application/x-java-archive-diff",
        "application/x-java-jnlp-file", "application/x-makeself", "application/x-perl", "application/x-pilot",
        "application/x-rar-compressed", "application/x-redhat-package-manager", "application/x-sea",
        "application/x-shockwave-flash", "application/x-stuffit", "application/x-tcl", "application/x-x509-ca-cert",
        "application/x-xpinstall", "application/xhtml+xml", "application/zip", "application/octet-stream", "audio/midi",
        "audio/mpeg", "audio/ogg", "audio/x-realaudio", "audio/x-m4a", "video/3gpp", "video/mpeg", "video/quicktime",
        "video/x-flv", "video/x-mng", "video/x-ms-asf", "video/x-ms-wmv", "video/x-msvideo", "video/mp4", "video/webm",
        "video/x-m4v"
    ];
});

var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ValidIssuer = tokenOptions?.Issuer,
    ValidAudience = tokenOptions?.Audience,
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions?.SecurityKey)
});


var app = builder.Build();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseExceptionMiddleware();
app.UseTransactionMiddleware();
app.UseResponseCompression();
app.UseResponseCaching();
app.UseCors(corsPolicyBuilder => corsPolicyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseSwagger();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Base Architecture v2025.1");
    options.RoutePrefix = string.Empty;
});

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx => ctx.Context.Response.Headers.Append("Cache-Control", "public, max-age=86400")
});

app.Run();