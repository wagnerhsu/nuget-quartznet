using NLog;
using Quartz;
using Quartz.Impl;
using QuartzWpf01Common;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;

namespace QuartzWpf01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ILogger Logger = LogManager.GetCurrentClassLogger();
        private IScheduler scheduler;
        private string cronExpression;

        public MainWindow()
        {
            InitializeComponent();
            cronExpression = ConfigurationManager.AppSettings["CronExpression"];
        }

        private void Grid_Click(object sender, RoutedEventArgs e)
        {
            Button button = e.Source as Button;
            if (button.Name == nameof(btnStart))
            {
                scheduler = StdSchedulerFactory.GetDefaultScheduler();
                IJobDetail job = JobBuilder.Create<ExecutePsFileJob>()
            .WithIdentity("job1", "group1")
            .Build();

                ICronTrigger trigger = (ICronTrigger)TriggerBuilder.Create()
                    .WithIdentity("trigger1", "group1")
                    .WithCronSchedule(cronExpression)
                    .Build();

                // Tell quartz to schedule the job using our trigger
                scheduler.ScheduleJob(job, trigger);
                scheduler.Start();
                Logger.Info("Start...");
            }
            else if (button.Name == nameof(btnStop))
            {
                scheduler.Shutdown();
                Logger.Info("Stop");
            }
        }
    }
}