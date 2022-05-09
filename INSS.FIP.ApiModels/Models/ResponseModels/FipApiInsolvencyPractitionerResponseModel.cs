using System.Diagnostics.CodeAnalysis;

namespace INSS.FIP.ApiModels.Models.ResponseModels;

[ExcludeFromCodeCoverage]
public class FipApiInsolvencyPractitionerResponseModel
{
    public int IpNumber { get; set; }

    public string? Title { get; set; }

    public string? FirstNames { get; set; }

    public string? LastName { get; set; }

    public string? RegisteredFirmName { get; set; }

    public string? RegisteredAddressLine1 { get; set; }

    public string? RegisteredAddressLine2 { get; set; }

    public string? RegisteredAddressLine3 { get; set; }

    public string? RegisteredAddressLine4 { get; set; }

    public string? RegisteredAddressLine5 { get; set; }

    public string? RegisteredPostCode { get; set; }

    public string? Telephone { get; set; }

    public string? Fax { get; set; }

    public string? Email { get; set; }

    public string? LicensingBody { get; set; }
}
