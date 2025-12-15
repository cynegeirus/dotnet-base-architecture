namespace Core.Entities.Concrete.Configuration;

public class MailConfiguration
{
    public string? IpAddress { get; set; }
    public int PortNumber { get; set; }
    public string? SenderTitle { get; set; }
    public string? SenderMailAddress { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public bool EnableSsl { get; set; }
}