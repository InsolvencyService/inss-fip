using INSS.FIP.ApiModels.Models.RequestModels;
using INSS.FIP.Models.DomainModels;

namespace INSS.FIP.Services;

public interface IInsolvencyPractitionerService
{
    Task<IList<SearchResultDomainModel>?> SearchAsync(FipApiSearchRequestModel fipApiSearchRequest);

    Task<InsolvencyPractitionerDomainModel?> IpGetByIpNumberAsync(int ipNumber);
}
