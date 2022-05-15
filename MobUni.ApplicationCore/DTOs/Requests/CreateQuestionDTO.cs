﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.ApplicationCore.DTOs.Requests
{
    public class CreateQuestionDTO: BaseCreateDTO<int>
    {
       // public UserDTO User { get; set; }
       public string UserId { get; set; }
        public string Text { get; set; }
       // public UniversityDTO University { get; set; }
       public int UniversityId { get; set; }
        public int CommentCount { get; set; }
        public int LikeCount { get; set; }
      
    }
}
