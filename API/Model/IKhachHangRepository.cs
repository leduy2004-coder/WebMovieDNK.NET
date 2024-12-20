﻿using API.Data;
using API.Dto;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Model
{
    public interface IKhachHangRepository
    {
        Task<IEnumerable<tbKhachHang>> GetDanhSachKhachHang();
        Task<tbKhachHang> GetKhachHangById(string maKhachHang);
        Task<IEnumerable<tbKhachHang>> GetTimKH(string tenKH);
        Task<tbKhachHang> AddKhachHang(tbKhachHang kh);
        Task<tbKhachHang> UpdateKhachHang(tbKhachHang kh);
        Task<bool> DeleteKhachHang(string maKhachHang);
        Task<tbKhachHang> Login(string email, string password);
        Task<tbNhanVien> LoginAdmin(string email, string password);
        Task<bool> RegisterAsync(tbKhachHang khachHang);
        Task<IEnumerable<LichSuKhachHangDTO>> GetLSKhachHang(string maKH);
        Task <bool> SendVerificationEmail(string email);
        Task<bool> VerifyCodeAsync(string email, string inputCode);
    }
}
