using Microsoft.Data.SqlClient;

namespace DataManagement.Application.Abstractions.Interfaces
{
	public interface ISqlConnectionFactory
    {
        SqlConnection CreateConnection();
		SqlConnection CreateMasterConnection();

	}
}
