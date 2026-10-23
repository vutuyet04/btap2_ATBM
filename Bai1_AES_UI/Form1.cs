using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace Bai1_AES_UI
{
    public partial class Form1 : Form
    {
        Aes myAes;
        TextBox txtInputText, txtOutputBase64;
        RichTextBox txtLog;
        string testFile = "test_100MB.dat";
        string encFile = "test_enc.dat";
        string decFile = "test_dec.dat";

        public Form1()
        {
            InitializeComponent();
            SetupUI();
        }

        private void SetupUI()
        {
            // Tăng kích thước Form và đổi màu nền sáng dịu
            this.Text = "BÀI 1: MÃ HÓA AES - Vũ Thị Ánh Tuyết";
            this.Size = new Size(750, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 248, 255); // Màu xanh nhạt AliceBlue
            this.Font = new Font("Segoe UI", 10);

            // Nhóm 1: Khóa AES (Màu xanh biển)
            GroupBox grpKey = new GroupBox { Text = "1. Sinh & Nạp Khóa AES", Bounds = new Rectangle(15, 15, 700, 80), ForeColor = Color.DarkBlue, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            Button btnGenKey = new Button { Text = "Sinh & Lưu Khóa", Bounds = new Rectangle(20, 30, 160, 35), BackColor = Color.LightSeaGreen, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10) };
            Button btnLoadKey = new Button { Text = "Nạp Khóa", Bounds = new Rectangle(200, 30, 160, 35), BackColor = Color.DodgerBlue, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10) };

            btnGenKey.Click += (s, e) => { AesHelper.GenerateAndSaveKey(); Log("Đã sinh và lưu khóa (aes_key.txt, aes_iv.txt).", Color.Cyan); };
            btnLoadKey.Click += (s, e) => { myAes = AesHelper.ImportKey(); Log("Đã nạp khóa AES sẵn sàng để mã hóa.", Color.Lime); };
            grpKey.Controls.AddRange(new Control[] { btnGenKey, btnLoadKey });

            // Nhóm 2: Mã hóa Text (Màu tím)
            GroupBox grpText = new GroupBox { Text = "2. Mã Hóa Văn Bản", Bounds = new Rectangle(15, 110, 700, 115), ForeColor = Color.DarkMagenta, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            txtInputText = new TextBox { Text = "Nhập văn bản cần mã hóa vào đây...", Bounds = new Rectangle(20, 30, 500, 30), Font = new Font("Segoe UI", 10) };
            Button btnEncText = new Button { Text = "Mã hóa Text", Bounds = new Rectangle(540, 29, 140, 32), BackColor = Color.MediumOrchid, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10) };
            txtOutputBase64 = new TextBox { Bounds = new Rectangle(20, 70, 660, 30), ReadOnly = true, Font = new Font("Segoe UI", 10), BackColor = Color.WhiteSmoke };

            btnEncText.Click += (s, e) => {
                if (myAes == null) { MessageBox.Show("Hãy nạp khóa trước!"); return; }
                txtOutputBase64.Text = AesHelper.EncryptText(txtInputText.Text, myAes);
                Log("Đã mã hóa văn bản ra chuỗi Base64.", Color.Fuchsia);
            };
            grpText.Controls.AddRange(new Control[] { txtInputText, btnEncText, txtOutputBase64 });

            // Nhóm 3: Mã hóa File lớn (Nhiều màu nổi bật)
            GroupBox grpFile = new GroupBox { Text = "3. Mã Hóa File Lớn (Thử nghiệm 100MB)", Bounds = new Rectangle(15, 240, 700, 85), ForeColor = Color.DarkRed, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            Button btnGenFile = new Button { Text = "Tạo file ảo 100MB", Bounds = new Rectangle(20, 30, 150, 35), BackColor = Color.DarkOrange, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10) };
            Button btnEncFile = new Button { Text = "Mã hóa File", Bounds = new Rectangle(190, 30, 140, 35), BackColor = Color.Crimson, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10) };
            Button btnDecFile = new Button { Text = "Giải mã File", Bounds = new Rectangle(350, 30, 140, 35), BackColor = Color.ForestGreen, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10) };
            Button btnCompare = new Button { Text = "So sánh Bytes", Bounds = new Rectangle(510, 30, 170, 35), BackColor = Color.Teal, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10) };

            btnGenFile.Click += (s, e) => {
                byte[] d = new byte[1024 * 1024];
                using (FileStream fs = new FileStream(testFile, FileMode.Create))
                {
                    for (int i = 0; i < 100; i++) fs.Write(d, 0, d.Length);
                }
                Log("Tạo xong file ảo test_100MB.dat để thử nghiệm.", Color.Yellow);
            };

            btnEncFile.Click += (s, e) => {
                if (myAes == null) { MessageBox.Show("Nạp khóa đã!"); return; }
                if (!File.Exists(testFile)) { MessageBox.Show("Hãy bấm 'Tạo file ảo 100MB' trước để có file nguồn!"); return; }
                Stopwatch sw = Stopwatch.StartNew();
                AesHelper.EncryptLargeFile(testFile, encFile, myAes);
                sw.Stop();
                Log($"Mã hóa file thành công! Mất: {sw.ElapsedMilliseconds} ms", Color.Tomato);
            };

            btnDecFile.Click += (s, e) => {
                if (myAes == null) return;
                Stopwatch sw = Stopwatch.StartNew();
                AesHelper.DecryptLargeFile(encFile, decFile, myAes);
                sw.Stop();
                Log($"Giải mã file thành công! Mất: {sw.ElapsedMilliseconds} ms", Color.LightGreen);
            };

            btnCompare.Click += (s, e) => {
                bool match = AesHelper.CompareFiles(testFile, decFile);
                if (match) Log("=> KẾT LUẬN: File nguồn và giải mã khớp 100%!", Color.SpringGreen);
                else Log("=> KẾT LUẬN: Lỗi! 2 file không khớp.", Color.Red);
            };

            grpFile.Controls.AddRange(new Control[] { btnGenFile, btnEncFile, btnDecFile, btnCompare });

            // Hộp thoại Log báo cáo (Font chữ hacker)
            txtLog = new RichTextBox { Bounds = new Rectangle(15, 345, 700, 240), ReadOnly = true, BackColor = Color.FromArgb(30, 30, 30), ForeColor = Color.White, Font = new Font("Consolas", 11) };

            this.Controls.Add(grpKey);
            this.Controls.Add(grpText);
            this.Controls.Add(grpFile);
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