using Microsoft.Data.SqlClient;

namespace DataManagement.Domain.InfrastructureInterfaces
{
	public interface ISqlConnectionFactory
    {
        SqlConnection CreateConnection();
		SqlConnection CreateMasterConnection();

	}
}
