using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MailKit.Security;

namespace SMTPing;

public partial class Form1 : Form
{
    private readonly SmtpService _smtpService = new();
    private bool _isBusy;

    public Form1()
    {
        InitializeComponent();
        SetApplicationIcon();
        SetupEvents();
        LoadSavedSettings();
    }

    private void SetApplicationIcon()
    {
        try
        {
            var exeIcon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            if (exeIcon != null)
            {
                this.Icon = exeIcon;
            }
            else if (File.Exists("app.ico"))
            {
                this.Icon = new Icon("app.ico");
            }
        }
        catch
        {
            // Fallback
        }
    }

    private void SetupEvents()
    {
        btnTogglePassword.Click += (s, e) =>
        {
            txtPassword.UseSystemPasswordChar = !txtPassword.UseSystemPasswordChar;
            btnTogglePassword.Text = txtPassword.UseSystemPasswordChar ? "👁" : "🔒";
        };

        chkUseAuth.CheckedChanged += (s, e) =>
        {
            txtUsername.Enabled = chkUseAuth.Checked;
            txtPassword.Enabled = chkUseAuth.Checked;
            btnTogglePassword.Enabled = chkUseAuth.Checked;
        };

        numPort.ValueChanged += (s, e) =>
        {
            var port = (int)numPort.Value;
            if (port == 465)
            {
                cmbSsl.SelectedIndex = 2; // SSL / TLS
            }
            else if (port is 587 or 25 or 2525)
            {
                cmbSsl.SelectedIndex = 1; // StartTLS
            }
        };

        btnClearLog.Click += (s, e) => rtbLog.Clear();

        btnCopyLog.Click += (s, e) =>
        {
            if (!string.IsNullOrEmpty(rtbLog.Text))
            {
                Clipboard.SetText(rtbLog.Text);
                lblStatus.Text = "Günlük panoya kopyalandı.";
            }
        };

        btnTestConnection.Click += async (s, e) => await HandleTestConnectionAsync();
        btnSendMail.Click += async (s, e) => await HandleSendMailAsync();

        btnAbout.Click += (s, e) =>
        {
            using var about = new AboutForm();
            about.ShowDialog(this);
        };

        lblAboutStatus.Click += (s, e) =>
        {
            using var about = new AboutForm();
            about.ShowDialog(this);
        };

        FormClosing += (s, e) => SaveCurrentSettings();
    }

    private void LoadSavedSettings()
    {
        var s = AppSettings.Load();
        txtHost.Text = s.Host;
        numPort.Value = Math.Clamp(s.Port, 1, 65535);
        cmbSsl.SelectedIndex = Math.Clamp(s.SslModeIndex, 0, cmbSsl.Items.Count - 1);
        chkIgnoreCert.Checked = s.IgnoreCertErrors;
        chkUseAuth.Checked = s.UseAuth;
        txtUsername.Text = s.Username;
        txtFromEmail.Text = s.FromEmail;
        txtFromName.Text = s.FromName;
        txtToEmail.Text = s.ToEmail;
        txtSubject.Text = s.Subject;
        txtBody.Text = s.Body;
        chkIsHtml.Checked = s.IsHtml;

        txtUsername.Enabled = s.UseAuth;
        txtPassword.Enabled = s.UseAuth;
        btnTogglePassword.Enabled = s.UseAuth;
    }

    private void SaveCurrentSettings()
    {
        var s = new AppSettings
        {
            Host = txtHost.Text.Trim(),
            Port = (int)numPort.Value,
            SslModeIndex = cmbSsl.SelectedIndex,
            IgnoreCertErrors = chkIgnoreCert.Checked,
            UseAuth = chkUseAuth.Checked,
            Username = txtUsername.Text.Trim(),
            FromEmail = txtFromEmail.Text.Trim(),
            FromName = txtFromName.Text.Trim(),
            ToEmail = txtToEmail.Text.Trim(),
            Subject = txtSubject.Text.Trim(),
            Body = txtBody.Text,
            IsHtml = chkIsHtml.Checked
        };
        s.Save();
    }

    private SmtpConfig BuildConfig()
    {
        var host = txtHost.Text.Trim();
        if (string.IsNullOrWhiteSpace(host))
        {
            throw new InvalidOperationException("Lütfen SMTP Sunucu adresini belirtin.");
        }

        var secureOption = cmbSsl.SelectedIndex switch
        {
            0 => SecureSocketOptions.Auto,
            1 => SecureSocketOptions.StartTls,
            2 => SecureSocketOptions.SslOnConnect,
            3 => SecureSocketOptions.None,
            _ => SecureSocketOptions.Auto
        };

        return new SmtpConfig
        {
            Host = host,
            Port = (int)numPort.Value,
            SecureSocketOptions = secureOption,
            IgnoreCertErrors = chkIgnoreCert.Checked,
            UseAuth = chkUseAuth.Checked,
            Username = txtUsername.Text.Trim(),
            Password = txtPassword.Text,
            TimeoutSeconds = (int)numTimeout.Value
        };
    }

