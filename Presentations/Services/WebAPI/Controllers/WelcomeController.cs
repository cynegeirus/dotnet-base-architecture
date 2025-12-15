using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("")]
public class WelcomeController : ControllerBase
{
    private static readonly DateTime StartTime = DateTime.UtcNow;

    [HttpGet]
    public IActionResult Get()
    {
        var hostName = Dns.GetHostName();

        var ipAddresses = Dns.GetHostAddresses(hostName)
            .Where(ip => ip.AddressFamily == AddressFamily.InterNetwork)
            .Select(ip => ip.ToString())
            .OrderBy(ip => ip)
            .ToList();

        var process = Process.GetCurrentProcess();

        return Ok(new
        {
            Application = new
            {
                Name = AppDomain.CurrentDomain.FriendlyName,
                Framework = RuntimeInformation.FrameworkDescription,
                ProcessId = process.Id
            },
            Host = new
            {
                MachineName = $"{Environment.MachineName} ({hostName})",
                IpAddresses = ipAddresses,
                OperatingSystem = RuntimeInformation.OSDescription,
                Architecture = RuntimeInformation.OSArchitecture.ToString()
            },
            Runtime = new
            {
                Uptime = (DateTime.UtcNow - StartTime).ToString(@"dd\.hh\:mm\:ss"),
                UptimeSeconds = (int)(DateTime.UtcNow - StartTime).TotalSeconds
            },
            Time = new
            {
                Utc = DateTime.UtcNow,
                Local = DateTime.Now,
                TimeZone = TimeZoneInfo.Local.DisplayName
            }
        });
    }
}