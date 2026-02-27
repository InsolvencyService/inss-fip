using INSS.FIP.Data;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using System.Net.Mime;
using System.Net;
using INSS.FIP.Models.CentrallyManagedPartyModels;
using INSS.FIP.Interfaces.CMP;

namespace INSS.FIP.Functions.Functions.CentrallyManagedParty;

public class CMPSyncTimerTrigger
{
    private readonly ILogger<CMPSyncTimerTrigger> _logger;
    private readonly IDbSyncData<CentrallyManagedPartyModel> _dbSyncCMPData;

    public CMPSyncTimerTrigger(ILogger<CMPSyncTimerTrigger> logger,
        IDbSyncData<CentrallyManagedPartyModel> dbSyncCMPData)
    {
        _logger = logger;
        _dbSyncCMPData = dbSyncCMPData;
    }

    [Function("CMPSyncTimerTrigger")]
    [OpenApiOperation(operationId: "CMPSyncTimerTrigger", tags: new[] { "CMPSyncTimerTrigger" }, Summary = "Transfer data from INSSight to Creditor Service.", Description = "Transfer data from INSSight to Creditor Service.", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: MediaTypeNames.Application.Json, bodyType: typeof(IList<CentrallyManagedPartyModel>), Summary = "CMP Synch Timer Trigger", Description = "Transfer data from INSSight to Creditor Service.")]
    [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Summary = "Invalid request/validation failures", Description = "Invalid request/validation failures")]
    [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.InternalServerError, Summary = "Error processing request", Description = "Error processing request")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Required for HttpTrigger signature")]
    public async Task Run([TimerTrigger("%BankruptcyCreditorsSyncSchedulePattern%")] TimerInfo myTimer)
    {
        await _dbSyncCMPData.SynchronizeDataAsync();
    }
}
