# 🛡️ Báo Cáo Bài 1: Mã Hóa Đối Xứng AES

**Học phần:** An Toàn & Bảo Mật Thông Tin  
**Sinh viên thực hiện:** Vũ Thị Ánh Tuyết  
**Mã sinh viên:** K235480106078  

---

## 📌 Giới thiệu dự án
Đây là ứng dụng Desktop (Windows Forms) được phát triển trên nền tảng .NET (C#) nhằm mô phỏng và thử nghiệm thuật toán mã hóa đối xứng **AES (256-bit)**. Ứng dụng giải quyết triệt để bài toán xử lý file kích thước lớn (hàng GB) mà không gây tràn bộ nhớ (RAM) bằng kỹ thuật luân chuyển luồng dữ liệu (Stream).

## 🚀 Các yêu cầu đã hoàn thành
- [x] **Sinh và quản lý khóa:** Khởi tạo khóa AES 256-bit ngẫu nhiên và lưu ra file (`aes_key.txt`, `aes_iv.txt`) dưới định dạng Base64. Import khóa thành công từ file để tái sử dụng.
- [x] **Mã hóa văn bản (Text):** Nhận đầu vào là chuỗi văn bản, tiến hành mã hóa và xuất kết quả mã hóa (Base64) trực tiếp ra màn hình.
- [x] **Xử lý File lớn (I/O Stream):** 
  - Tạo file ảo dung lượng lớn (100MB - hàng GB) để thử nghiệm.
  - Sử dụng `FileStream` kết hợp `CryptoStream` để đọc/ghi file theo từng block (Buffer), giúp mã hóa và giải mã mượt mà không làm đơ máy.
- [x] **Đánh giá hiệu năng:** Tích hợp `Stopwatch` để đo lường chính xác thời gian (mili-giây) thực thi của quá trình mã hóa và giải mã.
- [x] **Kiểm định tính toàn vẹn:** Thuật toán so sánh từng byte (Compare theo bytes) giữa file gốc và file sau khi giải mã. Kết luận: Hai file khớp nhau 100%.

Dưới đây là các hình ảnh chạy thử nghiệm toàn bộ quy trình trên giao diện phần mềm:

*(Xem các file ảnh đính kèm trong kho lưu trữ: `1.png`, `test2.png`, `test3.png`)*

---
*Báo cáo được hoàn thiện và đẩy lên GitHub bằng Git CLI.*