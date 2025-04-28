using Business.Abstract.Identity;
using Core.Entities.Concrete.Identity;
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
        return Ok(userService.GetUserById(id));
    }

    [HttpGet("GetAll")]
    public ActionResult GetAll()
    {
        return Ok(userService.GetUsers());
    }

    [HttpPost("Add")]
    public ActionResult Add(User entity)
    {
        var result = userService.Add(entity);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPost("Update")]
    public ActionResult Update(User entity)
    {
        var result = userService.Update(entity);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPost("Delete")]
    public ActionResult Delete(User entity)
    {
        var result = userService.Delete(entity);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}