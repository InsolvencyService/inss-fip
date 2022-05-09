using INSS.FIP.ApiModels.Models.ResponseModels;
using System.Diagnostics.CodeAnalysis;

namespace INSS.FIP.FnApp.Models.ResponseModels.InsolvencyPractitioner;

[ExcludeFromCodeCoverage]
public class IpGetByIpNumberResponseModel
{
    public FipApiInsolvencyPractitionerResponseModel? Payload { get; set; }
}
