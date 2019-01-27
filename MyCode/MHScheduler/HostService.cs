using QuartzWpf01Common;
using System.Configuration;

namespace MHScheduler
{
    internal class HostService
    {
        private readonly string _fileName;
        private readonly string _cronExpression;
        private SchedulerService schedulerService;

        public HostService(string fileName, string cronExpression)
        {
            _fileName = fileName;
            _cronExpression = cronExpression;
            var jobName = ConfigurationManager.AppSettings["JobName"];
            var groupName = ConfigurationManager.AppSettings["GroupName"];
            var triggerName = ConfigurationManager.AppSettings["TriggerName"];
            schedulerService = new SchedulerService(jobName, groupName, triggerName, groupName, cronExpression);
        }

        public void Start()
        {
            schedulerService.Start();
        }

        public void Stop()
        {
            schedulerService.Stop();
        }
    }
}