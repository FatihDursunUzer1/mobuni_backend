using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.ApplicationCore.DTOs
{
   public class ActivityParticipantDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        public UserDTO User { get; set; }
        public string ActivityId { get; set; }

        public ActivityDTO Activity { get; set; }
        public bool IsActive { get; set; }
        public bool? IsApproved { get; set; }
        public bool IsJoined { get; set; }

        public DateTime? CreatedTime { get; set;}
        public DateTime? UpdatedTime { get; set;}

    }
}
