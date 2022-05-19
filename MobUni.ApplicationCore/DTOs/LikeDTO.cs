using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.ApplicationCore.DTOs
{
    public class LikeDTO
    {
        public virtual UserDTO User { get; set; }
        public string UserId { get; set; }
        public int? QuestionId { get; set; }
        public virtual QuestionDTO? Question { get; set; }
        public int? QuestionCommentId { get; set; }
        public virtual QuestionCommentDTO? QuestionComment { get; set; }
        public bool IsActive { get; set; }
    }
}
