using Microsoft.AspNetCore.Http;

namespace Core.Entities.Concrete.Log;

public class ErrorLog
{
    public DateTime Logged { get; set; }
    public string? Title { get; set; }
    public Exception? Exception { get; set; }
}