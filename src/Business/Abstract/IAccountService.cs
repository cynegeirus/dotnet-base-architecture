using Core.Entities.Concrete.Management;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using Entities.Dtos.Requests;

namespace Business.Abstract;

public interface IAccountService
{
    IDataResult<User?> Login(LoginRequestDto? userForLoginDto);
    IDataResult<User?> Register(RegisterRequestDto? userForRegisterDto, string? password);
    IDataResult<AccessToken> CreateAccessToken(User? user);
    IResult UserExists(string? username);
}