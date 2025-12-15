using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Core.Extensions;

public static class ClaimExtensions
{
    extension(ICollection<Claim> claims)
    {
        public void AddEmail(string? email)
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, email!));
        }

        public void AddName(string name)
        {
            claims.Add(new Claim(ClaimTypes.Name, name));
        }

        public void AddUsername(string? username)
        {
            claims.Add(new Claim("Username", username!));
        }

        public void AddNameIdentifier(string? nameIdentifier)
        {
            claims.Add(new Claim(ClaimTypes.NameIdentifier, nameIdentifier!));
        }

        public void AddRoles(string?[] roles)
        {
            roles.ToList().ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role!)));
        }
    }
}