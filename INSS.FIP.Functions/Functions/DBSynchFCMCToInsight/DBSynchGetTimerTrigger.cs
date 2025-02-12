using AutoMapper;
using INSS.FIP.Data;
using INSS.FIP.Data.FCMCDataSource;
using INSS.FIP.Interfaces;
using INSS.FIP.Models.ResponseModels;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Net.Mime;
using TimerTriggerAttribute = Microsoft.Azure.Functions.Worker.TimerTriggerAttribute;

namespace INSS.FIP.Functions.Functions.DBSynchFCMCToInsight;

public class DBSynchGetTimerTrigger
{
    private readonly ILogger<DBSynchGetTimerTrigger> _logger;
    private readonly IMapper _mapper;
    private readonly IDbSync _dbSync;
    private readonly iirwebdbContext _iirwebdbContext;
    private readonly SourceDbContext _sourceDbContext;

    public DBSynchGetTimerTrigger(
       ILogger<DBSynchGetTimerTrigger> logger,
       IMapper mapper,
       IDbSync dbSync,
       iirwebdbContext iirwebdbContext,
       SourceDbContext sourceDbContext
       )
    {
        _logger = logger.ThrowIfNullOrDefault();
        _mapper = mapper;
        _iirwebdbContext = iirwebdbContext;
        _sourceDbContext = sourceDbContext;
        _dbSync = dbSync;
    }


    [Function("DBSynch")]
    [OpenApiOperation(operationId: "DBSynch", tags: new[] { "DBSynch" }, Summary = "Transfer data from fcmc to Insight.", Description = "Transfer data from fcmc to Insight.", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: MediaTypeNames.Application.Json, bodyType: typeof(IList<FipApiSearchResultResponseModel>), Summary = "DB Synch", Description = "Transfer data from fcmc to Insight.")]
    [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Summary = "Invalid request/validation failures", Description = "Invalid request/validation failures")]
    [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.InternalServerError, Summary = "Error processing request", Description = "Error processing request")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Required for HttpTrigger signature")]
    public void Run([TimerTrigger("%CronPattern%")] Microsoft.Azure.Functions.Worker.TimerInfo myTimer)
    //[HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "DBSynch")] HttpRequest req)
    {
        _dbSync.DBSynchronize();
    }
}