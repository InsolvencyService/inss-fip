using INSS.FIP.ApiModels.Models.ResponseModels;
using INSS.FIP.FnApp.Models.RequestModels.InsolvencyPractitioner;

namespace INSS.FIP.FnApp;

public interface IInsolvencyPractitionerService
{
    Task<IList<FipApiSearchResultResponseModel>?> SearchAsync(IpSearchRequestModel ipSearchRequestModel);

    Task<FipApiInsolvencyPractitionerResponseModel?> GetByIpNumberAsync(IpGetByIpNumberRequestModel ipGetByIpNumberRequestModel);
}
