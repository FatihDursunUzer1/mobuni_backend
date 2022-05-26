using MobUni.ApplicationCore.Entities.ActivityAggregate;
using MobUni.ApplicationCore.Interfaces.Repositories;
using MobUni.Infrastructure.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.Infrastructure.Repositories
{
    public class ActivityParticipantRepository:EfRepositoryBase<ActivityParticipant>,IActivityParticipantRepository
    {
        public ActivityParticipantRepository(MobUniDbContext mobUniDbContext) : base(mobUniDbContext)
        {
        }

        public async Task<(ActivityParticipant,bool isJoined)> JoinOrLeave(ActivityParticipant activityParticipant)
        {
            var dbActivityParticipants = await GetAll(a => a.IsApproved && a.UserId == activityParticipant.UserId && a.ActivityId == activityParticipant.ActivityId);
            var dbActivityParticipant = dbActivityParticipants.FirstOrDefault();
            if (dbActivityParticipant == null)
            {
                activityParticipant = await Add(activityParticipant,a=>a.Activity,a=>a.User);
                return (activityParticipant,true);
            }
            else
            {
                dbActivityParticipant.IsJoined = !dbActivityParticipant.IsJoined;
                await Update(dbActivityParticipant, dbActivityParticipant.Id);
                if(dbActivityParticipant.IsJoined)
                return (dbActivityParticipant,true);
                else 
                    return (dbActivityParticipant, false);
            }
        }

        public async Task<List<int>> GetJoinedActivitiesIds(string userId)
        {
            var activityParticipants = await GetAll(a => a.UserId == userId && a.IsActive && a.IsJoined);
            var Ids = activityParticipants.Select(a => a.ActivityId).ToList();

            return Ids;
        }
    }
}
