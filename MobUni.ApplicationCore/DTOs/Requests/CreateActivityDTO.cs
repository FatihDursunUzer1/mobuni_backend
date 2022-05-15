using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.ApplicationCore.DTOs.Requests
{
    public class CreateActivityDTO: BaseCreateDTO<int>
    {
       // public UserDTO User { get; set; }
       public int Id { get; set; }
       public string UserId { get; set; }
        public string Text { get; set; }
        public string? Image { get; set; } = string.Empty;
        //public UniversityDTO University { get; set; }
        public int UniversityId { get; set; }
        public DateTime? ActivityStartTime { get; set; } = DateTime.Now;
        public DateTime? ActivityEndTime { get; set; } = DateTime.Now;

        public int CommentCount { get; set; }
        public int LikeCount { get; set; }
     
    }
}
