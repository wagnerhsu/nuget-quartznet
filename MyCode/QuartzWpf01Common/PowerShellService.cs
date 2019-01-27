using NLog;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace QuartzWpf01Common
{
    public class PowerShellService
    {
        private readonly string _fileName;
        private ILogger Logger = LogManager.GetCurrentClassLogger();

        public PowerShellService()
        {
            _fileName = "PowerShell.exe";
        }

        public void StartProcess(string cmd, string label)
        {
            Logger.Debug($"{cmd}");
            ProcessStartInfo psi = new ProcessStartInfo
            {
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardError = true,
                FileName = _fileName,
                Arguments = cmd
            };
            var p = Process.Start(psi);
            Task.Factory.StartNew(() =>
            {
                StreamReader myStreamReader = p.StandardError;
                var status = myStreamReader.ReadToEnd();

                if (!string.IsNullOrEmpty(status))
                {
                    Logger.Error($"{label} ERROR: {status}");
                }

                p.Close();
            });
        }

        public void ExecuteFile(string fileName)
        {
            StartProcess($"-NoProfile -ExecutionPolicy Bypass -File {fileName}", $"Execute {fileName}");
        }
    }
}