using MobUni.ApplicationCore.Entities.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.ApplicationCore.Entities
{
    public class Comment:BaseEntity<int>
    {
        public User User { get; set; }
        public int TableId { get; set; }
        public int TableType { get; set; }
        public string Content { get; set; }
        public int LikeCount { get; set; }
    }
}
