using INSS.FIP.Models.DomainModels;

namespace INSS.FIP.Interfaces;

public interface IAuthBodyService
{
    Task<IList<AuthBodyDomainModel>> GetAsync();
}
