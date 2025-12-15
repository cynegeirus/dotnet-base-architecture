using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using Entities.Dtos;

namespace Business.Concrete;

public class AccountManager(IUserService userService, IRoleService roleService, IUserRoleService userRoleService, ITokenHelper tokenHelper) : IAccountService
{
    public IDataResult<User?> Register(RegisterDto? userForRegisterDto, string? password)
    {
        HashingHelper.CreatePasswordHash(password, out var passwordHash, out var passwordSalt);
        User? user = new()
        {
            FirstName = userForRegisterDto?.FirstName,
            LastName = userForRegisterDto?.LastName,
            MailAddress = userForRegisterDto?.MailAddress,
            Username = userForRegisterDto?.Username,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        };

        userService.Add(user);

        var defaultRole = roleService.GetList().Data.FirstOrDefault(x => x.Name == "User");
        var currentUser = userService.GetUserByUsername(userForRegisterDto!.Username);

        userRoleService.Add(new UserRole
        {
            RoleId = defaultRole!.Id,
            UserId = currentUser.Data!.Id
        });

        return new SuccessDataResult<User?>(user, CustomMessage.UserRegistered);
    }

    public IDataResult<User?> Login(LoginDto? userForLoginDto)
    {
        var userToCheck = userService.GetUserByUsername(userForLoginDto?.Username);
        return !HashingHelper.VerifyPasswordHash(userForLoginDto?.Password, userToCheck.Data?.PasswordHash, userToCheck.Data?.PasswordSalt)
            ? new ErrorDataResult<User?>(CustomMessage.PasswordError)
            : new SuccessDataResult<User?>(userToCheck.Data, CustomMessage.SuccessfulLogin);
    }

    public IResult UserExists(string? username)
    {
        return userService.GetUserByUsername(username).Data != null
            ? new ErrorResult(CustomMessage.UserNameAlreadyExists)
            : new SuccessResult();
    }

    public IDataResult<AccessToken> CreateAccessToken(User? user)
    {
        var claims = userService.GetRoles(user);
        var accessToken = tokenHelper.CreateToken(user, claims.Data);

        return new SuccessDataResult<AccessToken>(accessToken, CustomMessage.AccessTokenCreated);
    }
}