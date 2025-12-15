using System.Net;
using System.Net.Mail;
using System.Text;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Entities.Concrete.Configuration;
using Microsoft.Extensions.Configuration;

namespace Core.Utilities.Helpers;

public class MailHelper
{
    private static MailConfiguration? _cachedConfiguration = new();

    private static MailConfiguration GetMailConfiguration()
    {
        if (_cachedConfiguration is not null)
            return _cachedConfiguration;

        _cachedConfiguration = ConfigurationHelper
                                   .GetConfigWithFile("configurationSettings.json")
                                   .GetSection("Mailing")
                                   .Get<MailConfiguration>()
                               ?? throw new InvalidOperationException("Mail configuration not found in configurationSettings.json");

        return _cachedConfiguration;
    }

    public static bool SendMail(MailMessage mail)
    {
        ArgumentNullException.ThrowIfNull(mail);

        var configuration = GetMailConfiguration();

        using var client = new SmtpClient();
        client.Host = configuration.IpAddress!;
        client.Port = configuration.PortNumber;
        client.Credentials = new NetworkCredential(configuration.Username, configuration.Password);
        client.EnableSsl = configuration.EnableSsl;
        client.Timeout = 30000;

        mail.BodyEncoding = Encoding.UTF8;
        mail.SubjectEncoding = Encoding.UTF8;
        mail.HeadersEncoding = Encoding.UTF8;
        mail.From = new MailAddress(configuration.SenderMailAddress!, configuration.SenderTitle);
        mail.Sender = new MailAddress(configuration.SenderMailAddress!, configuration.SenderTitle);

        try
        {
            client.Send(mail);
            return true;
        }
        catch (SmtpException ex)
        {
            ErrorLogger.LogError($"[{nameof(MailHelper)}] SMTP error - Status: {ex.StatusCode}", ex);
            return false;
        }
        catch (Exception ex)
        {
            ErrorLogger.LogError($"[{nameof(MailHelper)}] Unexpected error sending email", ex);
            return false;
        }
    }
}