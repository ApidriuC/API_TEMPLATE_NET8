using System.Data;
using TaskProject.Manager.Domain.Interfaces;

namespace TaskProject.Manager.Domain.DataAccess;

/// <inheritdoc/>
public class DbContext : IDbContext
{
    private readonly IDbConnection _connection;

    private IDbTransaction _transaction;

    /// <summary>
    /// Inicializa el contexto de conexión.
    /// </summary>
    /// <param name="connection">Conexión.</param>
    /// <param name="transaction">Indica si necesita transacción.</param>
    public DbContext(IDbConnection connection, IDbTransaction transaction = null)
    {
        _connection = connection;
        _transaction = transaction;
    }

    private void OpenConnection()
    {
        if (_connection.State == ConnectionState.Closed)
        {
            _connection.Open();
        }
    }

    /// <inheritdoc/>
    public void Begin()
    {
        if (_transaction == null)
        {
            OpenConnection();
            _transaction = _connection.BeginTransaction();
        }
    }

    /// <inheritdoc/>
    public IDbConnection Connection()
    {
        OpenConnection();
        return _connection;
    }

    /// <inheritdoc/>
    public IDbTransaction Transaction()
    {
        return _transaction;
    }

    /// <inheritdoc/>
    public IDbCommand Command()
    {
        OpenConnection();
        IDbCommand dbCommand = _connection.CreateCommand();
        dbCommand.Transaction = _transaction;
        return dbCommand;
    }

    /// <inheritdoc/>
    public void Commit()
    {
        _transaction?.Commit();
        _transaction = null;
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Cierra la conexión y revierte la transacción si es necesario.
    /// </summary>
    /// <param name="disposing">Indica si se debe cerrar la conexión.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _transaction?.Rollback();
            _transaction = null;
            _connection.Dispose();
        }
    }
}
