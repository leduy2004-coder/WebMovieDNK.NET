using System;
using System.ComponentModel.DataAnnotations;

namespace API.Dto
{
    public class LoginDTO
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string TenTK { get; set; }
        public string HoTen { get; set; }
        public string Role { get; set; }

    }
}
