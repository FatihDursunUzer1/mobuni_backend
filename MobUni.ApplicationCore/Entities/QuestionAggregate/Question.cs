using MobUni.ApplicationCore.Entities.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.ApplicationCore.Entities.QuestionAggregate
{
    public class Question:BaseEntity<int>
    {
        public User User { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public University University { get; set; }
        public int CommentCount { get; set; }
        public int LikeCount { get; set; }
    }
}
