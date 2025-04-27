using Business.Abstract;
using Core.Entities.Concrete.Management;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Management;

[ApiController]
[Route("api/Management/[controller]")]
public class NotificationController(INotificationService notificationService) : ControllerBase
{
    [HttpGet("Get")]
    [Authorize(Roles = "Administrator,User")]
    public ActionResult Get(Guid id)
    {
        return Ok(notificationService.Get(id));
    }

    [HttpGet("GetAll")]
    [Authorize(Roles = "Administrator,User")]
    public ActionResult GetAll()
    {
        return Ok(notificationService.GetList());
    }

    [HttpPost("Add")]
    [Authorize(Roles = "Administrator")]
    public ActionResult Add(Notification entity)
    {
        var result = notificationService.Add(entity);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPost("Update")]
    [Authorize(Roles = "Administrator")]
    public ActionResult Update(Notification entity)
    {
        var result = notificationService.Update(entity);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPost("Delete")]
    [Authorize(Roles = "Administrator")]
    public ActionResult Delete(Notification entity)
    {
        var result = notificationService.Delete(entity);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}