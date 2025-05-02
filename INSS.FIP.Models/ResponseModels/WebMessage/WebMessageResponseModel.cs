using System.Diagnostics.CodeAnalysis;

namespace INSS.FIP.Models.ResponseModels.GetWebPageBannerMessage;

[ExcludeFromCodeCoverage]
public class GetWebPageBannerMessageResponseModel 
{
    public IList<FipApiGetWebPageBannerMessageResponseModel> Payload { get; set; } = new List<FipApiGetWebPageBannerMessageResponseModel>();
}
