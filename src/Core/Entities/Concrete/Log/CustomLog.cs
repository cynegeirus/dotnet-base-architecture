namespace Core.Entities.Concrete.Log;

public class CustomLog
{
    public DateTime Logged { get; set; }
    public string? Message { get; set; }
    public string? Detail { get; set; }
}