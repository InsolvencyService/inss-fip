using AutoMapper;
using INSS.FIP.Interfaces;
using INSS.FIP.Models.ApiConnectorModels;
using INSS.FIP.Models.DomainModels;
using INSS.FIP.Models.ResponseModels;
using Microsoft.Extensions.Logging;

namespace INSS.FIP.Services
{
    public class GetWebPageBannerMessageService : IGetWebPageBannerMessageService
    {
        private readonly ILogger<GetWebPageBannerMessageService> _logger;
        private readonly IFipApiConnector _fipApiConnector;
        private readonly IMapper _mapper;

        public GetWebPageBannerMessageService(
            ILogger<GetWebPageBannerMessageService> logger,
            IMapper mapper,
            IFipApiConnector fipApiConnector)
        {
            _logger = logger;
            _mapper = mapper;
            _fipApiConnector = fipApiConnector;
        }

        public async Task<IList<GetWebPageBannerMessageDomainModel>> GetAsync(string applicationPrefix)
        {
            var apiConnectorRequestModel = new ApiConnectorRequestModel
            {
                Uri = new Uri($"api/GetWebPageBannerMessage/{applicationPrefix}", UriKind.Relative),
            };

            var apiResponse = await _fipApiConnector.ProcessAsync<IList<FipApiGetWebPageBannerMessageResponseModel>>(apiConnectorRequestModel);

            if (apiResponse.IsSuccessStatusCode)
            {
                return _mapper.Map<IList<GetWebPageBannerMessageDomainModel>>(apiResponse.Payload);
            }

            _logger.LogError("Error response from {Method} API: {Code}, {Reason}", nameof(GetAsync), apiResponse.StatusCode, apiResponse.ErrorReasonPhrase);

            return default;
        }
    }
}
