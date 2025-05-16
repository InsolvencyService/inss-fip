using INSS.FIP.Data;
using INSS.FIP.Data.CMPDataSource;
using INSS.FIP.Interfaces;
using INSS.FIP.Models.ResponseModels.CentrallyManagedParties;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INSS.FIP.DataAccess;

public class INSSightDataSourceProvider : IDataSourceProvider<FipApiBankruptcyCreditorsResponseModel>
{
    private readonly sourceCMPDbContext _sourceDbContext;
    private readonly ILogger<INSSightDataSourceProvider> _logger;

    public INSSightDataSourceProvider(sourceCMPDbContext sourceCMPDbContext, ILogger<INSSightDataSourceProvider> logger)
    {
        _sourceDbContext = sourceCMPDbContext;
        _logger = logger;
    }

    public async Task<List<FipApiBankruptcyCreditorsResponseModel>> GetDataFromViewAsync()
    {
        var result = await _sourceDbContext.viewData.AsNoTracking()
            .Select(x => new FipApiBankruptcyCreditorsResponseModel
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
