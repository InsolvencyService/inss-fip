using AutoMapper;
using INSS.FIP.ApiModels.Models.RequestModels;
using INSS.FIP.ApiModels.Models.ResponseModels;
using INSS.FIP.Common.Models.ApiConnectorModels;
using INSS.FIP.Connectors;
using INSS.FIP.Extensions;
using INSS.FIP.Models.DomainModels;

namespace INSS.FIP.Services
{
    public class InsolvencyPractitionerService : IInsolvencyPractitionerService
    {
        private readonly ILogger<InsolvencyPractitionerService> _logger;
        private readonly IFipApiConnector _fipApiConnector;
        private readonly IMapper _mapper;

        public InsolvencyPractitionerService(
            ILogger<InsolvencyPractitionerService> logger,
            IMapper mapper,
            IFipApiConnector fipApiConnector)
        {
            _logger = logger;
            _mapper = mapper;
            _fipApiConnector = fipApiConnector;
        }

        public async Task<IList<SearchResultDomainModel>?> SearchAsync(FipApiSearchRequestModel fipApiSearchRequest)
        {
            var apiConnectorRequestModel = new ApiConnectorRequestModel
            {
                Uri = new Uri("api/IpSearch" + fipApiSearchRequest.ToQueryString(), UriKind.Relative),
            };

            var apiResponse = await _fipApiConnector.ProcessAsync<IList<FipApiSearchResultResponseModel>>(apiConnectorRequestModel);

            if (apiResponse.IsSuccessStatusCode)
            {
                return _mapper.Map<IList<SearchResultDomainModel>>(apiResponse.Payload);
            }

            _logger.LogError("Error response from {Method} API: {Code}, {Reason}", nameof(SearchAsync),apiResponse.StatusCode, apiResponse.ErrorReasonPhrase);

            return default;
        }

        public async Task<InsolvencyPractitionerDomainModel?> IpGetByIpNumberAsync(int ipNumber)
        {
            var apiConnectorRequestModel = new ApiConnectorRequestModel
            {
                Uri = new Uri($"api/Ip/{ipNumber}", UriKind.Relative),
            };
            var apiResponse = await _fipApiConnector.ProcessAsync<FipApiInsolvencyPractitionerResponseModel>(apiConnectorRequestModel);

            if (apiResponse.IsSuccessStatusCode)
            {
                return _mapper.Map<InsolvencyPractitionerDomainModel>(apiResponse.Payload);
            }

            _logger.LogError("Error response from {Method} API: {Code}, {Reason}", nameof(IpGetByIpNumberAsync), apiResponse.StatusCode, apiResponse.ErrorReasonPhrase);

            return default;
        }
    }
}
