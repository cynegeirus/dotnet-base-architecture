using System.ComponentModel.DataAnnotations.Schema;
using Core.Entities.Concrete.Base;

namespace Core.Entities.Concrete.Log;

[Table("TrafficAuditLog", Schema = "audit")]
public class TrafficAuditLog : BaseEntity
{
    public Guid AuditTraceId { get; set; }
    public DateTime AuditLoggedDate { get; set; }
    public DateTime? RequestReceivedDate { get; set; }
    public DateTime? ResponseSendDate { get; set; }
    public Guid? AuditCreatedByUserId { get; set; }
    public Guid? AuditUpdatedByUserId { get; set; }
    public string? HttpMethod { get; set; }
    public string? HttpRequestPath { get; set; }
    public string? HttpQueryString { get; set; }
    public string? HttpScheme { get; set; }
    public string? HttpProtocol { get; set; }
    public string? HttpRequestHeadersJson { get; set; }
    public string? HttpRequestBody { get; set; }
    public int? HttpResponseStatusCode { get; set; }
    public bool? HttpResponseIsSuccess { get; set; }
    public string? HttpResponseContentType { get; set; }
    public long? HttpResponseContentLength { get; set; }
    public string? HttpResponseBodySummary { get; set; }
    public string? ConnectionId { get; set; }
    public string? ClientRemoteIpAddress { get; set; }
    public string? ClientRemotePort { get; set; }
    public string? ServerLocalIpAddress { get; set; }
    public string? ServerLocalPort { get; set; }
    public string? RequestHost { get; set; }
    public string? ServerMachineName { get; set; }
    public string? ApplicationName { get; set; }
    public bool? IsAuthenticated { get; set; }
    public string? AuthenticatedUserName { get; set; }
    public string? AuthenticatedUserRoles { get; set; }
    public string? UserAgent { get; set; }
    public string? Referrer { get; set; }
    public long? RequestDurationMilliseconds { get; set; }
    public bool? HasException { get; set; }
    public string? ExceptionType { get; set; }
    public string? ExceptionMessage { get; set; }
    public string? ExceptionStackTrace { get; set; }
}