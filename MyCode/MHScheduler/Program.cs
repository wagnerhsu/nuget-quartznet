using QuartzWpf01Common;
using System.Configuration;
using Topshelf;

namespace MHScheduler
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var serviceName = ConfigurationManager.AppSettings["ServiceName"];
            var cronExpression = ConfigurationManager.AppSettings["CronExpression"];
            var powershellFileName = ConfigurationManager.AppSettings["PowerShellFileName"];
            var host = HostFactory.New(x =>
            {
                x.Service<HostService>(s =>
                {
                    s.ConstructUsing(name =>
                    {
                        return new HostService(powershellFileName, cronExpression);
                    });
                    s.WhenStarted(tc => { tc.Start(); });
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();
                x.StartAutomatically();
                x.DependsOnMsmq();
                x.SetDescription(serviceName);
                x.SetDisplayName(serviceName);
                x.SetServiceName(serviceName);
                x.ConfigureTimeout();
            });
            host.Run();
        }
    }
}