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

        private IActivityRepository _activityRepository;
        public ActivityParticipantRepository(MobUniDbContext mobUniDbContext, IActivityRepository activityRepository) : base(mobUniDbContext)
        {
            _activityRepository = activityRepository;
        }

        public async Task<(ActivityParticipant,bool isJoined)> JoinOrLeave(ActivityParticipant activityParticipant)
        {
            var dbActivityParticipants = GetAll(a => a.IsApproved && a.UserId == activityParticipant.UserId && a.ActivityId == activityParticipant.ActivityId);
            var dbActivityParticipant = dbActivityParticipants.FirstOrDefault();
            var activity = _activityRepository.GetById(activityParticipant.ActivityId);
            if (dbActivityParticipant == null)
            {
                if (activity.MaxUser < activity.JoinedCount + 1 && activity.MaxUser != 0)
                {
                    return (null, false);
                }
                activityParticipant = await Add(activityParticipant,a=>a.Activity,a=>a.User);
                return (activityParticipant,true);
            }
            else
            {
                if (!dbActivityParticipant.IsJoined && dbActivityParticipant.UpdatedTime.Value.AddHours(24) > DateTime.Now.ToUniversalTime())
                {
                    dbActivityParticipant.IsApproved = false;
                    return (dbActivityParticipant, false);
                }
                dbActivityParticipant.IsJoined = !dbActivityParticipant.IsJoined;
                if (activity.MaxUser < activity.JoinedCount + 1 && dbActivityParticipant.IsJoined && activity.MaxUser != 0)
                {
                    return (null, false);
                }
                await Update(dbActivityParticipant, dbActivityParticipant.Id);
                if(dbActivityParticipant.IsJoined)
                return (dbActivityParticipant,true);
                else 
                    return (dbActivityParticipant, false);
            }
        }

        public List<int> GetJoinedActivitiesIds(string userId)
        {
            var activityParticipants =  GetAll(a => a.UserId == userId && a.IsActive && a.IsJoined);
            var Ids = activityParticipants.Select(a => a.ActivityId).ToList();

            return Ids;
        }
    }
}
