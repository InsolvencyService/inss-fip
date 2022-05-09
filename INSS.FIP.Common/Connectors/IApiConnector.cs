using INSS.FIP.Common.Models.ApiConnectorModels;

namespace INSS.FIP.Common.Connectors;

public interface IApiConnector
{
    Task<ApiConnectorResponseModel<TApiModel>> ProcessAsync<TApiModel>(ApiConnectorRequestModel apiConnectorRequestModel)
        where TApiModel : class;
}
