using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobUni.ApplicationCore.Interfaces;

namespace MobUni.ApplicationCore.Entities.UserAggregate
{
   public class User:BaseEntity<string>,IAggregateRoot
    {
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; } = String.Empty;
        public string Name { get; set; }
        public string Surname { get; set; }
        public UserType UserType { get; set; }
        public string? Image { get; set; }=String.Empty;
       public int UniversityId { get; set; }
       public int DepartmentId { get; set; }

       public void CreateUserTime()
        {
            this.CreatedTime = DateTime.Now;
            this.UpdatedTime = DateTime.Now;
        }
       
    }
}
