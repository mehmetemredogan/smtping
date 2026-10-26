using System.Drawing;
using System.Windows.Forms;

namespace SMTPing;

partial class Form1
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        this.pnlHeader = new Panel();
        this.lblHeaderTitle = new Label();
        this.lblHeaderSubtitle = new Label();
        this.btnAbout = new Button();

        this.pnlLeftContainer = new Panel();
        this.splitterMiddle = new Splitter();
        this.pnlRightContainer = new Panel();

        // 1. Sunucu Kartı
        this.grpServer = new GroupBox();
        this.lblHost = new Label();
        this.txtHost = new TextBox();
        this.lblPort = new Label();
        this.numPort = new NumericUpDown();
        this.lblSsl = new Label();
        this.cmbSsl = new ComboBox();
        this.lblTimeout = new Label();
        this.numTimeout = new NumericUpDown();
        this.chkIgnoreCert = new CheckBox();

        // 2. Kimlik Doğrulama Kartı
        this.grpAuth = new GroupBox();
        this.chkUseAuth = new CheckBox();
        this.lblUsername = new Label();
        this.txtUsername = new TextBox();
        this.lblPassword = new Label();
        this.txtPassword = new TextBox();
        this.btnTogglePassword = new Button();

        // 3. Test Maili Kartı
        this.grpMail = new GroupBox();
        this.lblFrom = new Label();
        this.txtFromEmail = new TextBox();
        this.lblFromName = new Label();
        this.txtFromName = new TextBox();
        this.lblTo = new Label();
        this.txtToEmail = new TextBox();
        this.lblSubject = new Label();
        this.txtSubject = new TextBox();
        this.lblBody = new Label();
        this.txtBody = new TextBox();
        this.chkIsHtml = new CheckBox();

        // Butonlar Paneli
        this.pnlButtons = new Panel();
        this.btnTestConnection = new Button();
        this.btnSendMail = new Button();

        // Sağ Panel (Log)
        this.pnlLogHeader = new Panel();
        this.lblLogTitle = new Label();
        this.chkAutoScroll = new CheckBox();
        this.btnCopyLog = new Button();
        this.btnClearLog = new Button();
        this.rtbLog = new RichTextBox();

        // Status Strip
        this.statusStrip = new StatusStrip();
        this.lblStatus = new ToolStripStatusLabel();
        this.lblSpring = new ToolStripStatusLabel();
        this.lblDuration = new ToolStripStatusLabel();

        this.pnlHeader.SuspendLayout();
        this.pnlLeftContainer.SuspendLayout();
        this.grpServer.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.numPort)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.numTimeout)).BeginInit();
        this.grpAuth.SuspendLayout();
        this.grpMail.SuspendLayout();
        this.pnlButtons.SuspendLayout();
        this.pnlRightContainer.SuspendLayout();
        this.pnlLogHeader.SuspendLayout();
        this.statusStrip.SuspendLayout();
        this.SuspendLayout();

        // ==========================================
        // Form Genel Özellikleri
        // ==========================================
        this.ClientSize = new Size(1180, 810);
        this.MinimumSize = new Size(980, 680);
        this.Text = "SMTPing - Modern SMTP Test & Tanılama Aracı";
        this.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.BackColor = Color.FromArgb(244, 246, 249);

        // ==========================================
        // Üst Başlık (Header Bar)
        // ==========================================
        this.pnlHeader.Dock = DockStyle.Top;
        this.pnlHeader.Height = 64;
        this.pnlHeader.BackColor = Color.White;
        this.pnlHeader.Padding = new Padding(20, 10, 20, 10);
        this.pnlHeader.Paint += (s, e) =>
        {
            using var pen = new Pen(Color.FromArgb(226, 232, 240), 1);
            e.Graphics.DrawLine(pen, 0, pnlHeader.Height - 1, pnlHeader.Width, pnlHeader.Height - 1);
        };

        this.lblHeaderTitle.Text = "⚡ SMTPing";
        this.lblHeaderTitle.Font = new Font("Segoe UI", 13.5F, FontStyle.Bold, GraphicsUnit.Point);
        this.lblHeaderTitle.ForeColor = Color.FromArgb(15, 23, 42);
        this.lblHeaderTitle.Location = new Point(18, 9);
        this.lblHeaderTitle.AutoSize = true;

        this.lblHeaderSubtitle.Text = "SMTP sunucu bağlantı durumu, SSL/TLS el sıkışması ve test e-postası tanılama aracı";
        this.lblHeaderSubtitle.Font = new Font("Segoe UI", 8.5F, FontStyle.Regular, GraphicsUnit.Point);
        this.lblHeaderSubtitle.ForeColor = Color.FromArgb(100, 116, 139);
        this.lblHeaderSubtitle.Location = new Point(20, 36);
        this.lblHeaderSubtitle.AutoSize = true;

        this.pnlHeaderRight = new Panel();
        this.pnlHeaderRight.Dock = DockStyle.Right;
        this.pnlHeaderRight.Width = 140;
        this.pnlHeaderRight.Padding = new Padding(0, 14, 20, 14);

        this.btnAbout.Text = "ℹ️  Hakkında";
        this.btnAbout.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        this.btnAbout.ForeColor = Color.FromArgb(37, 99, 235);
        this.btnAbout.BackColor = Color.FromArgb(239, 246, 255);
        this.btnAbout.FlatStyle = FlatStyle.Flat;
        this.btnAbout.FlatAppearance.BorderColor = Color.FromArgb(191, 219, 254);
        this.btnAbout.Dock = DockStyle.Fill;
        this.btnAbout.Cursor = Cursors.Hand;

        this.pnlHeaderRight.Controls.Add(this.btnAbout);

        this.pnlHeader.Controls.Add(this.pnlHeaderRight);
        this.pnlHeader.Controls.Add(this.lblHeaderTitle);
        this.pnlHeader.Controls.Add(this.lblHeaderSubtitle);

        // ==========================================
        // Sol Panel (Form Kontrolleri) - FIXED DOCK LEFT
        // ==========================================
        this.pnlLeftContainer.Dock = DockStyle.Left;
        this.pnlLeftContainer.Width = 490;
        this.pnlLeftContainer.AutoScroll = true;
        this.pnlLeftContainer.BackColor = Color.FromArgb(248, 250, 252);
        this.pnlLeftContainer.Padding = new Padding(12);

        // ------------------------------------------
        // 1. Sunucu Ayarları Grubu
        // ------------------------------------------
        this.grpServer.Text = " 1. Sunucu ve Bağlantı Ayarları ";
        this.grpServer.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        this.grpServer.ForeColor = Color.FromArgb(30, 41, 59);
        this.grpServer.BackColor = Color.White;
        this.grpServer.Location = new Point(12, 12);
        this.grpServer.Size = new Size(446, 175);

        this.lblHost.Text = "Sunucu Adresi (Host):";
        this.lblHost.Font = new Font("Segoe UI", 8.5F);
        this.lblHost.ForeColor = Color.FromArgb(71, 85, 105);
        this.lblHost.Location = new Point(16, 24);
        this.lblHost.AutoSize = true;

        this.txtHost.Font = new Font("Segoe UI", 9.5F);
        this.txtHost.Location = new Point(16, 44);
        this.txtHost.Size = new Size(414, 25);

        this.lblPort.Text = "Port:";
        this.lblPort.Font = new Font("Segoe UI", 8.5F);
        this.lblPort.ForeColor = Color.FromArgb(71, 85, 105);
        this.lblPort.Location = new Point(16, 76);
        this.lblPort.AutoSize = true;

        this.numPort.Font = new Font("Segoe UI", 9.5F);
        this.numPort.Location = new Point(16, 96);
        this.numPort.Size = new Size(85, 25);
        this.numPort.Minimum = 1;
        this.numPort.Maximum = 65535;
        this.numPort.Value = 587;

        this.lblSsl.Text = "SSL / TLS Modu:";
        this.lblSsl.Font = new Font("Segoe UI", 8.5F);
        this.lblSsl.ForeColor = Color.FromArgb(71, 85, 105);
        this.lblSsl.Location = new Point(112, 76);
        this.lblSsl.AutoSize = true;

        this.cmbSsl.Font = new Font("Segoe UI", 9F);
        this.cmbSsl.Location = new Point(112, 96);
        this.cmbSsl.Size = new Size(205, 25);
        this.cmbSsl.DropDownStyle = ComboBoxStyle.DropDownList;
        this.cmbSsl.Items.AddRange(new object[] {
            "Otomatik (Auto)",
            "StartTLS (587 / 25 önerilir)",
            "SSL / TLS (465 önerilir)",
            "Güvenlik Yok (None)"
        });
        this.cmbSsl.SelectedIndex = 1;

        this.lblTimeout.Text = "Zaman Aşımı (sn):";
        this.lblTimeout.Font = new Font("Segoe UI", 8.5F);
        this.lblTimeout.ForeColor = Color.FromArgb(71, 85, 105);
        this.lblTimeout.Location = new Point(328, 76);
        this.lblTimeout.AutoSize = true;

        this.numTimeout.Font = new Font("Segoe UI", 9.5F);
        this.numTimeout.Location = new Point(328, 96);
        this.numTimeout.Size = new Size(102, 25);
        this.numTimeout.Minimum = 3;
        this.numTimeout.Maximum = 120;
        this.numTimeout.Value = 15;

        this.chkIgnoreCert.Text = "Sertifika Hatalarını Yoksay (Self-signed / Test)";
        this.chkIgnoreCert.Font = new Font("Segoe UI", 8.5F);
        this.chkIgnoreCert.ForeColor = Color.FromArgb(51, 65, 85);
        this.chkIgnoreCert.Location = new Point(16, 136);
        this.chkIgnoreCert.AutoSize = true;

        this.grpServer.Controls.Add(this.lblHost);
        this.grpServer.Controls.Add(this.txtHost);
        this.grpServer.Controls.Add(this.lblPort);
        this.grpServer.Controls.Add(this.numPort);
        this.grpServer.Controls.Add(this.lblSsl);
        this.grpServer.Controls.Add(this.cmbSsl);
        this.grpServer.Controls.Add(this.lblTimeout);
        this.grpServer.Controls.Add(this.numTimeout);
        this.grpServer.Controls.Add(this.chkIgnoreCert);

        // ------------------------------------------
        // 2. Kimlik Doğrulama Grubu
        // ------------------------------------------
        this.grpAuth.Text = " 2. Kimlik Doğrulama (AUTH) ";
        this.grpAuth.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        this.grpAuth.ForeColor = Color.FromArgb(30, 41, 59);
        this.grpAuth.BackColor = Color.White;
        this.grpAuth.Location = new Point(12, 198);
        this.grpAuth.Size = new Size(446, 155);

        this.chkUseAuth.Text = "Kimlik Doğrulama Gerekli";
        this.chkUseAuth.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        this.chkUseAuth.ForeColor = Color.FromArgb(15, 23, 42);
        this.chkUseAuth.Location = new Point(16, 22);
        this.chkUseAuth.Checked = true;
        this.chkUseAuth.AutoSize = true;

        this.lblUsername.Text = "Kullanıcı Adı / E-posta:";
        this.lblUsername.Font = new Font("Segoe UI", 8.5F);
        this.lblUsername.ForeColor = Color.FromArgb(71, 85, 105);
        this.lblUsername.Location = new Point(16, 48);
        this.lblUsername.AutoSize = true;

        this.txtUsername.Font = new Font("Segoe UI", 9.5F);
        this.txtUsername.Location = new Point(16, 68);
        this.txtUsername.Size = new Size(414, 25);

        this.lblPassword.Text = "Parola / Uygulama Şifresi:";
        this.lblPassword.Font = new Font("Segoe UI", 8.5F);
        this.lblPassword.ForeColor = Color.FromArgb(71, 85, 105);
        this.lblPassword.Location = new Point(16, 96);
        this.lblPassword.AutoSize = true;

        this.txtPassword.Font = new Font("Segoe UI", 9.5F);
        this.txtPassword.Location = new Point(16, 116);
        this.txtPassword.Size = new Size(372, 25);
        this.txtPassword.UseSystemPasswordChar = true;

        this.btnTogglePassword.Text = "👁";
        this.btnTogglePassword.Font = new Font("Segoe UI", 9F);
        this.btnTogglePassword.Location = new Point(392, 115);
        this.btnTogglePassword.Size = new Size(38, 27);
        this.btnTogglePassword.Cursor = Cursors.Hand;

        this.grpAuth.Controls.Add(this.chkUseAuth);
        this.grpAuth.Controls.Add(this.lblUsername);
        this.grpAuth.Controls.Add(this.txtUsername);
        this.grpAuth.Controls.Add(this.lblPassword);
        this.grpAuth.Controls.Add(this.txtPassword);
        this.grpAuth.Controls.Add(this.btnTogglePassword);

        // ------------------------------------------
        // 3. Test Maili Bilgileri Grubu
        // ------------------------------------------
        this.grpMail.Text = " 3. Test Maili Bilgileri ";
        this.grpMail.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        this.grpMail.ForeColor = Color.FromArgb(30, 41, 59);
        this.grpMail.BackColor = Color.White;
        this.grpMail.Location = new Point(12, 364);
        this.grpMail.Size = new Size(446, 288);

        this.lblFrom.Text = "Kimden (E-posta):";
        this.lblFrom.Font = new Font("Segoe UI", 8.5F);
        this.lblFrom.ForeColor = Color.FromArgb(71, 85, 105);
        this.lblFrom.Location = new Point(16, 24);
        this.lblFrom.AutoSize = true;

        this.txtFromEmail.Font = new Font("Segoe UI", 9.5F);
        this.txtFromEmail.Location = new Point(16, 44);
        this.txtFromEmail.Size = new Size(200, 25);

        this.lblFromName.Text = "Gönderici Adı:";
        this.lblFromName.Font = new Font("Segoe UI", 8.5F);
        this.lblFromName.ForeColor = Color.FromArgb(71, 85, 105);
        this.lblFromName.Location = new Point(226, 24);
        this.lblFromName.AutoSize = true;

        this.txtFromName.Font = new Font("Segoe UI", 9.5F);
        this.txtFromName.Location = new Point(226, 44);
        this.txtFromName.Size = new Size(204, 25);

        this.lblTo.Text = "Kime (Alıcı E-posta):";
        this.lblTo.Font = new Font("Segoe UI", 8.5F);
        this.lblTo.ForeColor = Color.FromArgb(71, 85, 105);
        this.lblTo.Location = new Point(16, 76);
        this.lblTo.AutoSize = true;

        this.txtToEmail.Font = new Font("Segoe UI", 9.5F);
        this.txtToEmail.Location = new Point(16, 96);
        this.txtToEmail.Size = new Size(414, 25);

        this.lblSubject.Text = "Konu Başlığı:";
        this.lblSubject.Font = new Font("Segoe UI", 8.5F);
        this.lblSubject.ForeColor = Color.FromArgb(71, 85, 105);
        this.lblSubject.Location = new Point(16, 128);
        this.lblSubject.AutoSize = true;

        this.txtSubject.Font = new Font("Segoe UI", 9.5F);
        this.txtSubject.Location = new Point(16, 148);
        this.txtSubject.Size = new Size(414, 25);

        this.lblBody.Text = "Mesaj İçeriği:";
        this.lblBody.Font = new Font("Segoe UI", 8.5F);
        this.lblBody.ForeColor = Color.FromArgb(71, 85, 105);
        this.lblBody.Location = new Point(16, 178);
        this.lblBody.AutoSize = true;

        this.txtBody.Font = new Font("Segoe UI", 9F);
        this.txtBody.Location = new Point(16, 198);
        this.txtBody.Size = new Size(414, 52);
        this.txtBody.Multiline = true;
        this.txtBody.ScrollBars = ScrollBars.Vertical;

        this.chkIsHtml.Text = "HTML Biçiminde Gönder";
        this.chkIsHtml.Font = new Font("Segoe UI", 8.5F);
        this.chkIsHtml.ForeColor = Color.FromArgb(51, 65, 85);
        this.chkIsHtml.Location = new Point(16, 258);
        this.chkIsHtml.AutoSize = true;

        this.grpMail.Controls.Add(this.lblFrom);
        this.grpMail.Controls.Add(this.txtFromEmail);
        this.grpMail.Controls.Add(this.lblFromName);
        this.grpMail.Controls.Add(this.txtFromName);
        this.grpMail.Controls.Add(this.lblTo);
        this.grpMail.Controls.Add(this.txtToEmail);
        this.grpMail.Controls.Add(this.lblSubject);
        this.grpMail.Controls.Add(this.txtSubject);
        this.grpMail.Controls.Add(this.lblBody);
        this.grpMail.Controls.Add(this.txtBody);
        this.grpMail.Controls.Add(this.chkIsHtml);

        // ------------------------------------------
        // İşlem Butonları Paneli
        // ------------------------------------------
        this.pnlButtons.Location = new Point(12, 662);
        this.pnlButtons.Size = new Size(446, 52);

        this.btnTestConnection.Text = "🔌  Bağlantıyı Test Et";
        this.btnTestConnection.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        this.btnTestConnection.Location = new Point(0, 4);
        this.btnTestConnection.Size = new Size(216, 44);
        this.btnTestConnection.BackColor = Color.FromArgb(37, 99, 235);
        this.btnTestConnection.ForeColor = Color.White;
        this.btnTestConnection.FlatStyle = FlatStyle.Flat;
        this.btnTestConnection.FlatAppearance.BorderSize = 0;
        this.btnTestConnection.Cursor = Cursors.Hand;

        this.btnSendMail.Text = "✉️  Test Maili Gönder";
        this.btnSendMail.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        this.btnSendMail.Location = new Point(228, 4);
        this.btnSendMail.Size = new Size(218, 44);
        this.btnSendMail.BackColor = Color.FromArgb(16, 185, 129);
        this.btnSendMail.ForeColor = Color.White;
        this.btnSendMail.FlatStyle = FlatStyle.Flat;
        this.btnSendMail.FlatAppearance.BorderSize = 0;
        this.btnSendMail.Cursor = Cursors.Hand;

        this.pnlButtons.Controls.Add(this.btnTestConnection);
        this.pnlButtons.Controls.Add(this.btnSendMail);

        // Sol Panele Kartları Ekle
        this.pnlLeftContainer.Controls.Add(this.grpServer);
        this.pnlLeftContainer.Controls.Add(this.grpAuth);
        this.pnlLeftContainer.Controls.Add(this.grpMail);
        this.pnlLeftContainer.Controls.Add(this.pnlButtons);

        // ==========================================
        // Ayırıcı (Splitter)
        // ==========================================
        this.splitterMiddle.Dock = DockStyle.Left;
        this.splitterMiddle.Width = 6;
        this.splitterMiddle.BackColor = Color.FromArgb(226, 232, 240);
        this.splitterMiddle.MinSize = 460;
        this.splitterMiddle.MinExtra = 350;

        // ==========================================
        // Sağ Panel (Terminal & Canlı Log) - DOCK FILL
        // ==========================================
        this.pnlRightContainer.Dock = DockStyle.Fill;
        this.pnlRightContainer.BackColor = Color.FromArgb(244, 246, 249);
        this.pnlRightContainer.Padding = new Padding(12);

        this.pnlLogHeader.Dock = DockStyle.Top;
        this.pnlLogHeader.Height = 38;
        this.pnlLogHeader.BackColor = Color.Transparent;

        this.lblLogTitle.Text = "🖥️  Canlı İletişim ve Protokol Günlüğü (SMTP Log)";
        this.lblLogTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        this.lblLogTitle.ForeColor = Color.FromArgb(30, 41, 59);
        this.lblLogTitle.Location = new Point(0, 8);
        this.lblLogTitle.AutoSize = true;

        this.btnClearLog.Text = "🗑️ Temizle";
        this.btnClearLog.Font = new Font("Segoe UI", 8.5F);
        this.btnClearLog.BackColor = Color.White;
        this.btnClearLog.FlatStyle = FlatStyle.Flat;
        this.btnClearLog.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
        this.btnClearLog.Dock = DockStyle.Right;
        this.btnClearLog.Width = 85;
        this.btnClearLog.Cursor = Cursors.Hand;

        this.btnCopyLog.Text = "📋 Kopyala";
        this.btnCopyLog.Font = new Font("Segoe UI", 8.5F);
        this.btnCopyLog.BackColor = Color.White;
        this.btnCopyLog.FlatStyle = FlatStyle.Flat;
        this.btnCopyLog.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
        this.btnCopyLog.Dock = DockStyle.Right;
        this.btnCopyLog.Width = 85;
        this.btnCopyLog.Cursor = Cursors.Hand;

        this.chkAutoScroll.Text = "Otomatik Kaydır";
        this.chkAutoScroll.Font = new Font("Segoe UI", 8.5F);
        this.chkAutoScroll.ForeColor = Color.FromArgb(71, 85, 105);
        this.chkAutoScroll.Checked = true;
        this.chkAutoScroll.Dock = DockStyle.Right;
        this.chkAutoScroll.Width = 125;

        this.pnlLogHeader.Controls.Add(this.lblLogTitle);
        this.pnlLogHeader.Controls.Add(this.chkAutoScroll);
        this.pnlLogHeader.Controls.Add(this.btnCopyLog);
        this.pnlLogHeader.Controls.Add(this.btnClearLog);

        this.rtbLog.Dock = DockStyle.Fill;
        this.rtbLog.Font = new Font("Cascadia Mono", 9.5F, FontStyle.Regular, GraphicsUnit.Point);
        this.rtbLog.BackColor = Color.FromArgb(15, 23, 42); // Modern Slate 900 dark theme
        this.rtbLog.ForeColor = Color.FromArgb(226, 232, 240);
        this.rtbLog.ReadOnly = true;
        this.rtbLog.BorderStyle = BorderStyle.None;
        this.rtbLog.WordWrap = false;

        this.pnlRightContainer.Controls.Add(this.rtbLog);
        this.pnlRightContainer.Controls.Add(this.pnlLogHeader);

        // ==========================================
        // Durum Çubuğu (StatusStrip)
        // ==========================================
        this.statusStrip.BackColor = Color.White;
        this.statusStrip.Font = new Font("Segoe UI", 9F);
        this.lblAboutStatus = new ToolStripStatusLabel();
        this.lblAboutStatus.Text = "ℹ️ Hakkında & Lisanslar";
        this.lblAboutStatus.IsLink = true;
        this.lblAboutStatus.LinkColor = Color.FromArgb(37, 99, 235);
        this.lblAboutStatus.ActiveLinkColor = Color.FromArgb(29, 78, 216);
        this.lblAboutStatus.Margin = new Padding(12, 3, 8, 2);

        this.statusStrip.Items.AddRange(new ToolStripItem[] {
            this.lblStatus,
            this.lblSpring,
            this.lblDuration,
            this.lblAboutStatus
        });
        this.lblStatus.Text = "Hazır";
        this.lblStatus.ForeColor = Color.FromArgb(30, 41, 59);
        this.lblSpring.Spring = true;
        this.lblDuration.Text = "";
        this.lblDuration.ForeColor = Color.FromArgb(100, 116, 139);

        // ==========================================
        // Form Kontrolleri Hiyerarşisi
        // Önemli: DockStyle.Fill olan kontroller en son veya doğru sırada eklenmelidir.
        // ==========================================
        this.Controls.Add(this.pnlRightContainer);
        this.Controls.Add(this.splitterMiddle);
        this.Controls.Add(this.pnlLeftContainer);
        this.Controls.Add(this.pnlHeader);
        this.Controls.Add(this.statusStrip);

        this.pnlHeader.ResumeLayout(false);
        this.pnlHeader.PerformLayout();
        this.pnlLeftContainer.ResumeLayout(false);
        this.grpServer.ResumeLayout(false);
        this.grpServer.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.numPort)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.numTimeout)).EndInit();
        this.grpAuth.ResumeLayout(false);
        this.grpAuth.PerformLayout();
        this.grpMail.ResumeLayout(false);
        this.grpMail.PerformLayout();
        this.pnlButtons.ResumeLayout(false);
        this.pnlRightContainer.ResumeLayout(false);
        this.pnlLogHeader.ResumeLayout(false);
        this.pnlLogHeader.PerformLayout();
        this.statusStrip.ResumeLayout(false);
        this.statusStrip.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    #endregion

    private Panel pnlHeader;
    private Panel pnlHeaderRight;
    private Label lblHeaderTitle;
    private Label lblHeaderSubtitle;
    private Button btnAbout;

    private Panel pnlLeftContainer;
    private Splitter splitterMiddle;
    private Panel pnlRightContainer;

    private GroupBox grpServer;
    private Label lblHost;
    private TextBox txtHost;
    private Label lblPort;
    private NumericUpDown numPort;
    private Label lblSsl;
    private ComboBox cmbSsl;
    private Label lblTimeout;
    private NumericUpDown numTimeout;
    private CheckBox chkIgnoreCert;

    private GroupBox grpAuth;
    private CheckBox chkUseAuth;
    private Label lblUsername;
    private TextBox txtUsername;
    private Label lblPassword;
    private TextBox txtPassword;
    private Button btnTogglePassword;

    private GroupBox grpMail;
    private Label lblFrom;
    private TextBox txtFromEmail;
    private Label lblFromName;
    private TextBox txtFromName;
    private Label lblTo;
    private TextBox txtToEmail;
    private Label lblSubject;
    private TextBox txtSubject;
    private Label lblBody;
    private TextBox txtBody;
    private CheckBox chkIsHtml;

    private Panel pnlButtons;
    private Button btnTestConnection;
    private Button btnSendMail;

    private Panel pnlLogHeader;
    private Label lblLogTitle;
    private CheckBox chkAutoScroll;
    private Button btnCopyLog;
    private Button btnClearLog;
    private RichTextBox rtbLog;

    private StatusStrip statusStrip;
    private ToolStripStatusLabel lblStatus;
    private ToolStripStatusLabel lblSpring;
    private ToolStripStatusLabel lblDuration;
    private ToolStripStatusLabel lblAboutStatus;
}
