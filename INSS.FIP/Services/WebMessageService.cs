using AutoMapper;
using INSS.FIP.ApiModels.Models.ResponseModels;
using INSS.FIP.Common.Models.ApiConnectorModels;
using INSS.FIP.Connectors;
using INSS.FIP.Models.DomainModels;

namespace INSS.FIP.Services
{
    public class WebMessageService : IWebMessageService
    {
        private readonly ILogger<WebMessageService> _logger;
        private readonly IFipApiConnector _fipApiConnector;
        private readonly IMapper _mapper;

        public WebMessageService(
            ILogger<WebMessageService> logger,
            IMapper mapper,
            IFipApiConnector fipApiConnector)
        {
            _logger = logger;
            _mapper = mapper;
            _fipApiConnector = fipApiConnector;
        }

        public async Task<IList<WebMessageDomainModel>?> GetAsync(string applicationPrefix)
        {
            var apiConnectorRequestModel = new ApiConnectorRequestModel
            {
                Uri = new Uri($"api/WebMessage/{applicationPrefix}", UriKind.Relative),
            };

            var apiResponse = await _fipApiConnector.ProcessAsync<IList<FipApiWebMessageResponseModel>>(apiConnectorRequestModel);

            if (apiResponse.IsSuccessStatusCode)
            {
                return _mapper.Map<IList<WebMessageDomainModel>>(apiResponse.Payload);
            }

            _logger.LogError("Error response from {Method} API: {Code}, {Reason}", nameof(GetAsync), apiResponse.StatusCode, apiResponse.ErrorReasonPhrase);

            return default;
        }
    }
}
