using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.ApplicationCore.Entities.ActivityAggregate
{
    public class ActivityComment:Comment
    {
        public int ActivityId { get; set; }
        public virtual Activity Activity { get; set; }
        
    }
}
