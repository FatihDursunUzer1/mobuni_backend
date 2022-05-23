namespace MobUni.WebAPI.BackgroundService
{
    public class ActivityTimeOutService : IHostedService
    {
        Timer _timer;
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(GetNoTimeOutActivities,null, TimeSpan.Zero, TimeSpan.FromHours(1));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        private void GetNoTimeOutActivities(object? state)
        {

        }
    }
}
