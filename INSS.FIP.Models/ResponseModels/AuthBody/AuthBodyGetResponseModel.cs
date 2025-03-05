using System.Diagnostics.CodeAnalysis;

namespace INSS.FIP.Models.ResponseModels.AuthBody;

[ExcludeFromCodeCoverage]
public class AuthBodyGetResponseModel
{
    public IList<FipApiAuthBodyResponseModel> Payload { get; set; } = new List<FipApiAuthBodyResponseModel>();
}