    private async Task HandleTestConnectionAsync()
    {
        if (_isBusy) return;

        try
        {
            var config = BuildConfig();
            if (config.UseAuth && string.IsNullOrWhiteSpace(config.Username))
            {
                MessageBox.Show("Kimlik doğrulama işaretliyken Kullanıcı Adı boş bırakılamaz.", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SetBusyState(true, "Bağlantı ve kimlik doğrulama test ediliyor...");
            AppendLog($"\n=======================================================\n[BAĞLANTI TESTİ BAŞLATILDI: {DateTime.Now:HH:mm:ss}]\n=======================================================\n");

            var sw = Stopwatch.StartNew();
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(config.TimeoutSeconds + 5));

            await _smtpService.TestConnectionAsync(config, AppendLog, cts.Token);
            sw.Stop();

            lblStatus.Text = "Bağlantı ve kimlik doğrulama testi başarılı.";
            lblDuration.Text = $"Süre: {sw.ElapsedMilliseconds} ms";
            MessageBox.Show("SMTP sunucu bağlantısı ve kimlik doğrulama testi başarıyla tamamlandı!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            AppendLog($"[HATA] {ex.Message}\n");
            lblStatus.Text = "Bağlantı testi başarısız oldu.";
            MessageBox.Show($"Bağlantı kurulamadı:\n{ex.Message}", "Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            SetBusyState(false, "Hazır");
        }
    }

    private async Task HandleSendMailAsync()
    {
        if (_isBusy) return;

        try
        {
            var config = BuildConfig();
            var fromEmail = txtFromEmail.Text.Trim();
            var toEmail = txtToEmail.Text.Trim();

            if (string.IsNullOrWhiteSpace(fromEmail))
            {
                MessageBox.Show("Lütfen Kimden (Gönderici) e-posta adresini girin.", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(toEmail))
            {
                MessageBox.Show("Lütfen Kime (Alıcı) e-posta adresini girin.", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var mail = new MailModel
            {
                FromEmail = fromEmail,
                FromName = txtFromName.Text.Trim(),
                ToEmail = toEmail,
                Subject = txtSubject.Text.Trim(),
                Body = txtBody.Text,
                IsHtml = chkIsHtml.Checked
            };

            SetBusyState(true, "Test e-postası iletiliyor...");
            AppendLog($"\n=======================================================\n[TEST E-POSTASI GÖNDERİMİ: {DateTime.Now:HH:mm:ss}]\n=======================================================\n");

            var sw = Stopwatch.StartNew();
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(config.TimeoutSeconds + 15));

            await _smtpService.SendTestMailAsync(config, mail, AppendLog, cts.Token);
            sw.Stop();

            lblStatus.Text = "Test e-postası başarıyla iletildi.";
            lblDuration.Text = $"Süre: {sw.ElapsedMilliseconds} ms";
            MessageBox.Show($"Test e-postası '{toEmail}' adresine başarıyla iletildi!", "E-posta Gönderildi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            AppendLog($"[HATA] {ex.Message}\n");
            lblStatus.Text = "E-posta gönderimi başarısız oldu.";
            MessageBox.Show($"E-posta gönderilemedi:\n{ex.Message}", "Gönderim Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            SetBusyState(false, "Hazır");
        }
    }

    private void SetBusyState(bool busy, string status)
    {
        _isBusy = busy;
        btnTestConnection.Enabled = !busy;
        btnSendMail.Enabled = !busy;
        Cursor = busy ? Cursors.WaitCursor : Cursors.Default;
        lblStatus.Text = status;
    }

    private void AppendLog(string text)
    {
        if (InvokeRequired)
        {
            BeginInvoke(new Action<string>(AppendLog), text);
            return;
        }

        Color textColor;
        if (text.Contains("[BAŞARILI]"))
        {
            textColor = Color.FromArgb(74, 222, 128); // Mint green
        }
        else if (text.Contains("[HATA]"))
        {
            textColor = Color.FromArgb(248, 113, 113); // Coral red
        }
        else if (text.Contains("[UYARI]"))
        {
            textColor = Color.FromArgb(251, 191, 36); // Amber
        }
        else if (text.Contains("[BİLGİ]"))
        {
            textColor = Color.FromArgb(56, 189, 248); // Sky blue
        }
        else if (text.StartsWith("S: ") || text.Contains("\nS: "))
        {
            textColor = Color.FromArgb(34, 211, 238); // Cyan
        }
        else if (text.StartsWith("C: ") || text.Contains("\nC: "))
        {
            textColor = Color.FromArgb(253, 224, 71); // Gold
        }
        else
        {
            textColor = Color.FromArgb(226, 232, 240); // Slate light
        }

        rtbLog.SelectionStart = rtbLog.TextLength;
        rtbLog.SelectionLength = 0;
        rtbLog.SelectionColor = textColor;
        rtbLog.AppendText(text);
        rtbLog.SelectionColor = rtbLog.ForeColor;

        if (chkAutoScroll.Checked)
        {
            rtbLog.SelectionStart = rtbLog.TextLength;
            rtbLog.ScrollToCaret();
        }
    }
}
