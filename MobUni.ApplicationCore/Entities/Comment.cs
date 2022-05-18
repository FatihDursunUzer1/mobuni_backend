using MobUni.ApplicationCore.Entities.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.ApplicationCore.Entities
{
    public abstract class Comment:BaseEntity<int>
    {
        public virtual User User { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
        public int LikeCount { get; set; }
    }
}
