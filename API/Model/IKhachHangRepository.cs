﻿using API.Data;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Model
{
    public interface IKhachHangRepository
    {
        Task<IEnumerable<tbKhachHang>> GetDanhSachKhachHang();
        Task<tbKhachHang> GetKhachHangById(string maKhachHang);
        Task<tbKhachHang> AddKhachHang(tbKhachHang kh);
        Task<tbKhachHang> UpdateKhachHang(tbKhachHang kh);
        Task<bool> DeleteKhachHang(string maKhachHang);
    }
}
