using INSS.FIP.Models.RequestModels.InsolvencyPractitioner;
using INSS.FIP.Models.ResponseModels;

namespace INSS.FIP.Interfaces;

public interface IInsolvencyPractitionerProvider
{
    Task<IList<FipApiSearchResultResponseModel>> SearchAsync(IpSearchRequestModel ipSearchRequestModel);

    Task<FipApiInsolvencyPractitionerResponseModel> GetByIpNumberAsync(IpGetByIpNumberRequestModel ipGetByIpNumberRequestModel);
}
