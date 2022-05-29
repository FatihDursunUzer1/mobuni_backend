using MobUni.ApplicationCore.Entities.UserAggregate;
using MobUni.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.ApplicationCore.Entities.QuestionAggregate
{
    public class Question:BaseEntity,IAggregateRoot
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public string Text { get; set; }
        public string? Image { get; set; }
        public int UniversityId { get; set; }
        public virtual University University { get; set; }

        public int? DepartmentId { get; set; }
        public virtual Department? Department { get; set; }
        public int CommentCount { get; set; }
        public int LikeCount { get; set; }
        
        public bool IsUniversityStudent { get; set; }
    }
}
