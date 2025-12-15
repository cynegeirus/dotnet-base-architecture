using Business.Abstract;
using Core.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Identity;

[ApiController]
[Authorize(Roles = "Administrator")]
[Route("api/Identity/[controller]")]
public class UserRoleController(IUserRoleService userRoleService) : ControllerBase
{
    [HttpGet("Get")]
    public ActionResult Get(Guid id)
    {
        var result = userRoleService.Get(id);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpGet("GetAll")]
    public ActionResult GetAll()
    {
        var result = userRoleService.GetList();
        return Ok(result);
    }

    [HttpPost("Add")]
    public ActionResult Add(UserRole entity)
    {
        var result = userRoleService.Add(entity);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPut("Update")]
    public ActionResult Update(UserRole entity)
    {
        var result = userRoleService.Update(entity);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpDelete("Delete")]
    public ActionResult Delete(UserRole entity)
    {
        var result = userRoleService.Delete(entity);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}