using INSS.FIP.ApiModels.Models.ResponseModels;

namespace INSS.FIP.FnApp;

public interface IAuthBodyService
{
    Task<IList<FipApiAuthBodyResponseModel>?> GetAsync();
}
