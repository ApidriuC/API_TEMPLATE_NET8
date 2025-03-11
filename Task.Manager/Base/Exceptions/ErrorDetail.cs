namespace TaskProject.Manager.Base.Exceptions;

/// <summary/>
public class ErrorDetail
{
    /// <summary>
    ///  Propiedad del modelo de datos que presento el error.
    /// </summary>
    public string PropertyName { get; set; }

    /// <summary>
    /// Mensaje o descripción del error.
    /// </summary>
    public string Message { get; set; }

    /// <summary/>
    public ErrorDetail()
    {
        Message = string.Empty;
        PropertyName = string.Empty;
    }
}
