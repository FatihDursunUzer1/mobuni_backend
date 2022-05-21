using System;
namespace MobUni.ApplicationCore.DTOs
{
	public class ActivityDTO
	{
        public int Id { get; set; }
        public UserDTO User { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public string? Image { get; set; } = string.Empty;
        public int UniversityId { get; set; }
        public UniversityDTO University { get; set; }

        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public DateTime? ActivityStartTime { get; set; } = DateTime.Now;
        public DateTime? ActivityEndTime { get; set; } = DateTime.Now;
        public int CommentCount { get; set; }
        public int LikeCount { get; set; }

    }
}

