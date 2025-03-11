using System.Reflection;

namespace TaskProject.Manager.Base.Exceptions;

/// <summary>
/// Excepciones personalizadas.
/// </summary
public sealed class CustomErrorException : Exception
{
    /// <summary>
    /// Nombre del lanzador.
    /// </summary>
    public string ExecMethodName { get; } = string.Empty;

    /// <summary>
    /// Listado de errores.
    /// </summary>
    public IEnumerable<ErrorDetail> Errores { get; } = [];

    /// <summary>
    /// Constructor por defecto.
    /// </summary>
    private CustomErrorException() : base() { }

    /// <summary>
    /// Constructor con mensaje
    /// </summary>
    public CustomErrorException(MethodBase baseMethod, string messsage) : base(messsage)
    {
        ExecMethodName = GetCurrentMethodName(baseMethod);
    }

    /// <summary>
    /// Constructor con mensaje
    /// </summary>
    public CustomErrorException(MethodBase baseMethod, string message, IEnumerable<ErrorDetail> errors) : base(message)
    {
        ExecMethodName = GetCurrentMethodName(baseMethod);
        Errores = errors;
    }

    /// <summary>
    /// Constructor con mensaje
    /// </summary>
    public CustomErrorException(MethodBase baseMethod, string message, string propertyName) : base(message)
    {
        ExecMethodName = GetCurrentMethodName(baseMethod);
        Errores =
        [
            new()
            {
                PropertyName = propertyName,
                Message = message
            }
        ];
    }

    private static string GetCurrentMethodName(MethodBase baseMethod)
    {
        var tipoLanzador = baseMethod.DeclaringType;
        return $"{tipoLanzador?.Name}.{baseMethod.Name}";
    }
}
