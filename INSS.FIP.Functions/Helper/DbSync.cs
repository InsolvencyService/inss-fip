
using AutoMapper;
using INSS.FIP.Data;
using INSS.FIP.Data.FCMCDataSource;
using INSS.FIP.Interfaces;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Extensions;
using Microsoft.Extensions.Logging;

namespace INSS.FIP.Functions.Helper
{
    public class DbSync : IDbSync
    {

        private readonly ILogger<DbSync> _logger;
        private readonly IMapper _mapper;
        private readonly iirwebdbContext _iirwebdbContext;
        private readonly SourceDbContext _sourceDbContext;

        public  DbSync(
           ILogger<DbSync> logger,
           IMapper mapper,
           iirwebdbContext iirwebdbContext,
           SourceDbContext sourceDbContext)
        {
            _logger = logger.ThrowIfNullOrDefault();
            _mapper = mapper;
            _iirwebdbContext = iirwebdbContext;
            _sourceDbContext = sourceDbContext;
        }

        public void DBSynchronize()
        {
            _logger.LogInformation("DBSynch Triger start");

            SynchronizeFindIpsDataAsync();
            SynchronizeFindIPAuthBodyDataAsync();

            _logger.LogInformation("DBSynch Triger End");
        }

        private bool SynchronizeFindIpsDataAsync()
        {
            using (var transaction = _iirwebdbContext.Database.BeginTransaction())
            {
                try
                {
                    var viewData = _sourceDbContext.vw_FindIps.ToList();


                    var mappedData = _mapper.Map<List<FindIp>>(viewData);

                    var existingRecords = _iirwebdbContext.FindIps.ToList();
                    _iirwebdbContext.FindIps.RemoveRange(existingRecords);

                    _iirwebdbContext.FindIps.AddRange(mappedData);

                    _iirwebdbContext.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    _logger.LogError("Exception: Inside SynchronizeFindIpsDataAsync");
                    _logger.LogError(ex.Message);
                    throw;
                }
                return true;
            }
        }

        private bool SynchronizeFindIPAuthBodyDataAsync()
        {
            using (var transaction = _iirwebdbContext.Database.BeginTransaction())
            {
                try
                {
                    var viewData = _sourceDbContext.vw_findipauthbodies.ToList();

                    var mappedData = _mapper.Map<List<FindIpAuthBody>>(viewData);
 
                    var existingRecords = _iirwebdbContext.FindIpAuthBodies.ToList();
                    _iirwebdbContext.FindIpAuthBodies.RemoveRange(existingRecords);
                    _iirwebdbContext.FindIpAuthBodies.AddRange(mappedData);
                    
                    _iirwebdbContext.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    _logger.LogError("Exception: Inside SynchronizeFindIpsDataAsync");
                    _logger.LogError(ex.Message);
                    throw;
                }
            }
            return true ;
        }


    }
}
