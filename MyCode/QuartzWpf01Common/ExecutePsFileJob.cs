using Quartz;
using System.Configuration;

namespace QuartzWpf01Common
{
    public class ExecutePsFileJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            var fileName = ConfigurationManager.AppSettings["PowerShellFileName"];
            PowerShellService service = new PowerShellService();
            service.ExecuteFile(fileName);
        }
    }
}