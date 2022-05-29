using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobUni.ApplicationCore.Entities.QuestionAggregate;
using MobUni.ApplicationCore.Interfaces;

namespace MobUni.ApplicationCore.Entities.UserAggregate
{
   public class User:BaseEntity,IAggregateRoot
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; } = String.Empty;
        public string Name { get; set; }
        public string Surname { get; set; }
        public UserType UserType { get; set; }
        public string? Image { get; set; }=String.Empty;
       public int? UniversityId { get; set; }
        public virtual University? University { get; set; }
       public int? DepartmentId { get; set; }
        public virtual Department? Department { get; set; }

        public virtual bool IsUniversityStudent { get; set; } = true;
        public virtual ICollection<Question> Questions { get; set; }
       public void CreateUserTime()
        {
            this.CreatedTime =  DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
            this.UpdatedTime =  DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
        }
       
    }
}
