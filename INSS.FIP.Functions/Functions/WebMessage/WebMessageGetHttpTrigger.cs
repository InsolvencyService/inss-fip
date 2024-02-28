using System.Net;
using System.Net.Mime;
using INSS.FIP.Interfaces;
using INSS.FIP.Models.RequestModels.WebMessage;
using INSS.FIP.Models.ResponseModels;
using INSS.FIP.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace INSS.FIP.Functions.Functions.WebMessage;

public class WebMessageGetHttpTrigger
{
    private readonly ILogger<WebMessageGetHttpTrigger> _logger;
    private readonly IWebMessageProvider _webMessageService;

    public WebMessageGetHttpTrigger(
        ILogger<WebMessageGetHttpTrigger> logger,
        IWebMessageProvider webMessageService)
    {
        _logger = logger.ThrowIfNullOrDefault();
        _webMessageService = webMessageService.ThrowIfNullOrDefault();
    }

    [FunctionName("WebMessage")]
    [OpenApiOperation(operationId: "WebMessage", tags: new[] { "WebMessage" }, Summary = "Gets web messages for an application", Description = "Gets web messages for an application.", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiParameter(name: "applicationPrefix", In = ParameterLocation.Path, Required = false, Type = typeof(string), Explode = false, Summary = "Application prefix", Description = "Application prefix", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: MediaTypeNames.Application.Json, bodyType: typeof(IList<FipApiSearchResultResponseModel>), Summary = "Web messages", Description = "List of Web messages")]
    [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Summary = "Invalid request/validation failures", Description = "Invalid request/validation failures")]
    [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.InternalServerError, Summary = "Error processing request", Description = "Error processing request")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Required for HttpTrigger signature")]
    public async Task<IActionResult> Run(
       [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "WebMessage/{applicationPrefix}")] HttpRequest req, string? applicationPrefix)
    {
        _logger.LogTrace("Executing WebMessage get list");

        var webMessageRequestModel = new WebMessageRequestModel { ApplicationPrefix = applicationPrefix, };
        var validationResults = ValidationHelpers.ValidateModel(webMessageRequestModel);

        if (validationResults != null && validationResults.Any())
        {
            _logger.LogError("Executed WebMessage get list, with validation failures. {validationFailures}", validationResults);

            return new BadRequestResult();
        }

        var result = await _webMessageService.GetAsync(webMessageRequestModel);

        if (result != null)
        {
            _logger.LogInformation("Executed WebMessage get list, returning {count} results.", result.Count);

            return new OkObjectResult(result);
        }

        _logger.LogError("Execute WebMessage get list failed.");

        return new StatusCodeResult(StatusCodes.Status500InternalServerError);
    }
}
