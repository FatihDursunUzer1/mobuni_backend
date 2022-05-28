using Google.Cloud.Firestore;
using MobUni.ApplicationCore.Entities.UserAggregate;
using MobUni.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.Infrastructure.Firestore.Users
{
    public class FirestoreUser:IFirestoreUser
    {
        private readonly FirestoreDb _firestoreDb;
        private  Dictionary<string, object> _userData;

        public FirestoreUser()
        {
            _firestoreDb = FirestoreDb.Create("mobuni-f6930");
        }

        public async Task<bool> AddToUserDocument(User user)
        {

            _userData = new Dictionary<string, object>
            {
                {"userId", user.Id},
                {"name", user.Name},
                {"surname", user.Surname},
                {"email", user.Email},
                {"phoneNumber", user.PhoneNumber??null},
                {"userName", user.UserName},
                {"universityId", user.UniversityId},
                {"departmentId", user.DepartmentId}
            };
            await _firestoreDb.Collection("user").Document(user.Id).SetAsync(_userData);
            return true;
        }
    }
}
