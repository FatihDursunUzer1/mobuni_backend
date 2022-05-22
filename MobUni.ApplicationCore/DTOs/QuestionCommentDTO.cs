using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.ApplicationCore.DTOs
{
    public class QuestionCommentDTO:CommentDTO
    {
       public bool IsLiked { get; set; }
    }
}
