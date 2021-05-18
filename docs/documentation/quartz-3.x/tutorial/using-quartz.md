---
title: 'Using Quartz'
---

Before you can use the scheduler, it needs to be instantiated (who'd have guessed?).
To do this, you use an implementor of ISchedulerFactory.

Once a scheduler is instantiated, it can be started, placed in stand-by mode, and shutdown.
Note that once a scheduler is shutdown, it cannot be restarted without being re-instantiated.
Triggers do not fire (jobs do not execute) until the scheduler has been started, nor while it is
in the paused state.

Here's a quick snippet of code, that instantiates and starts a scheduler, and schedules a job for execution:

__Using Quartz.NET__

```csharp
// construct a scheduler factory
StdSchedulerFactory factory = new StdSchedulerFactory();

// get a scheduler
IScheduler scheduler = await factory.GetScheduler();
await scheduler.Start();

// define the job and tie it to our HelloJob class
IJobDetail job = JobBuilder.Create<HelloJob>()
    .WithIdentity("myJob", "group1")
    .Build();

// Trigger the job to run now, and then every 40 seconds
ITrigger trigger = TriggerBuilder.Create()
    .WithIdentity("myTrigger", "group1")
    .StartNow()
    .WithSimpleSchedule(x => x
        .WithIntervalInSeconds(40)
        .RepeatForever())
.Build();
    
await scheduler.ScheduleJob(job, trigger);

// You could also schedule multiple triggers for the same job with
// await scheduler.ScheduleJob(job, new List<ITrigger>() { trigger1, trigger2 }, replace: true);
```

As you can see, working with Quartz.NET is rather simple. In [Lesson 2](jobs-and-triggers.md) we'll give a quick overview of Jobs and Triggers, so that you can more fully understand this example.
