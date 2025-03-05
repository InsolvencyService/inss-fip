using System.Net;
using System.Net.Mime;
using INSS.FIP.Interfaces;
using INSS.FIP.Models.RequestModels.InsolvencyPractitioner;
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

namespace INSS.FIP.Functions.Functions.InsolvencyPractitioner;

public class IpGetByIpNumberHttpTrigger
{
    private readonly ILogger<IpGetByIpNumberHttpTrigger> _logger;
    private readonly IInsolvencyPractitionerProvider _insolvencyPractitionerService;

    public IpGetByIpNumberHttpTrigger(
        ILogger<IpGetByIpNumberHttpTrigger> logger,
        IInsolvencyPractitionerProvider insolvencyPractitionerService)
    {
        _logger = logger.ThrowIfNullOrDefault();
        _insolvencyPractitionerService = insolvencyPractitionerService.ThrowIfNullOrDefault();
    }

    [FunctionName("IP")]
    [OpenApiOperation(operationId: "IP", tags: new[] { "IP" }, Summary = "Returns Insolvency Practitioner by their IP number", Description = "Get the IP by their IP number.", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiParameter(name: "ipNumber", In = ParameterLocation.Path, Required = true, Type = typeof(int), Explode = false, Summary = "IP number", Description = "Insolvency Practitioner's IP number", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: MediaTypeNames.Application.Json, bodyType: typeof(FipApiInsolvencyPractitionerResponseModel), Summary = "Success", Description = "An Insolvency Practitioner")]
    [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NoContent, Summary = "No Insolvency Practitioner for IP number found", Description = "No Insolvency Practitioner for IP number found")]
    [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Summary = "Invalid request/validation failures", Description = "Invalid request/validation failures")]
    [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.InternalServerError, Summary = "Error processing request", Description = "Error processing request")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Required for HttpTrigger signature")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "IP/{ipNumber}")] HttpRequest req, int ipNumber)
    {
        _logger.LogTrace("Executing get request for IP Number {ipNumber}.", ipNumber);

        var ipGetByIpNumberRequestModel = new IpGetByIpNumberRequestModel { IpNumber = ipNumber };
        var validationResults = ValidationHelpers.ValidateModel(ipGetByIpNumberRequestModel);

        if (validationResults != null && validationResults.Any())
        {
            _logger.LogError("Executed IP Get by IP number, with validation failures. {validationFailures}", validationResults);

            return new BadRequestResult();
        }

        var result = await _insolvencyPractitionerService.GetByIpNumberAsync(ipGetByIpNumberRequestModel);

        if (result != null)
        {
            _logger.LogInformation("Executed get request, returning details.");

            return new OkObjectResult(result);
        }

        _logger.LogWarning("Executed get request, returning no details.");

        return new NoContentResult();
    }
}
