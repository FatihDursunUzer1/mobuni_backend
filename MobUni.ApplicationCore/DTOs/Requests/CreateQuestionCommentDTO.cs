using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.ApplicationCore.DTOs.Requests
{
    public class CreateQuestionCommentDTO:CreateCommentDTO
    {
        public int QuestionId { get; set; }
    }
}
