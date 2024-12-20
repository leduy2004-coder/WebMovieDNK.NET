﻿namespace API.Dto
{

    public class ThongKeVe
    {
        public int TongSoVe { get; set; }
    }

    public class ThongKeDTO
    {
        public int TongVeTrongNam { get; set; }
        public List<int> VeBanTungThang { get; set; }
        public double TongTienTheoNam { get; set; }
        public int SoLuongPhimDaChieuTrongNam { get; set; }
        public List<TopCustomerDTO> topCustomerDTOs { get; set; }
    }

    public class DoanhThuTrongNamDTO
    {
        public double TongTienTrongNam { get; set; }
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
