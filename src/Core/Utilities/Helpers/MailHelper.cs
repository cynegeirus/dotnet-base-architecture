using System.ComponentModel;
using System.Net;
using System.Net.Mail;
using Core.Entities.Concrete.Log;
using Core.Entities.Concrete.Mail;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Core.Utilities.Helpers;

public class MailHelper
{
    private static MailConfiguration GetMailConfiguration()
    {
        return new MailConfiguration
        {
            Server = ConfigurationHelper.GetConfigWithFile("configurationSettings.json").GetValue<string>("Mail:Server"),
            Port = ConfigurationHelper.GetConfigWithFile("configurationSettings.json").GetValue<int>("Mail:Port"),
            Sender = ConfigurationHelper.GetConfigWithFile("configurationSettings.json").GetValue<string>("Mail:Sender"),
            Username = ConfigurationHelper.GetConfigWithFile("configurationSettings.json").GetValue<string>("Mail:Username"),
            Password = ConfigurationHelper.GetConfigWithFile("configurationSettings.json").GetValue<string>("Mail:Password"),
            EnableSsl = ConfigurationHelper.GetConfigWithFile("configurationSettings.json").GetValue<bool>("Mail:EnableSsl")
        };
    }

    public static bool SendMail(MailMessage mail)
    {
        using var client = new SmtpClient();
        client.Host = GetMailConfiguration().Server!;
        client.Port = GetMailConfiguration().Port;

        client.Credentials = new NetworkCredential(GetMailConfiguration().Username, GetMailConfiguration().Password);
        client.EnableSsl = GetMailConfiguration().EnableSsl;

        mail.From = new MailAddress(GetMailConfiguration().Sender!);
        mail.Sender = new MailAddress(GetMailConfiguration().Sender!);

        try
        {
            client.Send(mail);
            client.SendCompleted += SendCompletedEvent;
            FileLogHelper.WriteLog(new CustomLog
            {
                Logged = DateTime.Now,
                Message = "Send Mail: Success",
                Detail = JsonConvert.SerializeObject(mail, Formatting.Indented)
            });
            return true;
        }
        catch (Exception ex)
        {
            FileLogHelper.WriteLog(new CustomLog
            {
                Logged = DateTime.Now,
                Message = ex.Message,
                Detail = ex.ToString()
            });
            return false;
        }
    }

    private static void SendCompletedEvent(object sender, AsyncCompletedEventArgs e)
    {
        FileLogHelper.WriteLog(new CustomLog
        {
            Logged = DateTime.Now,
            Message = e.Cancelled!.ToString(),
            Detail = e.UserState!.ToString()
        });
    }
}