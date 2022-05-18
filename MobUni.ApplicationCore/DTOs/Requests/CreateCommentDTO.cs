using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.ApplicationCore.DTOs.Requests
{
    public class CreateCommentDTO:BaseCreateDTO<int>
    {
       // public UserDTO User { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }

        public int LikeCount { get; set; }
    }
}
