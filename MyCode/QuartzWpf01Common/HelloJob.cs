using NLog;
using Quartz;

namespace QuartzWpf01Common
{
    public class HelloJob : IJob
    {
        private static ILogger Logger = LogManager.GetCurrentClassLogger();

        public void Execute(IJobExecutionContext context)
        {
            Logger.Debug($"Greetings from HelloJob!");
        }
    }
}