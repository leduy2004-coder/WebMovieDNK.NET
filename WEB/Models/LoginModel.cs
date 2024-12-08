using System;
using System.ComponentModel.DataAnnotations;

namespace WEB.Models
{
    public class LoginModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string TenTK { get; set; }
        public string HoTen { get; set; }
        public string Role { get; set; }

    }
}
