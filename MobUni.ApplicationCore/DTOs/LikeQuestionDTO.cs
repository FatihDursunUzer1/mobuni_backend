using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.ApplicationCore.DTOs
{
    public class LikeQuestionDTO
    {
        public UserDTO User { get; set; }
        public string UserId { get; set; }
        public int QuestionId { get; set; }
        public QuestionDTO Question { get; set; }
        public bool IsActive { get; set; }
    }
}
