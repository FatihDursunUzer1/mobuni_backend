using Microsoft.Extensions.DependencyInjection;
using MobUni.ApplicationCore.Interfaces;
using MobUni.Infrastructure.Data.Contexts;

namespace MobUni.WebAPI.BackgroundServices
{
    public class ActivityTimeOutService : IHostedService
    {
        private readonly IActivityService _activityService;
        private readonly IServiceScopeFactory _scopeFactory;
        Timer _timer;
        public ActivityTimeOutService(IActivityService activityService,IServiceScopeFactory serviceProviderFactory)
        {
            _activityService= activityService;
            _scopeFactory= serviceProviderFactory;
        }
       
        public  Task StartAsync(CancellationToken cancellationToken)
        {
            // _timer = new Timer(GetNoTimeOutActivities,null, TimeSpan.Zero, TimeSpan.FromHours(1));

            _timer = new Timer(GetNoTimeOutActivities, null, TimeSpan.Zero, TimeSpan.FromDays(1));

            return Task.CompletedTask;
        }

        public  Task StopAsync(CancellationToken cancellationToken)
        {
            _timer.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        } 

        private void GetNoTimeOutActivities(object? state)
        {
            Console.WriteLine("Fonksiyon çalıştı");
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<MobUniDbContext>();
                var activities = dbContext.Activities.Where(activities => activities.Timeout == false).ToList();

                foreach (var activity in activities)
                {
                    if (activity.ActivityStartTime <= DateTime.Now)
                    {
                        activity.Timeout = true;
                        dbContext.Update(activity);
                    }
                }
                dbContext.SaveChanges();
            }
        }
    }
}
