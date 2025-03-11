using System.Data;

namespace TaskProject.Manager.Domain.Interfaces;

/// <summary>
/// Provee métodos para la ejecución de comandos hacia una capa de datos.
/// </summary>
internal interface IDbContext : IDisposable
{
    /// <summary>
    /// Inicia una conexión y abre una transacción en la capa de datos.
    /// </summary>
    void Begin();

    /// <summary>
    /// Ejecuta un comando hacia la capa de datos.
    /// </summary>
    /// <returns>Devuelve un <see cref="IDbCommand"/>.</returns>
    IDbCommand Command();

    /// <summary>
    /// Abré una conexión hacia la capa de datos.
    /// </summary>
    /// <returns>Devuelve un <see cref="IDbConnection"/></returns>
    IDbConnection Connection();

    /// <summary>
    /// Inicializa una transacción en la capa de datos.
    /// </summary>
    /// <returns>Devuelve un <see cref="IDbTransaction"/></returns>
    IDbTransaction Transaction();

    /// <summary>
    /// Confirma una transacción en la capa de datos.
    /// </summary>
    void Commit();
}
