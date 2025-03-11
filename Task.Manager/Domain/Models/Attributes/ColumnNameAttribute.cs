namespace TaskProject.Manager.Domain.Models.Attributes;

/// <summary>
/// Etiqueta con el nombre de una columna en la capa de datos cuya propiedad será mapeada.
/// </summary>
/// <param name="name">Nombre de la columna</param>
[AttributeUsage(AttributeTargets.Property)]
public class ColumnNameAttribute(string name) : Attribute
{
    /// <summary>
    /// Nombre de la columna.
    /// </summary>
    public string Name => name;
}
