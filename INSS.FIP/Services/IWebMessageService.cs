using INSS.FIP.Models.DomainModels;

namespace INSS.FIP.Services
{
    public interface IWebMessageService
    {
        Task<IList<WebMessageDomainModel>?> GetAsync(string applicationPrefix);
    }
}