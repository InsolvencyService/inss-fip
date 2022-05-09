using INSS.FIP.ApiModels.Models.ResponseModels;
using INSS.FIP.FnApp.Models.RequestModels.WebMessage;

namespace INSS.FIP.FnApp.Services
{
    public interface IWebMessageService
    {
        Task<IList<FipApiWebMessageResponseModel>?> GetAsync(WebMessageRequestModel request);
    }
}