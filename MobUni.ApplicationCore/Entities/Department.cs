using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.ApplicationCore.Entities
{
    public class Department:BaseEntity<int>
    {
        public string Name { get; set; }
    }
}
