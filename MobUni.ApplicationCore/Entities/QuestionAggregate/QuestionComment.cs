using MobUni.ApplicationCore.Entities.ActivityAggregate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.ApplicationCore.Entities.QuestionAggregate
{
    public class QuestionComment:Comment
    {
        public int? QuestionId { get; set; }
        public virtual Question? Question { get; set; }

        public int? ActivityId { get; set; }

        public virtual Activity? Activity { get; set; }
    }
}
