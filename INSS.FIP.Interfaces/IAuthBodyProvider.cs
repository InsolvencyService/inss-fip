using INSS.FIP.Models.ResponseModels;

namespace INSS.FIP.Interfaces;

public interface IAuthBodyProvider
{
    Task<IList<FipApiAuthBodyResponseModel>> GetAsync();
}
