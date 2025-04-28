using Business.Abstract.Identity;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete.Identity;
using Core.Entities.Concrete.Management;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract.Identity;
using Entities.Dtos.Requests;

namespace Business.Concrete.Identity;

public class AccountManager(IUserService userService, IRoleDal roleDal, IUserRoleDal userRoleDal, ITokenHelper tokenHelper) : IAccountService
{
    [ValidationAspect(typeof(RegisterValidator), Priority = 1)]
    public IDataResult<User?> Register(RegisterRequestDto? userForRegisterDto, string? password)
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

        var defaultRole = roleDal.Get(x => x!.Name == "User");
        var currentUser = userService.GetUserByUsername(userForRegisterDto!.Username);

        userRoleDal.Add(new UserRole
        {
            RoleId = defaultRole!.Id,
            UserId = currentUser.Data!.Id
        });

        return new SuccessDataResult<User?>(user, CustomMessage.UserRegistered);
    }

    [ValidationAspect(typeof(LoginValidator), Priority = 1)]
    public IDataResult<User?> Login(LoginRequestDto? userForLoginDto)
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