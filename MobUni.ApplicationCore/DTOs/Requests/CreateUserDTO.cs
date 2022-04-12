using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.ApplicationCore.DTOs.Requests
{
    public class CreateUserDTO
    {
        public string PasswordHash { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; } = String.Empty;
        public string Name { get; set; }
        public string Surname { get; set; }
        // at mappingprofile to UserType
        public int UserType { get; set; }
        public string? Image { get; set; } = String.Empty;
       public int UniversityId { get; set; }
       public int DepartmentId { get; set; }
    }
}
