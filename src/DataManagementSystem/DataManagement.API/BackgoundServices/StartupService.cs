using DataManagement.Application.Initializers;

namespace DataManagement.API.BackgoundServices
{
	public class StartupService : BackgroundService
	{
		private readonly IAccountInitializer _accountInitializer;

		public StartupService(IAccountInitializer accountInitializer)
		{
			_accountInitializer = accountInitializer;
		}

		protected override  Task ExecuteAsync(CancellationToken stoppingToken)
		{
			 _accountInitializer.TryCreate();

			return Task.CompletedTask;
		}
	}
}
	