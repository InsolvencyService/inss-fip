using INSS.FIP.Models.DomainModels;

namespace INSS.FIP.Interfaces
{
    public interface IGetWebPageBannerMessageService
    {
        Task<IList<GetWebPageBannerMessageDomainModel>> GetAsync(string applicationPrefix);
    }
}