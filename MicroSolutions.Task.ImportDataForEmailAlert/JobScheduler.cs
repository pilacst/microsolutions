using Quartz;
using Quartz.Impl;

namespace MicroSolutions.Task.ImportDataForEmailAlert
{
	public class JobScheduler
	{
		public static void Start()
		{
			IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
			scheduler.Start();

			IJobDetail job = JobBuilder.Create<Jobs>().Build();
			IJobDetail job2 = JobBuilder.Create<NotificationTableUpdateJob>().Build();
			///700
			ITrigger trigger1 = TriggerBuilder.Create()
				.WithIdentity("trigger1", "group1")
				.StartNow()
				.WithSimpleSchedule(x => x
					.WithIntervalInMinutes(60)
					.RepeatForever())
				.Build();

			///1400
			ITrigger trigger2 = TriggerBuilder.Create()
				.WithIdentity("trigger2", "group2")
				.StartNow()
				.WithSimpleSchedule(x => x
					.WithIntervalInMinutes(60)
					.RepeatForever())
				.Build();

			scheduler.ScheduleJob(job, trigger1);
			scheduler.ScheduleJob(job2, trigger2);
		}
	}
}
