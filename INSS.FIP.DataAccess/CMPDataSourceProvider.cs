using INSS.FIP.Data.CMPDataSource;
using INSS.FIP.Interfaces;
using INSS.FIP.Models.CentrallyManagedParties.ResponseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace INSS.FIP.DataAccess;

public class CMPDataSourceProvider : IDataSourceProvider<CentrallyManagedPartyResponseModel>
{
    private readonly sourceCMPDbContext _sourceDbContext;
    private readonly ILogger<CMPDataSourceProvider> _logger;

    public CMPDataSourceProvider(sourceCMPDbContext sourceCMPDbContext, ILogger<CMPDataSourceProvider> logger)
    {
        _sourceDbContext = sourceCMPDbContext;
        _logger = logger;
    }

    public async Task<List<CentrallyManagedPartyResponseModel>> GetDataFromViewAsync()
    {
        var result = await _sourceDbContext.viewData.AsNoTracking()
            .Select(x => new CentrallyManagedPartyResponseModel
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
            }).ToListAsync();
        return result;
    }
}
