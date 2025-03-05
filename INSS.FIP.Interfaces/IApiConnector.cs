using INSS.FIP.Models.ApiConnectorModels;

namespace INSS.FIP.Interfaces;

public interface IApiConnector
{
    Task<ApiConnectorResponseModel<TApiModel>> ProcessAsync<TApiModel>(ApiConnectorRequestModel apiConnectorRequestModel)
        where TApiModel : class;
}
