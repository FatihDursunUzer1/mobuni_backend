using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.ApplicationCore.DTOs
{
    public class TokenDTO
    {
        public string AccessToken { get; set; }
        public string TokenType { get; set; } = "bearer";
        public DateTime? ExpiresIn { get; set; }

        public UserDTO User { get; set; }
    }
}
