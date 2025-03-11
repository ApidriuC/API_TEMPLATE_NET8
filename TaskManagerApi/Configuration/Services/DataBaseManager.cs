using Microsoft.Data.SqlClient;
using System.Data;
using TaskProject.Manager.Domain.Interfaces;

namespace TaskProject.ManagerApi.Configuration.Services;

/// <summary/>
public class DataBaseManager(IConnectionStrings connectionStrings) : IDataBaseManager
{
    /// <inheritdoc/>
    public IDbConnection DbTasksSQL => new SqlConnection(connectionStrings.ConnectionStringTasks);
}
