using System.Net;
using TaskProject.Manager.Base.Interfaces;

namespace TaskProject.ManagerApi.Configuration.Base;

/// <summary/>
public class ErrorMiddleware(RequestDelegate next)
{
    /// <summary/>
    public async Task InvokeAsync(HttpContext context, IErrorHandler errorHandler)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            var errorMessage = errorHandler.Generar(ex);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.NotImplemented;
            await context.Response.WriteAsJsonAsync(errorMessage);
        }
    }
}
