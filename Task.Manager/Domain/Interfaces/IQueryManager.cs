using static Dapper.SqlMapper;

namespace TaskProject.Manager.Domain.Interfaces;

/// <summary>
/// Provee métodos para la ejecución de instrucciones hacia la capa de datos.
/// </summary>
internal interface IQueryManager : IDbContext
{
    /// <summary>
    /// Ejecuta la instrucción de un procedimiento almacenado.
    /// </summary>
    /// <param name="nameProcedure">Nombre del procedimiento.</param>
    /// <param name="parameters">Parámetros del procedimiento.</param>
    /// <param name="closeConnection">Indica si debe suprimir los objetos al finalizar la instrucción.</param>
    void ExecuteInstruction(string nameProcedure, object parameters, bool closeConnection = true);

    /// <summary>
    /// Ejecuta la instrucción de un procedimiento almacenado y mapea la respuesta a un objeto singular.
    /// </summary>
    /// <typeparam name="T">Modelo a mapaear.</typeparam>
    /// <param name="nameProcedure">Nombre del procedimiento.</param>
    /// <param name="parameters">Parámetros del procedimiento.</param>
    /// <param name="closeConnection">Indica si debe suprimir los objetos al finalizar la instrucción.</param>
    /// <returns>Modelo mapeado.</returns>
    T ExecuteSingleQuery<T>(string nameProcedure, object parameters, bool closeConnection = true);

    /// <summary>
    /// Ejecuta la instrucción de un procedimiento almacenado y mapea la respuesta a un objeto complejo.
    /// </summary>
    /// <typeparam name="T">Modelo a mapaear.</typeparam>
    /// <param name="nameProcedure">Nombre del procedimiento.</param>
    /// <param name="parameters">Parámetros del procedimiento.</param>
    /// <param name="closeConnection">Indica si debe suprimir los objetos al finalizar la instrucción.</param>
    /// <returns>Modelo mapeado.</returns>
    List<T> ExecuteMultipleQuery<T>(string nameProcedure, object parameters, bool closeConnection = true);

    /// <summary>
    /// Ejecuta la instrucción de un procedimiento almacenado y devuelve el comando con la respuesta.
    /// </summary>
    /// <param name="nameProcedure">Nombre del procedimiento.</param>
    /// <param name="parameters">Parámetros del procedimiento.</param>
    /// <param name="onSuccess">Acción a ejecutar luego de leer el comando.</param>
    /// <param name="closeConnection">Indica si debe suprimir los objetos al finalizar la instrucción.</param>
    /// <returns>Lector de tablas.</returns>
    GridReader ExecuteGroupedQuery(string nameProcedure, object parameters, Action<GridReader> onSuccess, bool closeConnection = true);
}
