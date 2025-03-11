using System.Reflection;

namespace TaskProject.Manager.Base.Exceptions;

/// <summary/>
public static class ErrorValidator
{
    /// <summary/>
    public static CustomErrorException ThrowInvalidValidator(string message, IEnumerable<ErrorDetail> errors)
    {
        return new(MethodBase.GetCurrentMethod(), message, errors);
    }

    /// <summary/>
    public static CustomErrorException ThrowNotFoundedValidator(string nameValidator)
    {
        return new(MethodBase.GetCurrentMethod(), $"No se encontro un validator para ({nameValidator})");
    }
}