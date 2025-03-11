using TaskProject.Manager.Base.Exceptions;

namespace TaskProject.Manager.Base.Interfaces;

/// <summary>
/// Manejo global del error
/// </summary>
public interface IErrorHandler
{
    /// <summary>
    /// Generar un mensaje de error.
    /// </summary>
    ErrorMessage Generar(Exception ex);
}