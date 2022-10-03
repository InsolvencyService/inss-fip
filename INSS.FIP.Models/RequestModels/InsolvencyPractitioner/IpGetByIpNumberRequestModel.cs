using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace INSS.FIP.Models.RequestModels.InsolvencyPractitioner;

[ExcludeFromCodeCoverage]
public class IpGetByIpNumberRequestModel : IValidatableObject
{
    [Required]
    public int? IpNumber { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        bool isValid = false;

        if (IpNumber.HasValue && IpNumber.Value > 0)
        {
            isValid = true;
        }

        if (!isValid)
        {
            yield return new ValidationResult("IP number is not valid");
        }
    }
}
