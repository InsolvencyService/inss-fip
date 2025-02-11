using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using INSS.FIP.Data;
using INSS.FIP.Data.FCMCDataSource;
using INSS.FIP.Functions.Functions.InsolvencyPractitioner;
//using INSS.FIP.Functions.Functions.InsolvencyPractitioner;
using INSS.FIP.Models.ResponseModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
//using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Extensions;
using Microsoft.EntityFrameworkCore;
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
    private readonly iirwebdbContext _iirwebdbContext;
    private readonly SourceDbContext _sourceDbContext;

    public DBSynchGetTimerTrigger(
       ILogger<DBSynchGetTimerTrigger> logger,
       IMapper mapper,
       iirwebdbContext iirwebdbContext,
       SourceDbContext sourceDbContext)
    {
        _logger = logger.ThrowIfNullOrDefault();
        _mapper = mapper;
        _iirwebdbContext = iirwebdbContext;
        _sourceDbContext = sourceDbContext;
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
        DBSynch();
    }

    private void DBSynch()
    {
        Console.WriteLine("DBSynch Triger start");

        SynchronizeFindIpsDataAsync();
        SynchronizeFindIPAuthBodyDataAsync();

        Console.WriteLine("DBSynch Triger End");
    }
    public async Task SynchronizeFindIpsDataAsync()
    {
        using (var transaction = await _iirwebdbContext.Database.BeginTransactionAsync())
        {
            try
            {
                var viewData = await _sourceDbContext.vw_FindIps.ToListAsync(CancellationToken.None);

                var uniqueViewData = viewData
                .GroupBy(x => x.IpNo)
                .Select(g => g.First())
                .Where(x => !string.IsNullOrEmpty(x.IpNo)) // Ensure IpNo is not null or empty
                .ToList();

                var mappedData = _mapper.Map<List<FindIp>>(uniqueViewData);
                //foreach (var item in mappedData)
                //{
                //    if (item.IpNo == null) // Check for nulls
                //    {
                //        // Handle or log the null case
                //        _logger.LogTrace("Mapped data contains a record with NULL IpNo");

                //    }
                //}
                var existingRecords = await _iirwebdbContext.FindIps.ToListAsync();
                _iirwebdbContext.FindIps.RemoveRange(existingRecords);

                await _iirwebdbContext.FindIps.AddRangeAsync(mappedData);

                await _iirwebdbContext.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: Inside SynchronizeFindIpsDataAsync");
                Console.WriteLine(ex);
                await transaction.RollbackAsync();
                throw;
            }
        }
    }

    public async Task SynchronizeFindIPAuthBodyDataAsync()
    {
        using (var transaction = await _iirwebdbContext.Database.BeginTransactionAsync())
        {
            try
            {
                var viewData = await _sourceDbContext.vw_findipauthbodies.ToListAsync();

                var mappedData = _mapper.Map<List<FindIpAuthBody>>(viewData);
                var i = 0;
                foreach (var item in mappedData)
                {
                    if (item.AuthBodyCode == null) // Check for nulls
                    {
                        // Handle or log the null case
                        item.AuthBodyCode = "null" + i;
                        _logger.LogTrace("Mapped data contains a record with NULL AuthBodyCode");
                        i++;
                    }
                }
                var existingRecords = await _iirwebdbContext.FindIpAuthBodies.ToListAsync();
                _iirwebdbContext.FindIpAuthBodies.RemoveRange(existingRecords);

                await _iirwebdbContext.FindIpAuthBodies.AddRangeAsync(mappedData);

                await _iirwebdbContext.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: Inside SynchronizeFindIPAuthBodyDataAsync");
                Console.WriteLine(ex);
                await transaction.RollbackAsync();
                throw;
            }
        }
    }

}