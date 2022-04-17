using System;
namespace MobUni.ApplicationCore.DTOs
{
	public class QuestionDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserDTO User { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int UniversityId { get; set; }
        public UniversityDTO University { get; set; }
        public int CommentCount { get; set; }
        public int LikeCount { get; set; }
    }
}

