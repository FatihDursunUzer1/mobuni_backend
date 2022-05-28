using MobUni.ApplicationCore.Entities.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.ApplicationCore.Interfaces
{
    public interface IFirestoreUser
    {
        Task<bool> AddToUserDocument(User user);
    }
}
