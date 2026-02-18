using INSS.FIP.Data;
using INSS.FIP.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using System.Net.Mime;
using System.Net;
using INSS.FIP.Models.CentrallyManagedPartyModels;

namespace INSS.FIP.Functions.Functions.CentrallyManagedParty;

public class CMPSyncHttpTrigger
{
    private readonly ILogger<CMPSyncHttpTrigger> _logger;
    private readonly IDbSyncData<CentrallyManagedPartyModel> _dbSyncCMPData;

    public CMPSyncHttpTrigger(ILogger<CMPSyncHttpTrigger> logger,
        IDbSyncData<CentrallyManagedPartyModel> dbSyncCMPData)
    {
        _logger = logger;
        _dbSyncCMPData = dbSyncCMPData;
    }

    [Function("CMPSyncHttp")]
    [OpenApiOperation(operationId: "CMPSyncHttp", tags: new[] { "CMPSyncHttp" }, Summary = "Transfer data from INSSight to Creditor Service.", Description = "Transfer data from INSSight to Creditor Service.", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: MediaTypeNames.Application.Json, bodyType: typeof(IList<CentrallyManagedPartyModel>), Summary = "CMP Sync Http Trigger", Description = "Transfer data from INSSight to Creditor Service.")]
    [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Summary = "Invalid request/validation failures", Description = "Invalid request/validation failures")]
    [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.InternalServerError, Summary = "Error processing request", Description = "Error processing request")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Required for HttpTrigger signature")]
    public void Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "CMPSyncHttp")] HttpRequest req)
    {
        _dbSyncCMPData.SynchronizeBankruptcyCreditorsAsync();
    }
}
