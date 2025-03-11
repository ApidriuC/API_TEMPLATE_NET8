using MediatR;
using TaskProject.Manager.Application.Models;

namespace TaskProject.Manager.Application.Mediator.Queries;

/// <summary/>
public class LoginQuery : IRequest<LoginResult>
{
    /// <summary>
    /// Usuario aplicación que se quiere autenticar encriptado.
    /// </summary>
    public required string Usuario { get; set; }

    /// <summary>
    /// Contraseña del usuario que se quiere autenticar encriptada.
    /// </summary>
    public required string Contraseña { get; set; }
}

/// <summary/>
internal class LoginHanlder : IRequestHandler<LoginQuery, LoginResult>
{
    /// <inheritdoc/>
    public async Task<LoginResult> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(new LoginResult("1234"));
    }
}