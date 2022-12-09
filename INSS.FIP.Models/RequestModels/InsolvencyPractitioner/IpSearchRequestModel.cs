using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace INSS.FIP.Models.RequestModels.InsolvencyPractitioner;

[ExcludeFromCodeCoverage]
public class IpSearchRequestModel : IValidatableObject
{
    public string Company { get; set; }

    public string County { get; set; }

    public string FirstName { get; set; }

    public int? IpNumber { get; set; }

    public string LastName { get; set; }

    public string Town { get; set; }

    public string Postcode { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        bool isValid = false;
        var stringValues = new List<string> { FirstName, LastName, Company, Town, Postcode };

        if (stringValues.Any(a => !string.IsNullOrWhiteSpace(a)))
        {
            isValid = true;
        }

        if (!isValid)
        {
            yield return new ValidationResult("No valid search values have been entered");
        }
    }
}
