using AutoMapper;
using INSS.FIP.Models.ResponseModels;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using INSS.FIP.Models.ResponseModels.CentrallyManagedParties;
using INSS.FIP.Interfaces;
using INSS.FIP.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace INSS.FIP.Functions.Functions.DBSynchFCMCToInsight;

public class SynchGetTimerTrigger
{
    private readonly ILogger<SynchGetTimerTrigger> _logger;
    private readonly IDbSyncINSSightData<FipApiBankruptcyCreditorsResponseModel, BankruptcyCreditorsList> _dbSyncINSSightData;
    private readonly IConfiguration _configuration;

    public SynchGetTimerTrigger(ILogger<SynchGetTimerTrigger> logger,
        IDbSyncINSSightData<FipApiBankruptcyCreditorsResponseModel, BankruptcyCreditorsList> dbSyncINSSightData, IConfiguration configuration)
    {
        _logger = logger;
        _dbSyncINSSightData = dbSyncINSSightData;
        _configuration = configuration;
    }

    [Function("SynchGetTimerTrigger")]
    [OpenApiOperation(operationId: "SynchGetTimerTrigger", tags: new[] { "SynchGetTimerTrigger" }, Summary = "Transfer data from Insight to bankcruptcy.", Description = "Transfer data from Insight to bankcruptcy.", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: MediaTypeNames.Application.Json, bodyType: typeof(IList<FipApiBankruptcyCreditorsResponseModel>), Summary = "Synch Get Timer Trigger", Description = "Transfer data from Insight to bankcruptcy.")]
    [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Summary = "Invalid request/validation failures", Description = "Invalid request/validation failures")]
    [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.InternalServerError, Summary = "Error processing request", Description = "Error processing request")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Required for HttpTrigger signature")]
    public async Task Run([TimerTrigger("%BankruptcyCreditorsSyncSchedulePattern%")] TimerInfo myTimer)
    {
        string orderBy = "Name";
        await _dbSyncINSSightData.SynchronizeBankruptcyCreditorsAsync(orderBy);
    }
}
