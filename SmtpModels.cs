using MailKit.Security;

namespace SMTPing;

public class SmtpConfig
{
    public string Host { get; set; } = string.Empty;
    public int Port { get; set; } = 587;
    public SecureSocketOptions SecureSocketOptions { get; set; } = SecureSocketOptions.Auto;
    public bool IgnoreCertErrors { get; set; }
    public bool UseAuth { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public int TimeoutSeconds { get; set; } = 15;
}

public class MailModel
{
    public string FromEmail { get; set; } = string.Empty;
    public string FromName { get; set; } = string.Empty;
    public string ToEmail { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public bool IsHtml { get; set; }
}
