using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace INSS.FIP.ViewModels;

[ExcludeFromCodeCoverage]
public class SearchParametersViewModel : IValidatableObject
{
    private const string RegExForName = "^[a-zA-Z ]+(([',.\\-][a-zA-Z ])?[a-zA-Z ]*)*$";
    private const string StringLengthValidationError = "{0} is limited to between 1 and {1} characters";
    private const string InvalidCharactersValidationError = "{0} is too long or contains invalid characters";

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
    [RegularExpression(RegExForName, ErrorMessage = InvalidCharactersValidationError)]
    public string? Company { get; set; }

    [DisplayName("Town or City")]
    [StringLength(100, ErrorMessage = StringLengthValidationError)]
    [RegularExpression(RegExForName, ErrorMessage = InvalidCharactersValidationError)]
    public string? Town { get; set; }

    [DisplayName("IP Number")]
    [DataType("int")]
    public int? IpNumber { get; set; }

    [DataType("DropdownList")]
    public string? County { get; set; }

    public SelectList? Counties
    {
        get
        {
            var countyList = new[] {
                "Alderney",
                "Argyll",
                "Argyll and Bute",
                "Avon",
                "Ayrshire",
                "Bedfordshire",
                "Berkshire",
                "Buckinghamshire",
                "Cambridgeshire",
                "Cheshire",
                "Cleveland",
                "Cork",
                "Cornwall",
                "County Down",
                "County Wicklow",
                "Cumbria",
                "Derbyshire",
                "Devon",
                "Dorset",
                "Dumfries and Galloway",
                "East Yorkshire",
                "Essex",
                "Fife",
                "Flintshire",
                "Galway",
                "Glamorgan",
                "Gloucestershire",
                "Grampian",
                "Greater Manchester",
                "Guernsey",
                "Gwent",
                "Gwynedd",
                "Hampshire",
                "Herefordshire",
                "Hertfordshire",
                "Highlands",
                "Hong Kong",
                "Invernesshire",
                "Isle of Lewis",
                "Kent",
                "Lancashire",
                "Leicestershire",
                "Lincolnshire",
                "Lothian",
                "Merseyside",
                "Middlesex",
                "New South Wales",
                "Norfolk",
                "North Lanarkshire",
                "North Yorkshire",
                "Northamptonshire",
                "Nottinghamshire",
                "Oxfordshire",
                "Pembrokeshire",
                "Perthshire",
                "Shropshire",
                "Somerset",
                "South Glamorgan",
                "South Yorkshire",
                "Staffordshire",
                "Strathclyde",
                "Suffolk",
                "Surrey",
                "Sussex",
                "Tayside",
                "Tyne and Wear",
                "Warwickshire",
                "West Lothian",
                "West Midlands",
                "West Yorkshire",
                "Wiltshire",
                "Worcestershire",
            };

            return new SelectList(countyList);
        }
    }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        bool isValid = false;
        var stringValues = new List<string?> { FirstName, LastName, Company, Town, County };

        if (stringValues.Any(a => !string.IsNullOrWhiteSpace(a)))
        {
            isValid = true;
        }

        if (IpNumber.HasValue && IpNumber.Value > 0)
        {
            isValid = true;
        }

        if (!isValid)
        {
            yield return new ValidationResult("No valid search values have been entered");
        }
    }
}
