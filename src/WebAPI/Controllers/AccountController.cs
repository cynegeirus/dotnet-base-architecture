using Business.Abstract;
using Entities.Dtos.Requests;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController(IAccountService accountService) : ControllerBase
{
    [HttpPost("Login")]
    public ActionResult Login(LoginRequestDto? loginDto)
    {
        var userToLogin = accountService.Login(loginDto);

        if (!userToLogin.Success)
            return BadRequest(userToLogin);

        var result = accountService.CreateAccessToken(userToLogin.Data);

        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPost("Register")]
    public ActionResult Register(RegisterRequestDto? registerDto)
    {
        var userExists = accountService.UserExists(registerDto?.Username);

        if (!userExists.Success)
            return BadRequest(userExists);

        var registerResult = accountService.Register(registerDto, registerDto?.Password);
        var result = accountService.CreateAccessToken(registerResult.Data);

        return result.Success ? Ok(result) : BadRequest(result);
    }
}