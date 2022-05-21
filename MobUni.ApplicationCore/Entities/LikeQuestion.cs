using MobUni.ApplicationCore.Entities.QuestionAggregate;
using MobUni.ApplicationCore.Entities.UserAggregate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.ApplicationCore.Entities
{
    public class LikeQuestion : BaseEntity

    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public virtual User User { get; set; }
        public string UserId { get; set; }
        public int? QuestionId { get; set; }
        public virtual Question? Question { get; set; }
        public int? QuestionCommentId { get; set; }
        public virtual QuestionComment? QuestionComment {get;set;} 
        public bool IsActive { get; set; }
    }
}
