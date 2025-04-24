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
            DateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
            Machine = Environment.MachineName,
            Domain = Environment.UserDomainName,
            User = Environment.UserName
        };

        return Ok(data);
    }
}