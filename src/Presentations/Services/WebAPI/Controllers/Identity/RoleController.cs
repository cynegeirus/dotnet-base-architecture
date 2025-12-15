using Business.Abstract;
using Core.Entities.Concrete;
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
        var result = roleService.Get(id);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpGet("GetAll")]
    public ActionResult GetAll()
    {
        var result = roleService.GetList();
        return Ok(result);
    }

    [HttpPost("Add")]
    public ActionResult Add(Role entity)
    {
        var result = roleService.Add(entity);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPut("Update")]
    public ActionResult Update(Role entity)
    {
        var result = roleService.Update(entity);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpDelete("Delete")]
    public ActionResult Delete(Role entity)
    {
        var result = roleService.Delete(entity);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}