using System.Net;
using System.Net.Mime;
using INSS.FIP.Interfaces;
using INSS.FIP.Models.ResponseModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Extensions;
using Microsoft.Extensions.Logging;

namespace INSS.FIP.Functions.Functions.AuthBody;

public class AuthBodyGetHttpTrigger
{
    private readonly ILogger<AuthBodyGetHttpTrigger> _logger;
    private readonly IAuthBodyProvider _authBodyService;

    public AuthBodyGetHttpTrigger(
        ILogger<AuthBodyGetHttpTrigger> logger,
        IAuthBodyProvider authBodyService)
    {
        _logger = logger.ThrowIfNullOrDefault();
        _authBodyService = authBodyService.ThrowIfNullOrDefault();
    }

    [FunctionName("AuthBody")]
    [OpenApiOperation(operationId: "AuthBody", tags: new[] { "AuthBody" }, Summary = "Returns Authorising Bodies", Description = "Returns Authorising Bodies.", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: MediaTypeNames.Application.Json, bodyType: typeof(IList<FipApiAuthBodyResponseModel>), Summary = "Success", Description = "List of Authorising Bodies")]
    [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NoContent, Summary = "No Authorising Bodies found", Description = "No Authorising Bodies found")]
    [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Summary = "Invalid request/validation failures", Description = "Invalid request/validation failures")]
    [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.InternalServerError, Summary = "Error processing request", Description = "Error processing request")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Required for HttpTrigger signature")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "AuthBody")] HttpRequest req)
    {
        _logger.LogTrace("Executing get request for AuthBodies");

        var result = await _authBodyService.GetAsync();

        if (result != null)
        {
            if (result.Any())
            {
                _logger.LogInformation("Executed get request, returning {count} details.", result.Count);

                return new OkObjectResult(result);
            }

            _logger.LogWarning("Executed get request, returning no details.");

            return new NoContentResult();
        }

        _logger.LogError("Execute get request failed.");

        return new StatusCodeResult(StatusCodes.Status500InternalServerError);
    }
}
