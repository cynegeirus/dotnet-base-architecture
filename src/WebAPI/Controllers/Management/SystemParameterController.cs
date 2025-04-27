using Business.Abstract;
using Core.Entities.Concrete.Management;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Management;

[ApiController]
[Authorize(Roles = "Administrator")]
[Route("api/Management/[controller]")]
public class SystemParameterController(ISystemParameterService systemParameterService) : ControllerBase
{
    [HttpGet("Get")]
    public ActionResult Get(Guid id)
    {
        return Ok(systemParameterService.Get(id));
    }

    [HttpGet("GetAll")]
    public ActionResult GetAll()
    {
        return Ok(systemParameterService.GetList());
    }

    [HttpPost("Add")]
    public ActionResult Add(SystemParameter entity)
    {
        var result = systemParameterService.Add(entity);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPost("Update")]
    public ActionResult Update(SystemParameter entity)
    {
        var result = systemParameterService.Update(entity);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPost("Delete")]
    public ActionResult Delete(SystemParameter entity)
    {
        var result = systemParameterService.Delete(entity);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}