﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.ApplicationCore.Entities.QuestionAggregate
{
    public class QuestionComment:Comment
    {
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }
    }
}
