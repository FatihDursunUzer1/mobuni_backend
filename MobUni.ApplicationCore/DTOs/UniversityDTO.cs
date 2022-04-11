using System;
namespace MobUni.ApplicationCore.DTOs
{
	public class UniversityDTO
	{
        public string Name { get; set; }
        public string Province { get; set; }
        public string District { get; set; }

        public string? Logo { get; set; }
        public string? FoundationYear { get; set; } = String.Empty;
        public string? Description { get; set; } = String.Empty;
    }
}

