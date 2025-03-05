using INSS.FIP.Models.DomainModels;

namespace INSS.FIP.Interfaces
{
    public interface IWebMessageService
    {
        Task<IList<WebMessageDomainModel>> GetAsync(string applicationPrefix);
    }
}