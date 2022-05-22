using MobUni.ApplicationCore.Entities.UserAggregate;
using MobUni.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.ApplicationCore.Entities.ActivityAggregate
{
    public class Activity:BaseEntity,IAggregateRoot
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public string? Content { get; set; }
        public string? Title { get; set; }
        public string? Image { get; set; } = string.Empty;
        public int UniversityId { get; set; }
        public virtual University University { get; set; }
        public DateTime? ActivityStartTime { get; set; } = DateTime.Now;
        public DateTime ActivityEndTime { get; set; } = DateTime.Now;

        public int CommentCount { get; set; }
        public int LikeCount { get; set; }

        public bool IsExternal { get; set; }
        public bool Timeout { get; set; }

        public bool IsActive { get; set; } = true;
        public int MaxUser { get; set; }
        public int TicketPrice { get; set; }

        public string? Categories { get; set; } = String.Empty;

        [NotMapped]
         public int[]? ActivityCategories
        {
            get
            {
                if (Categories != null && Categories != "")
                    return Array.ConvertAll(Categories.Split(';'), int.Parse);
                return null;
            }
            set
            {
                if (value != null)
                {
                    _data = value;
                    Categories = String.Join(";", _data.Select(p => p.ToString()).ToArray());
                }
                else
                {
                    _data = null;
                    Categories = String.Empty;
                }
            }
        }

        private int[] _data;



    }
}
