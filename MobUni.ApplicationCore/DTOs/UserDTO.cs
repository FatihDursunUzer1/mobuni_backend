using System;
namespace MobUni.ApplicationCore.DTOs
{
	public class UserDTO
    {
        public string Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; } = String.Empty;
        public string? Name { get; set; }

        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        //public List<QuestionDTO> Questions { get; set; }
        public string? Surname { get; set; }
        // at mappingprofile to UserType
        public int? UserType { get; set; }
        public string? Image { get; set; } = String.Empty;
        //UniversityId foreignKey. UniversityId is nullable because each user not have University
        public int? UniversityId { get; set; }
        public UniversityDTO? University { get; set; }
        //University DepartmentId foreignKey. DepartmentId is nullable
        public int? DepartmentId { get; set; }
        public DepartmentDTO? Department { get; set; }
    }
}

