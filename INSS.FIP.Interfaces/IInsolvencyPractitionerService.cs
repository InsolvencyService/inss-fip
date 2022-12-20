using INSS.FIP.Models.DomainModels;
using INSS.FIP.Models.RequestModels;

namespace INSS.FIP.Interfaces;

public interface IInsolvencyPractitionerService
{
    Task<IList<SearchResultDomainModel>> SearchAsync(FipApiSearchRequestModel fipApiSearchRequest);

    Task<InsolvencyPractitionerWithAuthBodyDomainModel> IpGetByIpNumberAsync(int ipNumber);
}
