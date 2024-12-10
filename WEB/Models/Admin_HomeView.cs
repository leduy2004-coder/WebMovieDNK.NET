using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WEB.Models
{
    public class Admin_HomeView
    {

        public int TongVeTrongNam { get; set; }
        public List<int> VeBanTungThang { get; set; }
        public double TongTienTheoNam { get; set; }
        public int SoLuongPhimDaChieuTrongNam { get; set; }
        public List<TopCustomerDTO> topCustomerDTOs { get; set; }
    }

    public class TopCustomerDTO
    {
        public string TenKhachHang { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public int TongSoVeMua { get; set; }
        public double TongTien { get; set; }
    }
}
