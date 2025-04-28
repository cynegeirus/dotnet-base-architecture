using Core.Utilities.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class WelcomeController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        var data = new
        {
            IpAddress = NetworkHelper.GetLocalIpAddress(),
            Environment.MachineName,
            DomainName = Environment.UserDomainName,
            Environment.UserName
        };

        return Ok(data);
    }
}