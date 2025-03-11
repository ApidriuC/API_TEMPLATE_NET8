using System.Data;

namespace TaskProject.Manager.Domain.Interfaces;

/// <summary/>
public interface IDataBaseManager
{
    /// <summary>
    /// Conexión SQL.
    /// </summary>
    IDbConnection DbTasksSQL { get; }
}
