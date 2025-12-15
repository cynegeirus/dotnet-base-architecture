using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Main;

[ApiController]
[Route("api/Main/[controller]")]
public class NotificationController(INotificationService notificationService) : ControllerBase
{
    [HttpGet("Get")]
    [Authorize(Roles = "Administrator,User")]
    public ActionResult Get(Guid id)
    {
        var result = notificationService.Get(id);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpGet("GetAll")]
    [Authorize(Roles = "Administrator,User")]
    public ActionResult GetAll()
    {
        var result = notificationService.GetList();
        return Ok(result);
    }

    [HttpPost("Add")]
    [Authorize(Roles = "Administrator")]
    public ActionResult Add(Notification entity)
    {
        var result = notificationService.Add(entity);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPut("Update")]
    [Authorize(Roles = "Administrator")]
    public ActionResult Update(Notification entity)
    {
        var result = notificationService.Update(entity);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpDelete("Delete")]
    [Authorize(Roles = "Administrator")]
    public ActionResult Delete(Notification entity)
    {
        var result = notificationService.Delete(entity);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}