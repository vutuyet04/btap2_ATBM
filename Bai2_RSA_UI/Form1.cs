using System;
using System.Drawing;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace Bai2_RSA_UI
{
    public partial class Form1 : Form
    {
        RSA publicKey;
        RSA privateKey;
        TextBox txtInput, txtEncrypted, txtDecrypted;
        RichTextBox txtLog;

        public Form1()
        {
            InitializeComponent();
            SetupUI();
        }

        private void SetupUI()
        {
            this.Text = "BÀI 2: MÃ HÓA RSA - Vũ Thị Ánh Tuyết (K235480106078)";
            this.Size = new Size(750, 680);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(245, 245, 255);
            this.Font = new Font("Segoe UI", 10);

            // Nhóm 1: Quản lý Khóa RSA (Màu cam)
            GroupBox grpKey = new GroupBox { Text = "1. Cặp Khóa RSA (Công khai & Bí mật)", Bounds = new Rectangle(15, 15, 700, 80), ForeColor = Color.SaddleBrown, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            Button btnGenKey = new Button { Text = "Sinh & Lưu 2 Khóa", Bounds = new Rectangle(20, 30, 160, 35), BackColor = Color.DarkOrange, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            Button btnLoadPub = new Button { Text = "Nạp Khóa Công Khai", Bounds = new Rectangle(200, 30, 170, 35), BackColor = Color.SteelBlue, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            Button btnLoadPriv = new Button { Text = "Nạp Khóa Bí Mật", Bounds = new Rectangle(390, 30, 170, 35), BackColor = Color.Crimson, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };

            btnGenKey.Click += (s, e) => { RsaHelper.GenerateAndSaveKeys(); Log("Đã sinh 2 file: rsa_public.txt và rsa_private.txt", Color.Cyan); };
            btnLoadPub.Click += (s, e) => { publicKey = RsaHelper.ImportPublicKey(); Log("Đã nạp Khóa Công Khai (Dùng để mã hóa).", Color.Lime); };
            btnLoadPriv.Click += (s, e) => { privateKey = RsaHelper.ImportPrivateKey(); Log("Đã nạp Khóa Bí Mật (Dùng để giải mã).", Color.Yellow); };
            grpKey.Controls.AddRange(new Control[] { btnGenKey, btnLoadPub, btnLoadPriv });

            // Nhóm 2: Mã hóa & Giải mã Text (Màu tím)
            GroupBox grpText = new GroupBox { Text = "2. Mã Hóa & Giải Mã Văn Bản", Bounds = new Rectangle(15, 110, 700, 240), ForeColor = Color.DarkMagenta, Font = new Font("Segoe UI", 10, FontStyle.Bold) };

            Label lbl1 = new Label { Text = "Văn bản gốc:", Bounds = new Rectangle(20, 30, 100, 25) };
            txtInput = new TextBox { Text = "He thong thi trac nghiem sinh vien...", Bounds = new Rectangle(20, 55, 500, 30), Font = new Font("Segoe UI", 10) };
            Button btnEnc = new Button { Text = "Mã hóa (RSA)", Bounds = new Rectangle(540, 54, 140, 32), BackColor = Color.MediumOrchid, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };

            Label lbl2 = new Label { Text = "Bản mã (Base64):", Bounds = new Rectangle(20, 95, 150, 25) };
            txtEncrypted = new TextBox { Bounds = new Rectangle(20, 120, 500, 30), ReadOnly = true, BackColor = Color.WhiteSmoke };
            Button btnDec = new Button { Text = "Giải mã (RSA)", Bounds = new Rectangle(540, 119, 140, 32), BackColor = Color.MediumSeaGreen, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };

            Label lbl3 = new Label { Text = "Kết quả giải mã:", Bounds = new Rectangle(20, 160, 150, 25) };
            txtDecrypted = new TextBox { Bounds = new Rectangle(20, 185, 660, 30), ReadOnly = true, BackColor = Color.WhiteSmoke, ForeColor = Color.Blue };

            btnEnc.Click += (s, e) => {
                if (publicKey == null) { MessageBox.Show("Vui lòng nạp Khóa Công Khai trước!"); return; }
                txtEncrypted.Text = RsaHelper.EncryptText(txtInput.Text, publicKey);
                Log("Đã mã hóa văn bản bằng Khóa Công Khai.", Color.Fuchsia);
            };

            btnDec.Click += (s, e) => {
                if (privateKey == null) { MessageBox.Show("Vui lòng nạp Khóa Bí Mật trước!"); return; }
                if (string.IsNullOrEmpty(txtEncrypted.Text)) return;
                txtDecrypted.Text = RsaHelper.DecryptText(txtEncrypted.Text, privateKey);
                Log("Đã giải mã thành công bằng Khóa Bí Mật.", Color.SpringGreen);
            };

            grpText.Controls.AddRange(new Control[] { lbl1, txtInput, btnEnc, lbl2, txtEncrypted, btnDec, lbl3, txtDecrypted });

            // Nhóm 3: Bảng Log báo cáo
            txtLog = new RichTextBox { Bounds = new Rectangle(15, 365, 700, 250), ReadOnly = true, BackColor = Color.FromArgb(30, 30, 30), ForeColor = Color.White, Font = new Font("Consolas", 11) };

            this.Controls.Add(grpKey);
            this.Controls.Add(grpText);
            this.Controls.Add(txtLog);
        }

        private void Log(string msg, Color color)
        {
            txtLog.SelectionStart = txtLog.TextLength;
            txtLog.SelectionLength = 0;
            txtLog.SelectionColor = color;
            txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {msg}\n");
            txtLog.ScrollToCaret();
        }
    }
}