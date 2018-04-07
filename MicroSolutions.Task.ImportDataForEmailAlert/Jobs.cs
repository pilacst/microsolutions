using Quartz;

namespace MicroSolutions.Task.ImportDataForEmailAlert
{
	public class Jobs : IJob
	{
		public void Execute(IJobExecutionContext context)
		{
			var sendEmail = new SendEmail();
			sendEmail.SendEmailsToAdmin();
		}
	}
}
