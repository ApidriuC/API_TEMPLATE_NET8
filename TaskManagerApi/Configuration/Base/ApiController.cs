using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TaskProject.Manager.Base.Exceptions;

namespace TaskProject.ManagerApi.Configuration.Base;

/// <summary>
/// Controlador de peticiones API.
/// </summary>
[ApiController]
[SwaggerResponse(200, "Solicitud recibida exitosamente")]
[SwaggerResponse(501, "Error de aplicación", typeof(ErrorMessage))]
public abstract class ApiController : ControllerBase { }
