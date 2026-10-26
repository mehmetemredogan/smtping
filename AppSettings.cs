using System;
using System.IO;
using System.Text.Json;

namespace SMTPing;

public class AppSettings
{
    public string Host { get; set; } = "smtp.gmail.com";
    public int Port { get; set; } = 587;
    public int SslModeIndex { get; set; } = 1; // 0: Auto, 1: StartTLS, 2: SslOnConnect, 3: None
    public bool IgnoreCertErrors { get; set; } = false;
    public bool UseAuth { get; set; } = true;
    public string Username { get; set; } = "";
    public string FromEmail { get; set; } = "";
    public string FromName { get; set; } = "SMTP Test";
    public string ToEmail { get; set; } = "";
    public string Subject { get; set; } = "SMTP Test Maili";
    public string Body { get; set; } = "Bu bir test e-postasıdır. SMTP bağlantısı ve kimlik doğrulama başarıyla çalışıyor.";
    public bool IsHtml { get; set; } = false;

    private static readonly string SettingsPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "SMTPing",
        "settings.json"
    );

    public static AppSettings Load()
    {
        try
        {
            if (File.Exists(SettingsPath))
            {
                var json = File.ReadAllText(SettingsPath);
                var loaded = JsonSerializer.Deserialize<AppSettings>(json);
                if (loaded != null) return loaded;
            }
        }
        catch
        {
            // Fallback to default
        }
        return new AppSettings();
    }

    public void Save()
    {
        try
        {
            var dir = Path.GetDirectoryName(SettingsPath);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            var json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(SettingsPath, json);
        }
        catch
        {
            // Ignore settings save error
        }
    }
}
