using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.ApplicationCore.Entities
{
    public class Token
    {
        public string AccessToken { get; set; }
        public string TokenType { get; set; } = "bearer";
        public DateTime? ExpiresIn { get; set; }
    }
}
