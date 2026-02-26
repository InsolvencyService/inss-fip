using INSS.FIP.Data.CMPDataSource;
using INSS.FIP.Models.CentrallyManagedPartyModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Data.SqlClient;

using System.Collections.Generic;
using INSS.FIP.Interfaces.CMP;


namespace INSS.FIP.DataAccess;

public class CMPDataSourceProvider : IDataSourceProvider<CentrallyManagedPartyModel>
{
    private readonly SourceCMPDbContext _sourceDbContext;
    private readonly ILogger<CMPDataSourceProvider> _logger;

    public CMPDataSourceProvider(SourceCMPDbContext sourceCMPDbContext, ILogger<CMPDataSourceProvider> logger)
    {
        _sourceDbContext = sourceCMPDbContext;
        _logger = logger;
    }

    public async Task<List<CentrallyManagedPartyModel>> GetDataFromViewAsync()
    {

        List<VwOdsInssightcmp> records = null;

        //Following block will throw a custom exception if configured view doesn't have all properties required by VwOdsInssightcmp.
        //Difficult to unit test due to lack of key and configurable nature of view, tested on actual database.
        try
        {
            records = await _sourceDbContext.viewData.AsNoTracking().ToListAsync();
        }
        catch (SqlException ex)
        {
            throw new CMPSyncDatabaseException($"Error retrieving data from configured source: {_sourceDbContext.GetViewNameWithSchema<VwOdsInssightcmp>()}" +
                $" as schema does not match agreed standard. Following issues detected: {ex.Message}", ex);
        }

        List<CentrallyManagedPartyModel> result = null;

        result = records.Select(x => new CentrallyManagedPartyModel()
        {
            SourceRef = x.SourceRef,
            Name = x.Name,
            AddressLine1 = x.AddressLine1,
            AddressLine2 = x.AddressLine2,
            AddressLine3 = x.AddressLine3,
            Town = x.Town,
            County = x.County,
            PostCode = x.PostCode,
            Country = x.Country
        }).ToList();

        return result;

    }
}
