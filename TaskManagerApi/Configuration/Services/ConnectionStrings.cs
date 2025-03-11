using TaskProject.Manager.Domain.Interfaces;

namespace TaskProject.ManagerApi.Configuration.Services;

/// <summary/>
public class ConnectionStrings : IConnectionStrings
{
    /// <inheritdoc/>
    public required string ConnectionStringTasks { get; set; }
}
