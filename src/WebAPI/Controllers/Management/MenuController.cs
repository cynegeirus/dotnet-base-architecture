using Business.Abstract;
using Core.Entities.Concrete.Management;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Management;

[ApiController]
[Route("api/Management/[controller]")]
public class MenuController(IMenuService menuService) : ControllerBase
{
    [HttpGet("Get")]
    public ActionResult Get(Guid id)
    {
        return Ok(menuService.Get(id));
    }

    [HttpGet("GetAll")]
    public ActionResult GetAll()
    {
        return Ok(menuService.GetList());
    }

    [HttpPost("Add")]
    public ActionResult Add(Menu entity)
    {
        var result = menuService.Add(entity);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPost("Update")]
    public ActionResult Update(Menu entity)
    {
        var result = menuService.Update(entity);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPost("Delete")]
    public ActionResult Delete(Menu entity)
    {
        var result = menuService.Delete(entity);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}