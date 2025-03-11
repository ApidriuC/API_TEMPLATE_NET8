using TaskProject.Manager.Base.Interfaces;

namespace TaskProject.Manager.Base.Exceptions;

/// <summary>
/// Manejo global del error.
/// </summary>
public class ErrorHandler() : IErrorHandler
{
    /// <inheritdoc/>
    public ErrorMessage Generar(Exception ex)
    {
        // Aplanar excepciones.
        if (ex is AggregateException aggException)
        {
            aggException
                .Flatten()
                .InnerExceptions
                .Where(innerEx => innerEx is not AggregateException)
                .ToList()
                .ForEach(innerEx => GenerarErrorMessage(innerEx));
        }

        return GenerarErrorMessage(ex);
    }

    private static ErrorMessage GenerarErrorMessage(Exception ex)
    {
        var exType = ex.GetType();
        var errorMessage = new ErrorMessage
        {
            Code = exType.Name,
            Message = ex.Message,
            TraceId = Guid.NewGuid().ToString()
        };

        if (ex is CustomErrorException customEx)
        {
            errorMessage.Details = customEx.Errores;
            errorMessage.Code = customEx.ExecMethodName;
        }
        else if (ex is UnauthorizedAccessException)
        {
            errorMessage.Message = "Acceso denegado.";
        }

        return errorMessage;
    }
}
