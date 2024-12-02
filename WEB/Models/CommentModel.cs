using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WEB.Models
{
    public class CommentModel
    {

        public int MaBinhLuan { get; set; }

        public string MaPhim { get; set; }

        public string maKH { get; set; }
        public CustomerModel KhachHang { get; set; }

        public DateTime Gio { get; set; }  

        public string NoiDung { get; set; }  

        public bool TinhTrang { get; set; } = true;  

    }
}
