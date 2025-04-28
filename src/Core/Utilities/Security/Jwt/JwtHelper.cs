﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Core.Entities.Concrete.Identity;
using Core.Extensions;
using Core.Utilities.Security.Encryption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Core.Utilities.Security.Jwt;

public class JwtHelper : ITokenHelper
{
    private readonly TokenOptions? _tokenOptions;
    private DateTime _accessTokenExpiration;
    public IConfiguration Configuration { get; }

    public JwtHelper(IConfiguration configuration)
    {
        Configuration = configuration;
        _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
    }

    public AccessToken CreateToken(User? user, List<Role> roles)
    {
        _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions!.AccessTokenExpiration);
        var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
        var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
        var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, roles);
        JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
        var token = jwtSecurityTokenHandler.WriteToken(jwt);

        return new AccessToken
        {
            Token = token,
            Expiration = _accessTokenExpiration
        };
    }

    public JwtSecurityToken CreateJwtSecurityToken(TokenOptions? tokenOptions, User? user, SigningCredentials signingCredentials, List<Role> roles)
    {
        JwtSecurityToken jwt = new(
            tokenOptions?.Issuer,
            tokenOptions?.Audience,
            expires: _accessTokenExpiration,
            notBefore: DateTime.Now,
            claims: SetClaims(user, roles),
            signingCredentials: signingCredentials
        );
        return jwt;
    }

    private IEnumerable<Claim> SetClaims(User? user, List<Role> roles)
    {
        List<Claim> claims = new();
        claims.AddNameIdentifier(user?.Id.ToString());
        claims.AddEmail(user?.MailAddress);
        claims.AddUsername(user?.Username);
        claims.AddName($"{user?.FirstName} {user?.LastName}");
        claims.AddRoles(roles.Select(c => c.Name).ToArray());

        return claims;
    }
}