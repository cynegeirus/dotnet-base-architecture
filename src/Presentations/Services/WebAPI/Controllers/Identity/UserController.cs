using Business.Abstract;
using Core.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Identity;

[ApiController]
[Authorize(Roles = "Administrator")]
[Route("api/Identity/[controller]")]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpGet("Get")]
    public ActionResult Get(Guid id)
    {
        var result = userService.GetUserById(id);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpGet("GetAll")]
    public ActionResult GetAll()
    {
        var result = userService.GetUsers();
        return Ok(result);
    }

    [HttpPost("Add")]
    public ActionResult Add(User entity)
    {
        var result = userService.Add(entity);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPut("Update")]
    public ActionResult Update(User entity)
    {
        var result = userService.Update(entity);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpDelete("Delete")]
    public ActionResult Delete(User entity)
    {
        var result = userService.Delete(entity);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}