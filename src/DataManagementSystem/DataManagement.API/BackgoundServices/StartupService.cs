using DataManagement.Application.Initializers;

namespace DataManagement.API.BackgoundServices
{
	public class StartupService : BackgroundService
	{
		private readonly IAccountInitializer _accountInitializer;
		private readonly ITableCreator _tableCreator;
		private readonly IDBCreator _dbCreator;

		public StartupService(IAccountInitializer accountInitializer, ITableCreator tableCreator, IDBCreator dBCreator)
		{
			_accountInitializer = accountInitializer;
			_tableCreator = tableCreator;
			_dbCreator = dBCreator;
		}

		protected override async Task<Task> ExecuteAsync(CancellationToken stoppingToken)
		{
			await _dbCreator.CreateDatabaseIfNotExists();
			await _tableCreator.CreateTablesIfNotExist();
			_accountInitializer.TryCreate();

			return Task.CompletedTask;
		}
	}
}
	