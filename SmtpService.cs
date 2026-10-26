using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace SMTPing;

public class SmtpService
{
    public async Task<string> TestConnectionAsync(SmtpConfig config, Action<string> logger, CancellationToken ct = default)
    {
        var sw = Stopwatch.StartNew();
        using var stream = new DelegateStream(logger);
        using var client = new SmtpClient(new ProtocolLogger(stream));

        client.Timeout = config.TimeoutSeconds * 1000;

        if (config.IgnoreCertErrors)
        {
            client.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            logger($"[UYARI] SSL/TLS sertifika doğrulama denetimi kapatıldı.{Environment.NewLine}");
        }

        logger($"[BİLGİ] {config.Host}:{config.Port} sunucusuna bağlanılıyor ({config.SecureSocketOptions})...{Environment.NewLine}");
        await client.ConnectAsync(config.Host, config.Port, config.SecureSocketOptions, ct);
        sw.Stop();
        logger($"[BAŞARILI] Bağlantı kuruldu! (Süre: {sw.ElapsedMilliseconds} ms){Environment.NewLine}");
        logger($"[BİLGİ] Desteklenen özellikler: {client.Capabilities}{Environment.NewLine}");

        if (config.UseAuth)
        {
            logger($"[BİLGİ] '{config.Username}' kullanıcısı ile kimlik doğrulanıyor...{Environment.NewLine}");
            var authSw = Stopwatch.StartNew();
            await client.AuthenticateAsync(config.Username, config.Password, ct);
            authSw.Stop();
            logger($"[BAŞARILI] Kimlik doğrulama başarılı! (Süre: {authSw.ElapsedMilliseconds} ms){Environment.NewLine}");
        }
        else
        {
            logger($"[BİLGİ] Kimlik doğrulama adımı atlandı.{Environment.NewLine}");
        }

        await client.DisconnectAsync(true, ct);
        logger($"[BİLGİ] Bağlantı düzgün biçimde sonlandırıldı.{Environment.NewLine}");
        return "Bağlantı ve kimlik doğrulama başarılı!";
    }

    public async Task<string> SendTestMailAsync(SmtpConfig config, MailModel mail, Action<string> logger, CancellationToken ct = default)
    {
        var sw = Stopwatch.StartNew();
        using var stream = new DelegateStream(logger);
        using var client = new SmtpClient(new ProtocolLogger(stream));

        client.Timeout = config.TimeoutSeconds * 1000;

        if (config.IgnoreCertErrors)
        {
            client.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            logger($"[UYARI] SSL/TLS sertifika doğrulama denetimi kapatıldı.{Environment.NewLine}");
        }

        logger($"[BİLGİ] {config.Host}:{config.Port} adresine bağlanılıyor...{Environment.NewLine}");
        await client.ConnectAsync(config.Host, config.Port, config.SecureSocketOptions, ct);

        if (config.UseAuth)
        {
            logger($"[BİLGİ] '{config.Username}' kullanıcısı ile kimlik doğrulanıyor...{Environment.NewLine}");
            await client.AuthenticateAsync(config.Username, config.Password, ct);
        }

        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(string.IsNullOrWhiteSpace(mail.FromName) ? mail.FromEmail : mail.FromName, mail.FromEmail));
        message.To.Add(new MailboxAddress("", mail.ToEmail));
        message.Subject = mail.Subject;

        var builder = new BodyBuilder();
        if (mail.IsHtml)
        {
            builder.HtmlBody = mail.Body;
        }
        else
        {
            builder.TextBody = mail.Body;
        }
        message.Body = builder.ToMessageBody();

        logger($"[BİLGİ] '{mail.ToEmail}' adresine test e-postası gönderiliyor...{Environment.NewLine}");
        var sendSw = Stopwatch.StartNew();
        var response = await client.SendAsync(message, ct);
        sendSw.Stop();
        sw.Stop();

        logger($"[BAŞARILI] E-posta gönderildi! (Gönderim: {sendSw.ElapsedMilliseconds} ms, Toplam: {sw.ElapsedMilliseconds} ms){Environment.NewLine}");
        if (!string.IsNullOrWhiteSpace(response))
        {
            logger($"[SUNUCU YANITI] {response.Trim()}{Environment.NewLine}");
        }

        await client.DisconnectAsync(true, ct);
        logger($"[BİLGİ] Bağlantı kapatıldı.{Environment.NewLine}");
        return response ?? "E-posta başarıyla iletildi.";
    }
}
