using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.ApplicationCore.DTOs.Requests
{
    public class LoginUserDTO
    {
        public string? Email { get; set; }
        public string Password { get; set; }
        public string? UserName { get; set; }
    }
}
