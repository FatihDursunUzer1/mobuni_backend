using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.ApplicationCore.Entities
{
    public class LikeQuestion:BaseEntity<int>
    {
        public string UserId { get; set; }
        public int QuestionId { get; set; }
        public bool IsActive { get; set; }
    }
}
