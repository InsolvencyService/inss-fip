using System.Diagnostics.CodeAnalysis;

namespace INSS.FIP.ApiModels.Models.RequestModels;

[ExcludeFromCodeCoverage]
public class FipApiSearchRequestModel
{
    public string? Company { get; set; }

    public string? County { get; set; }

    public string? FirstName { get; set; }

    public int? IpNumber { get; set; }

    public string? LastName { get; set; }

    public string? Town { get; set; }
}
