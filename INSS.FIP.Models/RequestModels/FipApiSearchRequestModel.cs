using System.Diagnostics.CodeAnalysis;

namespace INSS.FIP.Models.RequestModels;

[ExcludeFromCodeCoverage]
public class FipApiSearchRequestModel
{
    public string Company { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Town { get; set; }

    public string Postcode { get; set; }
}
