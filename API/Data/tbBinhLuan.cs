using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Data
{
    public class tbBinhLuan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  // Tạo giá trị tự động cho MaBinhLuan
        public int MaBinhLuan { get; set; }

        [Required]  // Bắt buộc phải có
        [StringLength(20)]  // Giới hạn độ dài là 20 ký tự
        public string MaPhim { get; set; }

        [Required]  // Bắt buộc phải có
        [StringLength(20)]  // Giới hạn độ dài là 20 ký tự
        public string MaKH { get; set; }

        [Required]  // Bắt buộc phải có
        public DateTime Gio { get; set; }  // Thời gian của bình luận

        [Required]  // Bắt buộc phải có
        public string NoiDung { get; set; }  // Nội dung bình luận

        public bool TinhTrang { get; set; } = true;  // Mặc định là true (tương ứng với 1)

        // Khóa ngoại - Mối quan hệ với Phim
        [ForeignKey("MaPhim")]  // Định nghĩa khóa ngoại liên kết với bảng Phim
        public virtual tbPhim Phim { get; set; }  // Đối tượng Phim liên kết

        // Khóa ngoại - Mối quan hệ với KhachHang
        [ForeignKey("MaKH")]  // Định nghĩa khóa ngoại liên kết với bảng KhachHang
        public virtual tbKhachHang KhachHang { get; set; }  // Đối tượng KhachHang liên kết
    }
}
