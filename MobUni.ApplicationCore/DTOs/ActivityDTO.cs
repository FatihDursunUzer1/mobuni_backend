using System;
namespace MobUni.ApplicationCore.DTOs
{
	public class ActivityDTO
	{
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Text { get; set; }
        public string? Image { get; set; } = string.Empty;
        public int UniversityId { get; set; }
        public DateTime? ActivityStartTime { get; set; } = DateTime.Now;
        public DateTime? ActivityEndTime { get; set; } = DateTime.Now;
        public int CommentCount { get; set; }
        public int LikeCount { get; set; }

    }
}

