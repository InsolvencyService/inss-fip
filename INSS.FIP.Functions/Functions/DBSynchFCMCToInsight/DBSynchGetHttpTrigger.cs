using AutoMapper;
using INSS.FIP.Data;
using INSS.FIP.Data.FCMCDataSource;
using INSS.FIP.Interfaces;
using INSS.FIP.Models.ResponseModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Net.Mime;

namespace INSS.FIP.Functions.Functions.DBSynchFCMCToInsight;

public class DBSynchGetHttpTrigger
{
    private readonly ILogger<DBSynchGetHttpTrigger> _logger;
    private readonly IMapper _mapper;
    private readonly IDbSync _dbSync;
    private readonly iirwebdbContext _iirwebdbContext;
    private readonly SourceDbContext _sourceDbContext;

    public DBSynchGetHttpTrigger(   
       ILogger<DBSynchGetHttpTrigger> logger,
       IMapper mapper,
       iirwebdbContext iirwebdbContext,
       SourceDbContext sourceDbContext,
        IDbSync dbSync)
    {
        _logger = logger.ThrowIfNullOrDefault();
        _mapper = mapper;
        _iirwebdbContext = iirwebdbContext;
        _sourceDbContext = sourceDbContext;
        _dbSync = dbSync;
    }

    [Function("DBSynchHttp")]
    [OpenApiOperation(operationId: "DBSynchHttp", tags: new[] { "DBSynchHttp" }, Summary = "Transfer data from fcmc to Insight.", Description = "Transfer data from fcmc to Insight.", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: MediaTypeNames.Application.Json, bodyType: typeof(IList<FipApiSearchResultResponseModel>), Summary = "DB Synch", Description = "Transfer data from fcmc to Insight.")]
    [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Summary = "Invalid request/validation failures", Description = "Invalid request/validation failures")]
    [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.InternalServerError, Summary = "Error processing request", Description = "Error processing request")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Required for HttpTrigger signature")]
    public void Run([Microsoft.Azure.Functions.Worker.HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "DBSynchHttp")] HttpRequest req)
    {
        _dbSync.DBSynchronize();
    }
}