using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Authentication;

[ApiController]
[Route("api/Authentication/[controller]")]
public class AccountController(IAccountService accountService) : ControllerBase
{
    [HttpPost("Login")]
    public ActionResult Login(LoginDto? loginDto)
    {
        var userToLogin = accountService.Login(loginDto);

        if (!userToLogin.Success)
            return BadRequest(userToLogin);

        var result = accountService.CreateAccessToken(userToLogin.Data);

        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPost("Register")]
    public ActionResult Register(RegisterDto? registerDto)
    {
        var userExists = accountService.UserExists(registerDto?.Username);

        if (!userExists.Success)
            return BadRequest(userExists);

        var registerResult = accountService.Register(registerDto, registerDto?.Password);
        var result = accountService.CreateAccessToken(registerResult.Data);

        return result.Success ? Ok(result) : BadRequest(result);
    }
}