using Business.Abstract.Identity;
using Core.Entities.Concrete.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Identity;

[ApiController]
[Authorize(Roles = "Administrator")]
[Route("api/Identity/[controller]")]
public class MenuRoleController(IMenuRoleService menuRoleService) : ControllerBase
{
    [HttpGet("Get")]
    public ActionResult Get(Guid id)
    {
        return Ok(menuRoleService.Get(id));
    }

    [HttpGet("GetAll")]
    public ActionResult GetAll()
    {
        return Ok(menuRoleService.GetList());
    }

    [HttpPost("Add")]
    public ActionResult Add(MenuRole entity)
    {
        var result = menuRoleService.Add(entity);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPost("Update")]
    public ActionResult Update(MenuRole entity)
    {
        var result = menuRoleService.Update(entity);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPost("Delete")]
    public ActionResult Delete(MenuRole entity)
    {
        var result = menuRoleService.Delete(entity);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}