using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace INSS.FIP.ViewModels;

[ExcludeFromCodeCoverage]
public class SearchResultViewModel
{
    [DisplayName("IP Number")]
    public int? IpNumber { get; set; }

    public string? Name { get; set; }

    public string? Company { get; set; }

    [DisplayName("Post code")]
    public string? Postcode { get; set; }

    [DisplayName("Authorising body")]
    public string? LicensingBody { get; set; }
}
