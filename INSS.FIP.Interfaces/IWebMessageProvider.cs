using INSS.FIP.Models.RequestModels.WebMessage;
using INSS.FIP.Models.ResponseModels;

namespace INSS.FIP.Interfaces
{
    public interface IWebMessageProvider
    {
        Task<IList<FipApiWebMessageResponseModel>> GetAsync(WebMessageRequestModel request);
    }
}