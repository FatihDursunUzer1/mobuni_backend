using System;
namespace MobUni.ApplicationCore.DTOs
{
	public class QuestionDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int UniversityId { get; set; }
        public int CommentCount { get; set; }
        public int LikeCount { get; set; }
        public bool IsLiked { get; set; }
    
    }
}

