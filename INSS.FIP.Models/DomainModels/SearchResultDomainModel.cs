using System.Diagnostics.CodeAnalysis;

namespace INSS.FIP.Models.DomainModels;

[ExcludeFromCodeCoverage]
public class SearchResultDomainModel
{
    public int? IpNumber { get; set; }

    public string Title { get; set; }

    public string FirstNames { get; set; }

    public string LastName { get; set; }

    public string Company { get; set; }

    public string Postcode { get; set; }

    public string LicensingBody { get; set; }
}
