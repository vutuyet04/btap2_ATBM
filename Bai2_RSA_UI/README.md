# 🔐 Báo Cáo Bài 2: Mã Hóa Bất Đối Xứng RSA

**Học phần:** An Toàn & Bảo Mật Thông Tin  
**Sinh viên thực hiện:** Vũ Thị Ánh Tuyết  
**Mã sinh viên:** K235480106078  

---

## 📌 Giới thiệu
Đây là ứng dụng Desktop (Windows Forms) được phát triển trên nền tảng .NET (C#) nhằm mô phỏng và thử nghiệm thuật toán mã hóa bất đối xứng **RSA (2048-bit)**. 

## 🚀 Các yêu cầu đã hoàn thành
- [x] **Sinh và quản lý cặp khóa:** Tạo ngẫu nhiên cặp khóa Public Key (Khóa công khai) và Private Key (Khóa bí mật), lưu xuất ra 2 file độc lập (`rsa_public.txt`, `rsa_private.txt`) dưới định dạng Base64.
- [x] **Nạp khóa an toàn:** Đọc và import khóa từ file để sử dụng cho các phiên làm việc tiếp theo.
- [x] **Mã hóa (Encryption):** Sử dụng Public Key và chuẩn padding PKCS#1 để mã hóa văn bản gốc thành bản mã Base64.
- [x] **Giải mã (Decryption):** Sử dụng Private Key để khôi phục bản mã Base64 về văn bản gốc chính xác 100%.
- [x] **Giao diện trực quan (UI/UX):** Tích hợp bảng Log ghi nhận chi tiết thời gian thực (real-time) cho từng bước thao tác sinh khóa, mã hóa và giải mã.

Dưới đây là hình ảnh chạy thử nghiệm toàn bộ quy trình trên giao diện phần mềm:

*(Các file ảnh minh chứng được đính kèm trực tiếp trong thư mục dự án này)*

---
*Báo cáo được hoàn thiện và đẩy lên GitHub bằng Git CLI.*