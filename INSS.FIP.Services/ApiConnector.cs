using INSS.FIP.Interfaces;
using INSS.FIP.Models.ApiConnectorModels;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace INSS.FIP.Services;

public abstract class ApiConnector : IApiConnector
{
    private readonly ILogger<ApiConnector> _logger;
    private readonly HttpClient _httpClient;

    protected ApiConnector(ILogger<ApiConnector> logger, HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
    }

    public async Task<ApiConnectorResponseModel<TApiModel>> ProcessAsync<TApiModel>(ApiConnectorRequestModel apiConnectorRequestModel)
         where TApiModel : class
    {
        _logger.LogInformation("{Method} data from {Uri}", apiConnectorRequestModel.Method, apiConnectorRequestModel.Uri);

        var request = new HttpRequestMessage(apiConnectorRequestModel.Method, apiConnectorRequestModel.Uri);

        request.Headers.Accept.Clear();
        request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(apiConnectorRequestModel.AcceptHeader));

        var result = new ApiConnectorResponseModel<TApiModel>();

        try
        {
            var response = await _httpClient.SendAsync(request);

            result.StatusCode = response.StatusCode;
            result.IsSuccessStatusCode = response.IsSuccessStatusCode;
            result.ErrorReasonPhrase = response.ReasonPhrase;

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Failed to {Method} {AcceptHeader} data from {Uri}, status code: {StatusCode}", apiConnectorRequestModel.Method, apiConnectorRequestModel.AcceptHeader, apiConnectorRequestModel.Uri, response.StatusCode);
            }
            else if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                _logger.LogError("Unexpected status code returned from {Uri}, status code: {StatusCode}", apiConnectorRequestModel.Uri, response.StatusCode);
            }
            else
            {
                string responseString = await response.Content.ReadAsStringAsync();
                result.Payload = JsonConvert.DeserializeObject<TApiModel>(responseString);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error received {Method} {AcceptHeader} data '{Message}'. Received from {Uri}", apiConnectorRequestModel.Method, apiConnectorRequestModel.AcceptHeader, ex.Message ?? ex.InnerException?.Message, apiConnectorRequestModel.Uri);
        }

        return result;
    }
}
