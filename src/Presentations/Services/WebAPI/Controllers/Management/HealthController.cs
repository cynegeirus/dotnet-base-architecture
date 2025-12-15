using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace WebAPI.Controllers.Management;

[ApiController]
[Route("api/Management/[controller]")]
public class HealthController(HealthCheckService healthCheckService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var report = await healthCheckService.CheckHealthAsync();

        var response = new
        {
            status = report.Status.ToString(),
            totalDuration = report.TotalDuration.TotalMilliseconds,
            checks = report.Entries.Select(e => new
            {
                name = e.Key,
                status = e.Value.Status.ToString(),
                duration = e.Value.Duration.TotalMilliseconds,
                description = e.Value.Description,
                exception = e.Value.Exception?.Message
            })
        };

        return report.Status == HealthStatus.Healthy
            ? Ok(response)
            : StatusCode(StatusCodes.Status503ServiceUnavailable, response);
    }

    [HttpGet("live")]
    public IActionResult GetLiveness()
    {
        return Ok(new { status = "Healthy", timestamp = DateTime.UtcNow });
    }

    [HttpGet("ready")]
    public async Task<IActionResult> GetReadiness()
    {
        var report = await healthCheckService.CheckHealthAsync(c => c.Tags.Contains("ready"));

        var response = new
        {
            status = report.Status.ToString(),
            timestamp = DateTime.UtcNow,
            checks = report.Entries.Select(e => new
            {
                name = e.Key,
                status = e.Value.Status.ToString(),
                description = e.Value.Description
            })
        };

        return report.Status == HealthStatus.Healthy
            ? Ok(response)
            : StatusCode(StatusCodes.Status503ServiceUnavailable, response);
    }
}