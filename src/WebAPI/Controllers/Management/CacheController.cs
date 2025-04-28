using Business.Constants;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.IoC;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Management;

[Route("api/Management/[controller]")]
[ApiController]
public class CacheController : ControllerBase
{
    private readonly ICacheManager _cacheManager = ServiceTool.ServiceProvider!.GetService<ICacheManager>()!;

    [HttpGet("GetAll")]
    public IActionResult GetAll()
    {
        var overview = _cacheManager.Overview();
        return Ok(overview);
    }

    [HttpGet("Remove")]
    public IActionResult Remove(string key)
    {
        _cacheManager.Remove(key);
        return Ok(CustomMessage.CacheRemoved);
    }

    [HttpGet("RemoveAll")]
    public IActionResult RemoveAll()
    {
        _cacheManager.RemoveAll();
        return Ok(new SuccessResult(CustomMessage.CacheRemovedAll));
    }
}