using NLog;
using Quartz;
using Quartz.Impl;

namespace QuartzWpf01Common
{
    public class SchedulerService
    {
        private readonly string _jobName;
        private readonly string _jobGroupName;
        private readonly string _triggerName;
        private readonly string _triggerGroupName;
        private readonly string _cronExpression;
        private IScheduler scheduler;
        private ILogger Logger = LogManager.GetCurrentClassLogger();

        public SchedulerService(string jobName, string jobGroupName, string triggerName, string triggerGroupName, string cronExpression)
        {
            _jobName = jobName;
            _jobGroupName = jobGroupName;
            _triggerName = triggerName;
            _triggerGroupName = triggerGroupName;
            _cronExpression = cronExpression;
        }

        public void Start()
        {
            scheduler = StdSchedulerFactory.GetDefaultScheduler();
            IJobDetail job = JobBuilder.Create<ExecutePsFileJob>()
        .WithIdentity(_jobName, _jobGroupName)
        .Build();

            ICronTrigger trigger = (ICronTrigger)TriggerBuilder.Create()
                .WithIdentity(_triggerName, _triggerGroupName)
                .WithCronSchedule(_cronExpression)
                .Build();

            // Tell quartz to schedule the job using our trigger
            scheduler.ScheduleJob(job, trigger);
            scheduler.Start();
            Logger.Info("Start...");
        }

        public void Stop()
        {
            Logger.Info("Stop...");
            scheduler.Shutdown();
            Logger.Info("Stop Done");
        }
    }
}