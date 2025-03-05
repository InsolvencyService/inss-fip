using System.Diagnostics.CodeAnalysis;

namespace INSS.FIP.Models.ResponseModels.InsolvencyPractitioner;

[ExcludeFromCodeCoverage]
public class IpGetByIpNumberResponseModel
{
    public FipApiInsolvencyPractitionerResponseModel Payload { get; set; }
}
