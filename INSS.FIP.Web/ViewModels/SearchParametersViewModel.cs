using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace INSS.FIP.Web.ViewModels;

[ExcludeFromCodeCoverage]
public class SearchParametersViewModel : IValidatableObject
{
    private const string RegExForName = "^[a-zA-Z ]+(([',.\\-][a-zA-Z ])?[a-zA-Z ]*)*$";
    private const string RegExForCompanyName = "^[a-zA-Z ]+(([',&.\\-][a-zA-Z ])?[a-zA-Z ]*)*$";
    private const string StringLengthValidationError = "{0} is limited to between 1 and {1} characters";
    private const string InvalidCharactersValidationError = "{0} is too long or contains invalid characters";
    private const string PostcodeValidationError = "Enter a real postcode";

    public IEnumerable<BreadcrumbItemViewModel>? Breadcrumbs { get; set; }

    public string ShowWarningMessage { get; set; } = string.Empty;

    public int PageSize { get; set; } = 10;

    public int PageNumber { get; set; } = 1;

    [Display(Name = "First name")]
    [StringLength(100, ErrorMessage = StringLengthValidationError)]
    [RegularExpression(RegExForName, ErrorMessage = InvalidCharactersValidationError)]
    public string? FirstName { get; set; }

    [Display(Name = "Last name")]
    [StringLength(100, ErrorMessage = StringLengthValidationError)]
    [RegularExpression(RegExForName, ErrorMessage = InvalidCharactersValidationError)]
    public string? LastName { get; set; }

    [StringLength(100, ErrorMessage = StringLengthValidationError)]
    [RegularExpression(RegExForCompanyName, ErrorMessage = InvalidCharactersValidationError)]
    public string? Company { get; set; }

    [DisplayName("Town or City")]
    [StringLength(100, ErrorMessage = StringLengthValidationError)]
    [RegularExpression(RegExForName, ErrorMessage = InvalidCharactersValidationError)]
    public string? Town { get; set; }

    [StringLength(8, ErrorMessage = StringLengthValidationError)]
    [RegularExpression("(?i:^(([A-Z]{1,2}[0-9][A-Z0-9]?|ASCN|STHL|TDCU|BBND|[BFS]IQQ|PCRN|TKCA) ?[0-9][A-Z]{2}|BFPO ?[0-9]{1,4}|(KY[0-9]|MSR|VG|AI)[ -]?[0-9]{4}|[A-Z]{2} ?[0-9]{2}|GE ?CX|GIR ?0A{2}|SAN ?TA1)$)",
        ErrorMessage = PostcodeValidationError)]
    public string? Postcode { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        bool isValid = false;
        var stringValues = new List<string?> { FirstName, LastName, Company, Town, Postcode };

        if (stringValues.Any(a => !string.IsNullOrWhiteSpace(a)))
        {
            isValid = true;
        }

        if (!isValid)
        {
            yield return new ValidationResult("Enter either first name, last name, company, town or city, or postcode");
        }
    }
}
