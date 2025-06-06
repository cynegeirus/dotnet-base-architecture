using Business.Abstract.Identity;
using Core.Entities.Concrete.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Identity;

[ApiController]
[Authorize(Roles = "Administrator")]
[Route("api/Identity/[controller]")]
public class RoleController(IRoleService roleService) : ControllerBase
{
    [HttpGet("Get")]
    public ActionResult Get(Guid id)
    {
        return Ok(roleService.Get(id));
    }

    [HttpGet("GetAll")]
    public ActionResult GetAll()
    {
        return Ok(roleService.GetList());
    }

    [HttpPost("Add")]
    public ActionResult Add(Role entity)
    {
        var result = roleService.Add(entity);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPost("Update")]
    public ActionResult Update(Role entity)
    {
        var result = roleService.Update(entity);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPost("Delete")]
    public ActionResult Delete(Role entity)
    {
        var result = roleService.Delete(entity);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}