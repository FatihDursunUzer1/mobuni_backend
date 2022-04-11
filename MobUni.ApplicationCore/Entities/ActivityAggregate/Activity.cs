using MobUni.ApplicationCore.Entities.UserAggregate;
using MobUni.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.ApplicationCore.Entities.ActivityAggregate
{
    public class Activity:BaseEntity<int>,IAggregateRoot
    {
        public User User { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string? Image { get; set; } = string.Empty;
        public University University { get; set; }
        public DateTime? ActivityStartTime { get; set; } = DateTime.Now;
        public DateTime? ActivityEndTime { get; set; } = DateTime.Now;

        public int CommentCount { get; set; }
        public int LikeCount { get; set; }

    }
}
