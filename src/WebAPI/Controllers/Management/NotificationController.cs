using Business.Abstract;
using Core.Entities.Concrete.Management;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Management;

[ApiController]
[Route("api/Management/[controller]")]
public class NotificationController(INotificationService notificationService) : ControllerBase
{
    [HttpGet("Get")]
    public ActionResult Get(Guid id)
    {
        return Ok(notificationService.Get(id));
    }

    [HttpGet("GetAll")]
    public ActionResult GetAll()
    {
        return Ok(notificationService.GetList());
    }

    [HttpPost("Add")]
    public ActionResult Add(Notification entity)
    {
        var result = notificationService.Add(entity);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPost("Update")]
    public ActionResult Update(Notification entity)
    {
        var result = notificationService.Update(entity);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPost("Delete")]
    public ActionResult Delete(Notification entity)
    {
        var result = notificationService.Delete(entity);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}