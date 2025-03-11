namespace TaskProject.Manager.Base.Exceptions;

/// <summary/>
public class ErrorMessage
{
    /// <summary>
    /// Tipo/Categoria del error lanzado.
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// Código del error lanzado.
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Mensaje o descripción del error.
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// Id de log de errores.
    /// </summary>
    public string TraceId { get; set; }

    /// <summary>
    /// Listado de propiedades que presentaron error.
    /// </summary>
    public IEnumerable<ErrorDetail> Details { get; set; }

    /// <summary/>
    public ErrorMessage()
    {
        Type = string.Empty;
        Code = string.Empty;
        Message = string.Empty;
        TraceId = string.Empty;
        Details = [];
    }
}
