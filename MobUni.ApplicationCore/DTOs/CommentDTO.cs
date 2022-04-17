using System;
namespace MobUni.ApplicationCore.DTOs
{
	public class CommentDTO
	{
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserDTO User { get; set; }
        public int TableId { get; set; }
        public int TableType { get; set; }
        public string Content { get; set; }
        public int LikeCount { get; set; }
    }
}

