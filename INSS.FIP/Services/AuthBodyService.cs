using AutoMapper;
using INSS.FIP.ApiModels.Models.ResponseModels;
using INSS.FIP.Common.Models.ApiConnectorModels;
using INSS.FIP.Connectors;
using INSS.FIP.Models.DomainModels;

namespace INSS.FIP.Services;

public class AuthBodyService : IAuthBodyService
{
    private readonly ILogger<AuthBodyService> _logger;
    private readonly IMapper _mapper;
    private readonly IFipApiConnector _fipApiConnector;

    public AuthBodyService(
        ILogger<AuthBodyService> logger,
        IMapper mapper,
        IFipApiConnector fipApiConnector)
    {
        _logger = logger;
        _mapper = mapper;
        _fipApiConnector = fipApiConnector;
    }

    public async Task<IList<AuthBodyDomainModel>?> GetAsync()
    {
        var apiConnectorRequestModel = new ApiConnectorRequestModel
        {
            Uri = new Uri("api/AuthBody", UriKind.Relative),
        };

        var apiResponse = await _fipApiConnector.ProcessAsync<IList<FipApiAuthBodyResponseModel>>(apiConnectorRequestModel);

        if (apiResponse.IsSuccessStatusCode)
        {
            return _mapper.Map<IList<AuthBodyDomainModel>>(apiResponse.Payload);
        }

        _logger.LogError("Error response from {Method} API: {Code}, {Reason}", nameof(GetAsync), apiResponse.StatusCode, apiResponse.ErrorReasonPhrase);

        return default;
    }
}
