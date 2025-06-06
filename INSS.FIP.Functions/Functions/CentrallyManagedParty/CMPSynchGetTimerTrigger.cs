using INSS.FIP.Data;
using INSS.FIP.Interfaces;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using System.Net.Mime;
using System.Net;
using INSS.FIP.Models.CentrallyManagedPartyModels;

namespace INSS.FIP.Functions.Functions.CentrallyManagedParty;

public class CMPSynchGetTimerTrigger
{
    private readonly ILogger<CMPSynchGetTimerTrigger> _logger;
    private readonly IDbSyncData<CentrallyManagedPartyModel, BankruptcyCreditorsList> _dbSyncCMPData;

    public CMPSynchGetTimerTrigger(ILogger<CMPSynchGetTimerTrigger> logger,
        IDbSyncData<CentrallyManagedPartyModel, BankruptcyCreditorsList> dbSyncCMPData)
    {
        _logger = logger;
        _dbSyncCMPData = dbSyncCMPData;
    }

    [Function("CMPSynchGetTimerTrigger")]
    [OpenApiOperation(operationId: "CMPSynchGetTimerTrigger", tags: new[] { "CMPSynchGetTimerTrigger" }, Summary = "Transfer data from Insight to bankcruptcy.", Description = "Transfer data from Insight to bankcruptcy.", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: MediaTypeNames.Application.Json, bodyType: typeof(IList<CentrallyManagedPartyModel>), Summary = "CMP Synch Get Timer Trigger", Description = "Transfer data from Insight to bankcruptcy.")]
    [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Summary = "Invalid request/validation failures", Description = "Invalid request/validation failures")]
    [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.InternalServerError, Summary = "Error processing request", Description = "Error processing request")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Required for HttpTrigger signature")]
    public async Task Run([TimerTrigger("%BankruptcyCreditorsSyncSchedulePattern%")] TimerInfo myTimer)
    {
        string orderBy = "Name";
        await _dbSyncCMPData.SynchronizeBankruptcyCreditorsAsync(orderBy);
    }
}
