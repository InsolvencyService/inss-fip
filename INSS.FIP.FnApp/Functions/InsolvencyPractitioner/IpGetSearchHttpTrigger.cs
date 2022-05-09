using AutoMapper;
using INSS.FIP.ApiModels.Models.RequestModels;
using INSS.FIP.ApiModels.Models.ResponseModels;
using INSS.FIP.FnApp.Helpers;
using INSS.FIP.FnApp.Models.RequestModels.InsolvencyPractitioner;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Globalization;
using System.Net;
using System.Net.Mime;

namespace INSS.FIP.FnApp.Functions.InsolvencyPractitioner;

public class IpGetSearchHttpTrigger
{
    private readonly ILogger<IpGetSearchHttpTrigger> _logger;
    private readonly IMapper _mapper;
    private readonly IInsolvencyPractitionerService _insolvencyPractitionerService;

    public IpGetSearchHttpTrigger(
        ILogger<IpGetSearchHttpTrigger> logger,
        IMapper mapper,
        IInsolvencyPractitionerService insolvencyPractitionerService)
    {
        _logger = logger.ThrowIfNullOrDefault();
        _mapper = mapper.ThrowIfNullOrDefault();
        _insolvencyPractitionerService = insolvencyPractitionerService.ThrowIfNullOrDefault();
    }

    [FunctionName("IpSearch")]
    [OpenApiOperation(operationId: "IpSearch", tags: new[] { "IP" }, Summary = "Finds Insolvency Practitioners using various search clues", Description = "Search using the combination of supplied paramaters.", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiParameter(name: "Company", In = ParameterLocation.Query, Required = false, Type = typeof(string), Explode = false, Summary = "IP's Company name", Description = "Insolvency Practitioner's company name", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiParameter(name: "County", In = ParameterLocation.Query, Required = false, Type = typeof(string), Explode = false, Summary = "IP's County location", Description = "Insolvency Practitioner's County location", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiParameter(name: "FirstName", In = ParameterLocation.Query, Required = false, Type = typeof(string), Explode = false, Summary = "IP's first name", Description = "Insolvency Practitioner's first name", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiParameter(name: "IpNumber", In = ParameterLocation.Query, Required = false, Type = typeof(int), Explode = false, Summary = "IP number", Description = "Insolvency Practitioner's IP number", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiParameter(name: "LastName", In = ParameterLocation.Query, Required = false, Type = typeof(string), Explode = false, Summary = "IP's last name", Description = "Insolvency Practitioner's last name", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiParameter(name: "Town", In = ParameterLocation.Query, Required = false, Type = typeof(string), Explode = false, Summary = "IP's Town location", Description = "Insolvency Practitioner's town location", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: MediaTypeNames.Application.Json, bodyType: typeof(IList<FipApiSearchResultResponseModel>), Summary = "Seach results", Description = "List of Insolvency Practitioners")]
    [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Summary = "Invalid search request/validation failures", Description = "Invalid search request/validation failures")]
    [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.InternalServerError, Summary = "Error processing request", Description = "Error processing request")]
    public async Task<IActionResult> Run(
       [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "IpSearch")] HttpRequest req)
    {
        var searchRequestModel = new FipApiSearchRequestModel
        {
            Company = req.Query[nameof(FipApiSearchRequestModel.Company)].FirstOrDefault(),
            County = req.Query[nameof(FipApiSearchRequestModel.County)].FirstOrDefault(),
            FirstName = req.Query[nameof(FipApiSearchRequestModel.FirstName)].FirstOrDefault(),
            IpNumber = !string.IsNullOrWhiteSpace(req.Query[nameof(FipApiSearchRequestModel.IpNumber)].FirstOrDefault()) ? int.Parse(req.Query[nameof(FipApiSearchRequestModel.IpNumber)].First(), NumberStyles.Integer, new CultureInfo("en-GB")) : (int?)default,
            LastName = req.Query[nameof(FipApiSearchRequestModel.LastName)].FirstOrDefault(),
            Town = req.Query[nameof(FipApiSearchRequestModel.Town)].FirstOrDefault(),
        };

        _logger.LogTrace("Executing search request");

        var ipSearchRequestModel = _mapper.Map<IpSearchRequestModel>(searchRequestModel);
        var validationResults = ValidationHelpers.ValidateModel(ipSearchRequestModel);

        if (validationResults != null && validationResults.Any())
        {
            _logger.LogError("Executed search request, with validation failures. {validationFailures}", validationResults);

            return new BadRequestResult();
        }

        var result = await _insolvencyPractitionerService.SearchAsync(ipSearchRequestModel);

        if (result != null)
        {
            _logger.LogInformation("Executed search request, returning {count} results.", result.Count);

            return new OkObjectResult(result);
        }

        _logger.LogError("Execute search request failed.");

        return new StatusCodeResult(StatusCodes.Status500InternalServerError);
    }
}
