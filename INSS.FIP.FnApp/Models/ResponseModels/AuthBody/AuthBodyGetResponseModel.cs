using INSS.FIP.ApiModels.Models.ResponseModels;
using System.Diagnostics.CodeAnalysis;

namespace INSS.FIP.FnApp.Models.ResponseModels.AuthBody;

[ExcludeFromCodeCoverage]
public class AuthBodyGetResponseModel
{
    public IList<FipApiAuthBodyResponseModel> Payload { get; set; } = new List<FipApiAuthBodyResponseModel>();
}
