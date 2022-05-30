using MobUni.ApplicationCore.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.Infrastructure.Notification
{
    public class OneSignalNotification:IPushNotification
    {
        public async Task<bool> SendActivityJoinedNotification(string senderId, string receiverId, int CommentId, string senderName, string title)
        {
            var pushObject = new PushObject
            {
                NotificationType = NotificationType.ACTIVITYJOINED,
                SenderUserId = senderId,
                Include_external_user_ids = new string[] { receiverId },
                Header = senderName + " " + title + "isimli etkinliğinize katılıyor.",
                Content = $"{senderName}: Bu güzel etkinliği düzenlediğiniz için teşekkür ederim",
                DataId = CommentId
            };
            return await SendNotificationAsync(pushObject);
        }

       

        public async Task<bool> SendQuestionCommentLikeNotification(string senderId, string receiverId, int CommentId, string senderName, string text)
        
        {
            return await SendCommentLikeNotification(senderId, receiverId, CommentId, senderName, text, NotificationType.QUESTIONCOMMENTLIKE);
        }

        public async Task<bool> SendActivityCommentLikeNotification(string senderId, string receiverId, int CommentId, string senderName, string text)
        {
            return await SendCommentLikeNotification(senderId, receiverId, CommentId, senderName, text, NotificationType.ACTIVITYCOMMENTLIKE);
        }
        public async Task<bool> SendQuestionLikeNotification(string senderId, string receiverId, int QuestionId, string senderName,string text)
        {
            var pushObject = new PushObject
            {
                NotificationType = NotificationType.QUESTIONLIKE,
                SenderUserId = senderId,
                Include_external_user_ids = new string[] { receiverId },
                DataId = QuestionId,
                Header = senderName + " sorunuzu beğendi",
                Content = text.Count() > 100 ? text.Substring(0, 100) + "..." : text
            };

            return await SendNotificationAsync(pushObject);
        }


        public async Task<bool> SendActivityCommentNotification(string senderId, string receiverId, int CommentId, string senderName, string text)
        {
            return await SendCommentNotification(senderId, receiverId, CommentId, senderName, text, NotificationType.ACTIVITYCOMMENT);
        }

        public async Task<bool> SendQuestionCommentNotification(string senderId, string receiverId, int CommentId, string senderName, string text)
        {
            return await SendCommentNotification(senderId, receiverId, CommentId, senderName, text, NotificationType.QUESTIONCOMMENT);
        }

        private async Task<bool> SendCommentNotification(string senderId, string receiverId, int commentId, string senderName, string text, NotificationType notificationType)
        {
            var pushObject = new PushObject
            {
                NotificationType = notificationType,
                SenderUserId = senderId,
                Include_external_user_ids = new string[] { receiverId },
                DataId = commentId,
                Header = notificationType == NotificationType.QUESTIONCOMMENT ? $"{senderName} sorunuza yorum yaptı" : $"{senderName} Etkinliğinize yorum yaptı",
                Content = text.Count() > 100 ? text.Substring(0, 100) + "..." : text
            };

            return await SendNotificationAsync(pushObject);
        }

        private async Task<bool> SendCommentLikeNotification(string senderId, string receiverId, int CommentId, string senderName, string text, NotificationType type)
        {
            var pushObject = new PushObject
            {
                NotificationType = type,
                SenderUserId = senderId,
                Include_external_user_ids = new string[] { receiverId },
                DataId = CommentId,
                Header = senderName + " yorumunuzu beğendi",
                Content = text.Count() > 100 ? text.Substring(0, 100) + "..." : text
            };

            return await SendNotificationAsync(pushObject);
        }        

        private async Task<bool> SendNotificationAsync(PushObject pushObject)
        {
            try
            {

                if (pushObject.Include_external_user_ids.Length == 0)
                {
                    return false;
                }
                else if (pushObject.Include_external_user_ids.Length == 1 && pushObject.SenderUserId == pushObject.Include_external_user_ids[0])
                {
                    return false;
                }

                var obj = new
                {
                    app_id = "73ffc06f-2990-4166-823d-38350e97c803",
                    include_external_user_ids = pushObject.Include_external_user_ids,
                    data =new {
                    notificationType = ((int)pushObject.NotificationType),
                         senderUserGid = pushObject.SenderUserId,
                        dataId = pushObject.DataId.ToString(),
                    },
                headings = new {
                    en = pushObject.Header
                 },
                contents = new {
                    en= pushObject.Content
                 },
                ios_badgeType = "Increase",
                ios_badgeCount = 1
        };
            var obString = JsonConvert.SerializeObject(obj);
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("https://onesignal.com/api/v1/notifications"),
                    Headers =
                    {
                        { "Accept", "application/json" },
                        { "Authorization", "Basic NjZlODM3NTAtZmU3NC00NzVmLTlkN2EtZWM4YTgzNGM2ZDQ3" },
                    },
                    Content = new StringContent(obString, Encoding.UTF8, "application/json") {
                        Headers =
        {
            ContentType = new MediaTypeHeaderValue("application/json")
        }
                    }

                };

                var a = request.Content;
                var b = a.ToString();
          
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(body);
                }

                return true;

            }
            catch (Exception ex)
            {
                var str = ex.Message;
                return false;
            }
        }
    }
    
}
