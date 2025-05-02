using INSS.FIP.Models.RequestModels.GetWebPageBannerMessage;
using INSS.FIP.Models.ResponseModels;

namespace INSS.FIP.Interfaces
{
    public interface IGetWebPageBannerMessageProvider
    {
        Task<IList<FipApiGetWebPageBannerMessageResponseModel>> GetAsync(GetWebPageBannerMessageRequestModel request);
    }
}