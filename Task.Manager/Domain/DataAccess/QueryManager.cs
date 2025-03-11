using Dapper;
using System.Data;
using System.Reflection;
using TaskProject.Manager.Domain.Interfaces;
using TaskProject.Manager.Domain.Models.Attributes;
using static Dapper.SqlMapper;

namespace TaskProject.Manager.Domain.DataAccess;

/// <inheritdoc/>
/// <summary>
/// Inicializa el manejo de la capa de datos.
/// </summary>
/// <param name="connection">Conexión a la capa de datos.</param>
/// <param name="transaction">Transacción previamente abierta.</param>
/// <param name="commandTimeout">Tiempo de espera para la ejecución de la instrucción hacia la capa de datos.</param>
public class QueryManager(IDbConnection connection, IDbTransaction transaction = null, int commandTimeout = 300)
    : DbContext(connection, transaction), IQueryManager
{
    private int CommandTimeout => commandTimeout;

    /// <inheritdoc/>
    public void ExecuteInstruction(string nameProcedure, object parameters, bool closeConnection = true)
    {
        IDbConnection conn = Connection();

        try
        {
            conn.Execute(
                param: parameters,
                sql: nameProcedure,
                transaction: Transaction(),
                commandTimeout: CommandTimeout,
                commandType: CommandType.StoredProcedure
            );
        }
        finally
        {
            if (closeConnection)
            {
                conn.Close();
            }
        }
    }

    /// <inheritdoc/>
    public T ExecuteSingleQuery<T>(string nameProcedure, object parameters, bool closeConnection = true)
    {
        IDbConnection conn = Connection();

        try
        {
            SetCustomMapProperty<T>();
            return conn.QuerySingleOrDefault<T>(
                param: parameters,
                sql: nameProcedure,
                transaction: Transaction(),
                commandTimeout: CommandTimeout,
                commandType: CommandType.StoredProcedure
            );
        }
        finally
        {
            if (closeConnection)
            {
                conn.Close();
            }
        }
    }

    /// <inheritdoc/>
    public List<T> ExecuteMultipleQuery<T>(string nameProcedure, object parameters, bool closeConnection = true)
    {
        IDbConnection conn = Connection();

        try
        {
            SetCustomMapProperty<T>();
            var command = conn.QueryMultiple(
                param: parameters,
                sql: nameProcedure,
                transaction: Transaction(),
                commandTimeout: CommandTimeout,
                commandType: CommandType.StoredProcedure
            );

            return command.Read<T>().ToList();
        }
        finally
        {
            if (closeConnection)
            {
                conn.Close();
            }
        }
    }

    /// <inheritdoc/>
    public GridReader ExecuteGroupedQuery(string nameProcedure, object parameters, Action<GridReader> onSuccess, bool closeConnection = true)
    {
        IDbConnection conn = Connection();

        try
        {
            var command = conn.QueryMultiple(
                param: parameters,
                sql: nameProcedure,
                transaction: Transaction(),
                commandTimeout: CommandTimeout,
                commandType: CommandType.StoredProcedure
            );

            onSuccess?.Invoke(command);
            return command;
        }
        finally
        {
            if (closeConnection)
            {
                conn.Close();
            }
        }
    }

    /// <summary>
    /// Establece la personalización del mapeo de las propiedades por el atributo <see cref="ColumnNameAttribute"/>.
    /// </summary>
    /// <typeparam name="T">Modelo a mapaear.</typeparam>
    public static void SetCustomMapProperty<T>()
    {
        if (!typeof(T).IsClass) return;

        var customPropertyTypeMap = new CustomPropertyTypeMap(typeof(T), (type, columnName) =>
        {
            PropertyInfo[] properties = type.GetProperties();

            var property = Array.Find(properties, p =>
            {
                string customName = GetCustomNameFromAttribute(p);
                return !string.IsNullOrEmpty(customName) && customName.Equals(columnName, StringComparison.OrdinalIgnoreCase);
            });

            return property;
        });

        SetTypeMap(typeof(T), customPropertyTypeMap);
    }

    private static string GetCustomNameFromAttribute(PropertyInfo property)
    {
        if (property == null) return string.Empty;

        //Asignar el mapeo con el nombre especificado en el atributo.
        var customAttribute = (ColumnNameAttribute?)Attribute.GetCustomAttribute(property, typeof(ColumnNameAttribute), false);

        return (customAttribute?.Name ?? property.Name).ToLower();
    }
}
