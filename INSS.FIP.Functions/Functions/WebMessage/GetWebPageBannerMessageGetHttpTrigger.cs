using System.Net;
using System.Net.Mime;
using INSS.FIP.Interfaces;
using INSS.FIP.Models.RequestModels.GetWebPageBannerMessage;
using INSS.FIP.Models.ResponseModels;
using INSS.FIP.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Extensions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;

namespace INSS.FIP.Functions.Functions.GetWebPageBannerMessage;

public class GetWebPageBannerMessageGetHttpTrigger
{
    private readonly ILogger<GetWebPageBannerMessageGetHttpTrigger> _logger;
    private readonly IGetWebPageBannerMessageProvider _GetWebPageBannerMessageService;

    public GetWebPageBannerMessageGetHttpTrigger(
        ILogger<GetWebPageBannerMessageGetHttpTrigger> logger,
        IGetWebPageBannerMessageProvider GetWebPageBannerMessageService)
    {
        _logger = logger.ThrowIfNullOrDefault();
        _GetWebPageBannerMessageService = GetWebPageBannerMessageService.ThrowIfNullOrDefault();
    }

    //Change name from 'GetWebPageBannerMessage' to 'GetWebPageBannerMessage'
    [Function("GetWebPageBannerMessage")]
    [OpenApiOperation(operationId: "GetWebPageBannerMessage", tags: new[] { "GetWebPageBannerMessage" }, Summary = "Gets web messages for an application", Description = "Gets web messages for an application.", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiParameter(name: "applicationPrefix", In = ParameterLocation.Path, Required = false, Type = typeof(string), Explode = false, Summary = "Application prefix", Description = "Application prefix", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: MediaTypeNames.Application.Json, bodyType: typeof(IList<FipApiSearchResultResponseModel>), Summary = "Web messages", Description = "List of Web messages")]
    [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Summary = "Invalid request/validation failures", Description = "Invalid request/validation failures")]
    [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.InternalServerError, Summary = "Error processing request", Description = "Error processing request")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Required for HttpTrigger signature")]
    public async Task<IActionResult> Run(
       [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetWebPageBannerMessage/{applicationPrefix}")] HttpRequest req, string? applicationPrefix)
    {
        _logger.LogTrace("Executing GetWebPageBannerMessage get list");

        var GetWebPageBannerMessageRequestModel = new GetWebPageBannerMessageRequestModel { ApplicationPrefix = applicationPrefix, };
        var validationResults = ValidationHelpers.ValidateModel(GetWebPageBannerMessageRequestModel);

        if (validationResults != null && validationResults.Any())
        {
            _logger.LogError("Executed GetWebPageBannerMessage get list, with validation failures. {validationFailures}", validationResults);

            return new BadRequestResult();
        }

        var result = await _GetWebPageBannerMessageService.GetAsync(GetWebPageBannerMessageRequestModel);

        if (result != null)
        {
            _logger.LogInformation("Executed GetWebPageBannerMessage get list, returning {count} results.", result.Count);

            return new OkObjectResult(result);
        }

        _logger.LogError("Execute GetWebPageBannerMessage get list failed.");

        return new StatusCodeResult(StatusCodes.Status500InternalServerError);
    }
}
