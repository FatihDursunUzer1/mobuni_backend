using MobUni.ApplicationCore.Entities.ActivityAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.ApplicationCore.Interfaces.Repositories
{
    public interface IActivityParticipantRepository:IRepository<ActivityParticipant>
    {
        List<int> GetJoinedActivitiesIds(string userId);
        Task<(ActivityParticipant, bool isJoined)> JoinOrLeave(ActivityParticipant activityParticipant);
    }
}
