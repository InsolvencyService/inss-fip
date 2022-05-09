using INSS.FIP.Models.DomainModels;

namespace INSS.FIP.Services;

public interface IAuthBodyService
{
    Task<IList<AuthBodyDomainModel>?> GetAsync();
}
