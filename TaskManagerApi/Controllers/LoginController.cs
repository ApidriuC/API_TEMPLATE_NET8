using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskProject.Manager.Application.Mediator.Queries;
using TaskProject.Manager.Application.Models;
using TaskProject.ManagerApi.Configuration.Base;

namespace TaskProject.ManagerApi.Controllers;

/// <summary>
/// Controlador de la autenticación.
/// </summary>
public class LoginController(IMediator mediator) : ApiController
{
    private IMediator Mediator => mediator;

    /// <summary>
    /// Permite a un usuario autorizado obtener un token (JWT).
    /// El token permite acceder a los demás recursos de la API.
    /// </summary>
    [AllowAnonymous]
    [HttpPost("Login/V1/AutenticarUsuario")]
    public async Task<ActionResult<LoginResult>> ExecuteAsync(LoginQuery request)
    {
        ArgumentNullException.ThrowIfNull(request);
        var result = await Mediator.Send(request);
        return Ok(result);
    }
}
