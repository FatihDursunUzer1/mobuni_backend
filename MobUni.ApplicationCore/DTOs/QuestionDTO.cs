using System;
namespace MobUni.ApplicationCore.DTOs
{
	public class QuestionDTO
	{
        public User User { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public University University { get; set; }
        public int CommentCount { get; set; }
        public int LikeCount { get; set; }
    }
}

