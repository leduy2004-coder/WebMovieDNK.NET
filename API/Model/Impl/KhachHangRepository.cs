using API.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using API.Dto;
using Microsoft.Extensions.Caching.Memory;
using System.Net.Mail;
using System.Net;

namespace API.Model.Impl
{

    public class KhachHangRepository : IKhachHangRepository
    {
        private readonly AppDbContext _context;
        private readonly IMemoryCache _cache;
        public KhachHangRepository(AppDbContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _cache = memoryCache;
        }




        // Thêm mới một khách hàng
        public async Task<tbKhachHang> AddKhachHang(tbKhachHang kh)
        {
            kh.MaKH = "";
            _context.KhachHang.Add(kh); // Thêm khách hàng vào DbSet KhachHang
            await _context.SaveChangesAsync(); // Lưu thay đổi vào cơ sở dữ liệu
            return kh; // Trả về khách hàng đã thêm
        }

        // Xóa khách hàng dựa trên mã khách hàng
        public async Task<bool> DeleteKhachHang(string maKhachHang)
        {
            var khachHang = await _context.KhachHang.FindAsync(maKhachHang);
            if (khachHang == null) return false;

            khachHang.TinhTrang = false;
            _context.KhachHang.Update(khachHang);
            await _context.SaveChangesAsync();
            return true;
        }


        // Lấy danh sách tất cả khách hàng
        public async Task<IEnumerable<tbKhachHang>> GetDanhSachKhachHang()
        {
            return await _context.KhachHang
                                 .Where(kh => kh.TinhTrang == true)
                                 .ToListAsync();
        }


        // Lấy thông tin khách hàng theo mã
        public async Task<tbKhachHang> GetKhachHangById(string maKhachHang)
        {
            var khachHang = await _context.KhachHang
                .FirstOrDefaultAsync(nv => nv.MaKH == maKhachHang);  // Tìm nhân viên theo mã
            return khachHang; // Tìm khách hàng theo mã và trả về
        }

        // Cập nhật thông tin khách hàng
        public async Task<tbKhachHang> UpdateKhachHang(tbKhachHang kh)
        {
            var existingKhachHang = await _context.KhachHang.FindAsync(kh.MaKH); // Tìm khách hàng theo mã
            if (existingKhachHang == null) return null; // Nếu không tìm thấy, trả về null

            // Cập nhật các thuộc tính của khách hàng
            existingKhachHang.HoTen = kh.HoTen;
            existingKhachHang.Sdt = kh.Sdt;
            existingKhachHang.NgaySinh = kh.NgaySinh;
            existingKhachHang.Email = kh.Email;
            existingKhachHang.TinhTrang = kh.TinhTrang;
            existingKhachHang.TenTK = kh.TenTK;
            existingKhachHang.MatKhau = kh.MatKhau;


            await _context.SaveChangesAsync(); // Lưu thay đổi vào cơ sở dữ liệu
            return existingKhachHang; // Trả về khách hàng đã cập nhật
        }

        [HttpGet("LichSu/{maKH}")]
        public async Task<IEnumerable<LichSuKhachHangDTO>> GetLSKhachHang(string maKH)
        {


            var result = await _context.LichSuKH
                .FromSqlInterpolated($"SELECT * FROM dbo.fLichSuKH({maKH})")
                .ToListAsync();
            return result;
        }

        public async Task<tbKhachHang> Login(string email, string plainPassword)
        {
            // Tìm khách hàng dựa trên TenTK
            var khachHang = await _context.KhachHang
                .FirstOrDefaultAsync(kh => kh.TenTK == email);

            if (khachHang == null)
                return null; // Không tìm thấy người dùng

            // So sánh mật khẩu băm
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(plainPassword, khachHang.MatKhau);

            if (!isPasswordValid)
                return null; // Sai mật khẩu

            return khachHang; // Trả về thông tin khách hàng nếu đăng nhập thành công
        }
        public async Task<tbNhanVien> LoginAdmin(string email, string plainPassword)
        {
            var nhanVien = await _context.NhanVien
                .FirstOrDefaultAsync(nv => nv.TenTK == email && nv.MatKhau == plainPassword && nv.MaRole == 2);

            if (nhanVien == null)
                return null; // Không tìm thấy người dùng

            return nhanVien;
        }
        public async Task<bool> RegisterAsync(tbKhachHang khachHang)
        {
            // Kiểm tra xem tài khoản hoặc email đã tồn tại
            var existingAccount = await _context.Set<tbKhachHang>()
                .FirstOrDefaultAsync(x => x.TenTK == khachHang.TenTK || x.Email == khachHang.Email);

            if (existingAccount != null)
            {
                return false; // Tài khoản hoặc email đã tồn tại
            }

            // Băm mật khẩu bằng BCrypt
            khachHang.MatKhau = BCrypt.Net.BCrypt.HashPassword(khachHang.MatKhau);

            // Thêm tài khoản mới vào cơ sở dữ liệu
            await _context.Set<tbKhachHang>().AddAsync(khachHang);
            await _context.SaveChangesAsync();

            return true; // Đăng ký thành công
        }

        public async Task<bool> SendVerificationEmail(string email)
        {

            // Tạo mã xác nhận
            var verificationCode = new Random().Next(100000, 999999).ToString();

            // Lưu mã xác nhận vào cache (thời hạn 5 phút)
            _cache.Set(email, verificationCode, TimeSpan.FromMinutes(5));

            // Tạo nội dung email
            var message = new MailMessage
            {
                From = new MailAddress("appmoviednk@gmail.com", "Your App Name"),
                Subject = "Mã xác nhận đăng ký",
                Body = $"Cảm ơn bạn đã đăng ký tài khoản. Mã xác nhận của bạn là: {verificationCode}",
                IsBodyHtml = false
            };

            message.To.Add(email);

            // Cấu hình SMTP client
            using var smtpClient = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential("appmoviednk@gmail.com", "vmqpxvjghtsuygfs")
            };

            try
            {
                // Gửi email
                smtpClient.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Không thể gửi email xác nhận.", ex);
                return false;
            }

        }

        public async Task<bool> VerifyCodeAsync(string email, string inputCode)
        {
            // Lấy mã xác nhận từ cache
            if (_cache.TryGetValue(email, out string cachedCode))
            {
                if (cachedCode == inputCode)
                {
                    // Xóa mã xác nhận khỏi cache
                    _cache.Remove(email);

                    return true; // Đăng ký thành công
                }
            }
            return false; // Mã xác nhận không đúng hoặc đã hết hạn
        }

        public async Task<IEnumerable<tbKhachHang>> GetTimKH(string tenKH)
        {
            if (string.IsNullOrEmpty(tenKH))
            {
                return Enumerable.Empty<tbKhachHang>();
            }

            var result = await _context.KhachHang
                                       .Where(p => p.HoTen.Contains(tenKH))
                                       .ToListAsync();

            return result;
        }
    }
}
