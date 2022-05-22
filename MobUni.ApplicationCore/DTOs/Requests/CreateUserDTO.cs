using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.ApplicationCore.DTOs.Requests
{
    public class CreateUserDTO: BaseCreateDTO<string>
    {
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int UserType { get; set; }
        public string? Image { get; set; }
       public int UniversityId { get; set; }
       public int DepartmentId { get; set; }
    }
}
