﻿namespace Core.Entities.Concrete.Mail;

public class MailConfiguration
{
    public string? Server { get; set; }
    public int Port { get; set; }
    public string? Sender { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public bool EnableSsl { get; set; }
}