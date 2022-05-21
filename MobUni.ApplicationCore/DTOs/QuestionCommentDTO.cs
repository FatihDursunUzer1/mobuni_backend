using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.ApplicationCore.DTOs
{
    public class QuestionCommentDTO:CommentDTO
    {
       //public int QuestionId { get; set; }
       //public QuestionDTO Question { get; set; }
       public bool IsLiked { get; set; }
    }
}
