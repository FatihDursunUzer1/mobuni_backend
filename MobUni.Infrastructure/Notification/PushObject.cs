using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.Infrastructure.Notification
{
   public class PushObject
    {
        public string Header { get; set; }
        public string Content { get; set; }
        public string[] Include_external_user_ids { get; set; }
        
        public string SenderUserId { get; set; }
        public NotificationType NotificationType { get; set; }
        public object DataId { get; set; }
        
    }
}
