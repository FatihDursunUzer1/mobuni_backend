using MobUni.ApplicationCore.Entities.ActivityAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.ApplicationCore.Filters
{
    public class ActivityFilter
    {
        public int? Id { get; set; }

        public int? UniversityId { get; set; }

        public int? CityId { get; set; }

        public string? UserId { get; set; }

        public int[]? Categories { get; set; }
       
    }
}
