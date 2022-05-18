using System;
namespace MobUni.ApplicationCore.DTOs
{
	public abstract class CommentDTO
	{
        public int Id { get; set; }
        public string UserId { get; set; }
        public UserDTO User { get; set; }
        public string Content { get; set; }
        public int LikeCount { get; set; }
    }
}

