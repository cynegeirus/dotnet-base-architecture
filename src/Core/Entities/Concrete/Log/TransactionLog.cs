namespace Core.Entities.Concrete.Log;

public class TransactionLog
{
    public DateTime Logged { get; set; }
    public string? ConnectionId { get; set; }
    public string? RemoteIp { get; set; }
    public string? RemotePort { get; set; }
    public string? LocalIp { get; set; }
    public string? LocalPort { get; set; }
    public string? Method { get; set; }
    public string? Path { get; set; }
    public string? QueryString { get; set; }
    public string? Payload { get; set; }
    public string? Response { get; set; }
    public string? ResponseCode { get; set; }
    public DateTime? RequestedOn { get; set; }
    public DateTime? RespondedOn { get; set; }
    public bool? IsSuccess { get; set; }
}