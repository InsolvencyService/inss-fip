using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace INSS.FIP.Services;

[ExcludeFromCodeCoverage]
public static class ValidationHelpers
{
    public static List<ValidationResult> ValidateModel<TModel>(TModel model)
        where TModel : class, new()
    {
        var vr = new List<ValidationResult>();
        var vc = new ValidationContext(model);
        Validator.TryValidateObject(model, vc, vr, true);

        return vr;
    }
}
