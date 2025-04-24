using Core.Entities.Concrete.Management;

namespace Core.Utilities.Security.Jwt;

public interface ITokenHelper
{
    AccessToken CreateToken(User? user, List<Role> operationClaims);
}