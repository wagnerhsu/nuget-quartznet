using System;
using System.Configuration;
using Topshelf.HostConfigurators;

namespace QuartzWpf01Common
{
    public static class TopShelfExtensions
    {
        public static void ConfigureTimeout(this HostConfigurator configurator)
        {
            string startTimeout = ConfigurationManager.AppSettings["StartTimeout"];
            string stopTimeout = ConfigurationManager.AppSettings["StopTimeout"];
            if (startTimeout == null)
            {
                configurator.SetStartTimeout(TimeSpan.FromSeconds(30));
            }
            else
            {
                configurator.SetStartTimeout(TimeSpan.Parse(startTimeout));
            }

            if (stopTimeout == null)
            {
                configurator.SetStopTimeout(TimeSpan.FromSeconds(30));
            }
            else
            {
                configurator.SetStopTimeout(TimeSpan.Parse(stopTimeout));
            }
        }
    }
}