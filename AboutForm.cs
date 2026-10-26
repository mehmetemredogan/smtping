using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SMTPing;

public class AboutForm : Form
{
    public AboutForm()
    {
        InitializeComponent();
        SetFormIcon();
    }

    private void SetFormIcon()
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
        catch { }
    }

    private void InitializeComponent()
    {
        this.ClientSize = new Size(540, 480);
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.ShowInTaskbar = false;
        this.StartPosition = FormStartPosition.CenterParent;
        this.Text = "Hakkında - SMTPing";
        this.BackColor = Color.White;
        this.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);

        // ============================
        // Üst Başlık Bölümü
        // ============================
        var pnlTop = new Panel
        {
            Dock = DockStyle.Top,
            Height = 115,
            BackColor = Color.FromArgb(248, 250, 252),
            Padding = new Padding(24, 18, 24, 16)
        };
        pnlTop.Paint += (s, e) =>
        {
            using var pen = new Pen(Color.FromArgb(226, 232, 240), 1);
            e.Graphics.DrawLine(pen, 0, pnlTop.Height - 1, pnlTop.Width, pnlTop.Height - 1);
        };

        var picIcon = new PictureBox
        {
            Location = new Point(24, 20),
            Size = new Size(56, 56),
            SizeMode = PictureBoxSizeMode.Zoom
        };
        try
        {
            if (File.Exists("app.ico"))
            {
                using var ico = new Icon("app.ico", 64, 64);
                picIcon.Image = ico.ToBitmap();
            }
            else
            {
                picIcon.Image = Icon.ExtractAssociatedIcon(Application.ExecutablePath)?.ToBitmap();
            }
        }
        catch { }

        var lblTitle = new Label
        {
            Text = "SMTPing",
            Font = new Font("Segoe UI", 16F, FontStyle.Bold),
            ForeColor = Color.FromArgb(15, 23, 42),
            Location = new Point(92, 16),
            AutoSize = true
        };

        var lblVersion = new Label
        {
            Text = "Sürüm 1.0.0 (.NET 8.0)",
            Font = new Font("Segoe UI", 8.5F),
            ForeColor = Color.FromArgb(100, 116, 139),
            Location = new Point(94, 48),
            AutoSize = true
        };

        var lblSubtitle = new Label
        {
            Text = "Hızlı, Güvenli ve Modern SMTP Test & Tanılama Aracı",
            Font = new Font("Segoe UI", 8.5F, FontStyle.Italic),
            ForeColor = Color.FromArgb(71, 85, 105),
            Location = new Point(94, 70),
            AutoSize = true
        };

        pnlTop.Controls.Add(picIcon);
        pnlTop.Controls.Add(lblTitle);
        pnlTop.Controls.Add(lblVersion);
        pnlTop.Controls.Add(lblSubtitle);

        // ============================
        // Lisans & Geliştirici Bilgisi
        // ============================
        var pnlContent = new Panel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(24, 16, 24, 16)
        };

        var lblAuthorTitle = new Label
        {
            Text = "Telif Hakkı & Lisans:",
            Font = new Font("Segoe UI", 9F, FontStyle.Bold),
            ForeColor = Color.FromArgb(30, 41, 59),
            Location = new Point(24, 14),
            AutoSize = true
        };

        var lblAuthor = new Label
        {
            Text = "Mehmet Emre Doğan | MIT License - 2026",
            Font = new Font("Segoe UI", 9.5F),
            ForeColor = Color.FromArgb(15, 23, 42),
            Location = new Point(24, 36),
            AutoSize = true
        };

        var lblRepoTitle = new Label
        {
            Text = "GitHub Deposu:",
            Font = new Font("Segoe UI", 9F, FontStyle.Bold),
            ForeColor = Color.FromArgb(30, 41, 59),
            Location = new Point(24, 68),
            AutoSize = true
        };

        var lnkGithub = new LinkLabel
        {
            Text = "https://github.com/mehmetemredogan/smtping",
            Font = new Font("Segoe UI", 9.5F),
            LinkColor = Color.FromArgb(37, 99, 235),
            ActiveLinkColor = Color.FromArgb(29, 78, 216),
            VisitedLinkColor = Color.FromArgb(37, 99, 235),
            Location = new Point(24, 90),
            AutoSize = true,
            Cursor = Cursors.Hand
        };
        lnkGithub.LinkClicked += (s, e) =>
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "https://github.com/mehmetemredogan/smtping",
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bağlantı açılamadı: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        };

        // ============================
        // Açık Kaynak Kütüphaneler Grubu
        // ============================
        var grpLibs = new GroupBox
        {
            Text = " Kullanılan Açık Kaynaklı Bileşenler ve Lisanslar ",
            Font = new Font("Segoe UI", 9F, FontStyle.Bold),
            ForeColor = Color.FromArgb(30, 41, 59),
            Location = new Point(24, 126),
            Size = new Size(492, 160)
        };

        var txtLibs = new TextBox
        {
            Multiline = true,
            ReadOnly = true,
            BorderStyle = BorderStyle.None,
            BackColor = Color.White,
            Font = new Font("Segoe UI", 8.5F),
            ForeColor = Color.FromArgb(51, 65, 85),
            Location = new Point(14, 26),
            Size = new Size(464, 120),
            ScrollBars = ScrollBars.Vertical,
            Text = "• MailKit (v4.18.2) — MIT License\r\n" +
                   "  Telif Hakkı: Jeffrey Stedfast\r\n" +
                   "  Modern ve standart-uyumlu .NET SMTP protokol istemcisi.\r\n\r\n" +
                   "• MimeKit (v4.18.2) — MIT License\r\n" +
                   "  Telif Hakkı: Jeffrey Stedfast\r\n" +
                   "  MIME mesaj oluşturma, ayrıştırma ve karakter kodlama motoru.\r\n\r\n" +
                   "• BouncyCastle.Cryptography (v2.7.0) — MIT License\r\n" +
                   "  Telif Hakkı: Legion of the Bouncy Castle Inc.\r\n" +
                   "  Gelişmiş kriptografik algoritmalar ve X.509 sertifika desteği.\r\n\r\n" +
                   "• Microsoft .NET 8 & Windows Forms — MIT License\r\n" +
                   "  Telif Hakkı: .NET Foundation and Contributors\r\n" +
                   "  Açık kaynaklı modern Windows masaüstü uygulama çalışma ortamı."
        };

        grpLibs.Controls.Add(txtLibs);

        pnlContent.Controls.Add(lblAuthorTitle);
        pnlContent.Controls.Add(lblAuthor);
        pnlContent.Controls.Add(lblRepoTitle);
        pnlContent.Controls.Add(lnkGithub);
        pnlContent.Controls.Add(grpLibs);

        // ============================
        // Alt Buton Bölümü
        // ============================
        var pnlBottom = new Panel
        {
            Dock = DockStyle.Bottom,
            Height = 60,
            BackColor = Color.FromArgb(248, 250, 252),
            Padding = new Padding(0, 0, 24, 0)
        };
        pnlBottom.Paint += (s, e) =>
        {
            using var pen = new Pen(Color.FromArgb(226, 232, 240), 1);
            e.Graphics.DrawLine(pen, 0, 0, pnlBottom.Width, 0);
        };

        var btnClose = new Button
        {
            Text = "Kapat",
            DialogResult = DialogResult.OK,
            Size = new Size(95, 34),
            Location = new Point(420, 13),
            BackColor = Color.FromArgb(37, 99, 235),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Font = new Font("Segoe UI", 9F, FontStyle.Bold),
            Cursor = Cursors.Hand
        };
        btnClose.FlatAppearance.BorderSize = 0;
        this.AcceptButton = btnClose;

        pnlBottom.Controls.Add(btnClose);

        // Bileşenleri Forma Ekle
        this.Controls.Add(pnlContent);
        this.Controls.Add(pnlBottom);
        this.Controls.Add(pnlTop);
    }
}
