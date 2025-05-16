using INSS.FIP.Data;
using INSS.FIP.Functions.Helper;
using INSS.FIP.Interfaces;
using INSS.FIP.Models.ResponseModels.CentrallyManagedParties;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace INSS.FIP.Functions.Functions.DBSynchFCMCToInsight;

public class SyncGetHttpTrigger
{
    private readonly ILogger<SyncGetHttpTrigger> _logger;
    private readonly IDbSyncINSSightData<FipApiBankruptcyCreditorsResponseModel, BankruptcyCreditorsList> _dbSyncINSSightData;

    public SyncGetHttpTrigger(ILogger<SyncGetHttpTrigger> logger,
        IDbSyncINSSightData<FipApiBankruptcyCreditorsResponseModel, BankruptcyCreditorsList> dbSyncINSSightData)
    {
        _logger = logger;
        _dbSyncINSSightData = dbSyncINSSightData;
    }

    [Function("SyncGetHttp")]
    [OpenApiOperation(operationId: "SynchGetTimerTrigger", tags: new[] { "SynchGetTimerTrigger" }, Summary = "Transfer data from Insight to bankcruptcy.", Description = "Transfer data from Insight to bankcruptcy.", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: MediaTypeNames.Application.Json, bodyType: typeof(IList<FipApiBankruptcyCreditorsResponseModel>), Summary = "Synch Get Timer Trigger", Description = "Transfer data from Insight to bankcruptcy.")]
    [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Summary = "Invalid request/validation failures", Description = "Invalid request/validation failures")]
    [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.InternalServerError, Summary = "Error processing request", Description = "Error processing request")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Required for HttpTrigger signature")]
    public void Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "SyncGetHttp")] HttpRequest req)
    {
        string orderBy = "Name";
        _dbSyncINSSightData.SynchronizeBankruptcyCreditorsAsync(orderBy);
    }

}
