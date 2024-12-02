using WEB.Models;

namespace WEB.Models
{
    public class Admin_NhanVienViewModel
    {
        public List<Admin_NhanVienModel> ListNhanVien { get; set; }

        public IEnumerable<Admin_NhanVienModel> DanhSachNhanVien { get; set; }
        public Admin_NhanVienModel NhanVienMoi { get; set; }
  

        public Admin_NhanVienViewModel()
        {
            DanhSachNhanVien = new List<Admin_NhanVienModel>();
            NhanVienMoi = new Admin_NhanVienModel();
          
        }
    }
}
