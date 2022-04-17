using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobUni.ApplicationCore.Interfaces;

namespace MobUni.ApplicationCore.Entities.UserAggregate
{
   public class User:BaseEntity<string>,IAggregateRoot
    {
        public string PasswordHash { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; } = String.Empty;
        public string Name { get; set; }
        public string Surname { get; set; }
        public UserType UserType { get; set; }
        public string? Image { get; set; }=String.Empty;
        //UniversityId foreignKey. UniversityId is nullable because each user not have University
        public virtual University? University { get;  set; }
        public int UniversityId { get; set; }
        //University DepartmentId foreignKey. DepartmentId is nullable
        public virtual Department? Department { get;  set; }
        public int DepartmentId { get; set; }

       /* public void setUniversityInfo(int UniversityId, int DepartmentId)
        {
            this.UniversityId= UniversityId;
            this.DepartmentId= DepartmentId;
        } */

       
    }
}
