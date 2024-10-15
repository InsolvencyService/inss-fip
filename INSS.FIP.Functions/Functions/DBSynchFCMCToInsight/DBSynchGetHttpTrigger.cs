using AutoMapper;
using INSS.FIP.Data;
using INSS.FIP.Data.FCMCDataSource;
using INSS.FIP.Models.ResponseModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Net.Mime;

namespace INSS.FIP.Functions.Functions.DBSynchFCMCToInsight;

public class DBSynchGetHttpTrigger
{
    private readonly IMapper _mapper;
    private readonly iirwebdbContext _iirwebdbContext;
    private readonly SourceDbContext _sourceDbContext;

    public DBSynchGetHttpTrigger(
       IMapper mapper,
       iirwebdbContext iirwebdbContext,
       SourceDbContext sourceDbContext)
    {
        _mapper = mapper;
        _iirwebdbContext = iirwebdbContext;
        _sourceDbContext = sourceDbContext;
    }



    [FunctionName("DBSynch")]
    [OpenApiOperation(operationId: "DBSynch", tags: new[] { "DBSynch" }, Summary = "Transfer data from fcmc to Insight.", Description = "Transfer data from fcmc to Insight.", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: MediaTypeNames.Application.Json, bodyType: typeof(IList<FipApiSearchResultResponseModel>), Summary = "DB Synch", Description = "Transfer data from fcmc to Insight.")]
    [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Summary = "Invalid request/validation failures", Description = "Invalid request/validation failures")]
    [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.InternalServerError, Summary = "Error processing request", Description = "Error processing request")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Required for HttpTrigger signature")]
    public async Task<IActionResult> Run(
       [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "DBSynch/{applicationPrefix}")] HttpRequest req, string? applicationPrefix)
    {
        await SynchronizeFindIpsDataAsync();
        await SynchronizeFindIPAuthBodyDataAsync();

        return new OkResult();
    }
    public async Task SynchronizeFindIpsDataAsync()
    {
        using (var transaction = await _iirwebdbContext.Database.BeginTransactionAsync())
        {
            try
            {
                var viewData = await _sourceDbContext.vw_FindIps.ToListAsync();

                var uniqueViewData = viewData
                .GroupBy(x => x.IpNo)
                .Select(g => g.First())
                .Where(x => !string.IsNullOrEmpty(x.IpNo)) // Ensure IpNo is not null or empty
                .ToList();

                var mappedData = _mapper.Map<List<FindIp>>(uniqueViewData);
                foreach (var item in mappedData)
                {
                    if (item.IpNo == null) // Check for nulls
                    {
                        // Handle or log the null case
                        Console.WriteLine("Mapped data contains a record with NULL IpNo");
                    }
                }
                var existingRecords = await _iirwebdbContext.FindIps.ToListAsync();
                _iirwebdbContext.FindIps.RemoveRange(existingRecords);

                await _iirwebdbContext.FindIps.AddRangeAsync(mappedData);

                await _iirwebdbContext.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
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
              
                var existingRecords = await _iirwebdbContext.FindIpAuthBodies.ToListAsync();
                _iirwebdbContext.FindIpAuthBodies.RemoveRange(existingRecords);

                await _iirwebdbContext.FindIpAuthBodies.AddRangeAsync(mappedData);

                await _iirwebdbContext.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }

}
