using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.Infrastructure.Firestore.Notifications
{
    public class FirebaseNotification
    {
        private readonly FirestoreDb _firestoreDb;
        public FirebaseNotification()
        {
            _firestoreDb = FirestoreDb.Create("mobuni-f6930");
        }

        public async Task<bool> AddToFirebase(object Data, string receiverUserId)
        {
            await _firestoreDb.Collection("notifications").Document(receiverUserId).Collection("notification").Document().SetAsync(Data);
            return true;
        }


    }
}
