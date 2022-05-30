using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.ApplicationCore.Interfaces
{
    public interface IPushNotification
    {
        Task<bool> SendQuestionLikeNotification(string senderId, string receiverId, int QuestionId, string senderName, string text);
        Task<bool> SendQuestionCommentLikeNotification(string senderId, string receiverId, int CommentId, string senderName, string text);

        Task<bool> SendActivityCommentLikeNotification(string senderId, string receiverId, int CommentId, string senderName, string text);
        Task<bool> SendActivityJoinedNotification(string senderId, string receiverId, int CommentId, string senderName, string title);

        Task<bool> SendActivityCommentNotification(string senderId, string receiverId, int CommentId, string senderName, string text);

        Task<bool> SendQuestionCommentNotification(string senderId, string receiverId, int CommentId, string senderName, string text);
        //Task<bool> SendQuestionLikeNotification();
    }
}
