using Business.Abstract.Identity;
using Core.Entities.Concrete.Identity;
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
        return Ok(userRoleService.Get(id));
    }

    [HttpGet("GetAll")]
    public ActionResult GetAll()
    {
        return Ok(userRoleService.GetList());
    }

    [HttpPost("Add")]
    public ActionResult Add(UserRole entity)
    {
        var result = userRoleService.Add(entity);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPost("Update")]
    public ActionResult Update(UserRole entity)
    {
        var result = userRoleService.Update(entity);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPost("Delete")]
    public ActionResult Delete(UserRole entity)
    {
        var result = userRoleService.Delete(entity);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}