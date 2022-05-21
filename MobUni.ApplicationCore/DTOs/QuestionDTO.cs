using System;
namespace MobUni.ApplicationCore.DTOs
{
	public class QuestionDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public UserDTO User { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public int UniversityId { get; set; }
        public UniversityDTO University { get; set; }
        public int CommentCount { get; set; }
        public int LikeCount { get; set; }
        public bool IsLiked { get; set; }
    
    }
}

