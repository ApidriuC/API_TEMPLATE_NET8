namespace TaskProject.Manager.Application.Models;

/// <summary/>
/// <param name="token">Llave de autenticación.</param>
public class LoginResult(string token)
{
    /// <summary>
    /// Token JWT con las credenciales de la autenticación.
    /// </summary>
    public string Token => token;
}
