using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.IoC;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Management;

[ApiController]
[Route("api/Management/[controller]")]
public class CacheController : ControllerBase
{
    private readonly ICacheManager _cacheManager = ServiceTool.ServiceProvider!.GetService<ICacheManager>()!;

    [HttpGet("GetAll")]
    public IActionResult GetAll()
    {
        var overview = _cacheManager.Overview();
        return Ok(overview);
    }

    [HttpDelete("Remove")]
    public IActionResult Remove([FromQuery] string key)
    {
        _cacheManager.Remove(key);
        return Ok(CustomMessage.CacheRemoved);
    }

    [HttpDelete("RemoveAll")]
    public IActionResult RemoveAll()
    {
        _cacheManager.RemoveAll();
        return Ok(new SuccessResult(CustomMessage.CacheRemovedAll));
    }

    [HttpGet("Test")]
    [CacheActionFilter]
    public IActionResult Test()
    {
        return Ok(new
        {
            Id = Guid.NewGuid(),
            Logged = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        });
    }
}