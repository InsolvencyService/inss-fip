using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace INSS.FIP.Web.ViewModels;

[ExcludeFromCodeCoverage]
public class InsolvencyPractitionerViewModel
{
    public IEnumerable<BreadcrumbItemViewModel>? Breadcrumbs { get; set; }

    public int PageNumber { get; set; }

    [DisplayName("IP Number")]
    public int? IpNumber { get; set; }

    public string? Name { get; set; }

    public string? Company { get; set; }

    public string? Town { get; set; }
    public string? Address { get; set; }

    public string? Telephone { get; set; }

    public string? Fax { get; set; }

    public string? Email { get; set; }

    [DisplayName("Authorising body")]
    public string? LicensingBody { get; set; }
}
