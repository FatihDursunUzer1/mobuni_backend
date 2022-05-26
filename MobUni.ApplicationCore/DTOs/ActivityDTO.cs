using MobUni.ApplicationCore.Entities.ActivityAggregate;
using Newtonsoft.Json.Converters;
using System;
using Newtonsoft.Json;


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
        public DateTime? ActivityStartTime { get; set; }
        public DateTime? ActivityEndTime { get; set; }
        public int CommentCount { get; set; }
        public int JoinedCount { get; set; }

        public int MaxUser { get; set; }

        public int TicketPrice { get; set; }

        public bool IsActive { get; set; }
        public bool IsExternal { get; set; }
        public bool Timeout { get; set; }
        public int[]? ActivityCategories { get; set; }

        public bool IsJoined { get; set; }

    }
}

