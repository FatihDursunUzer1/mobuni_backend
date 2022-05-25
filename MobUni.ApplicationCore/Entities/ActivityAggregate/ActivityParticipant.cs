using MobUni.ApplicationCore.Entities.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.ApplicationCore.Entities.ActivityAggregate
{
    public class ActivityParticipant:BaseEntity
    {

        public int Id { get; set; }
        public string UserId { get; set; }

        public virtual User User { get; set; }
        public int ActivityId { get; set; }
        public virtual Activity Activity { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsApproved { get; set; } = true;
        public bool IsJoined { get; set; } = true;

    }
}
