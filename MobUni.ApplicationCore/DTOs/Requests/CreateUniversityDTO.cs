using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.ApplicationCore.DTOs.Requests
{
    public class CreateUniversityDTO: BaseCreateDTO<int>
    {
        public string Name { get; set; }
        public string Province { get; set; }
        public string District { get; set; }

        public string? Logo { get; set; }
        public string? FoundationYear { get; set; } = String.Empty;
        public string? Description { get; set; } = String.Empty;
    }
}
