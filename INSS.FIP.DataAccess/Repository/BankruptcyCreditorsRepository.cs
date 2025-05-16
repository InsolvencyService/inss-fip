using INSS.FIP.Data;
using INSS.FIP.Data.CMPDataSource;
using INSS.FIP.Data.FCMCDataSource;
using INSS.FIP.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INSS.FIP.DataAccess.Repository;

public class BankruptcyCreditorsRepository : IDataTargetRepository<BankruptcyCreditorsList>
{
    private readonly targetCMPDbContext _targetCMPDbContext; 
    private readonly ILogger<BankruptcyCreditorsRepository> _logger;

    public BankruptcyCreditorsRepository(targetCMPDbContext targetCMPDbContext, ILogger<BankruptcyCreditorsRepository> logger)
    {
        _targetCMPDbContext = targetCMPDbContext;
        _logger = logger;
    }

    public async Task TruncateAndInsertAsync(List<BankruptcyCreditorsList> data)
    {
        using var transaction = await _targetCMPDbContext.Database.BeginTransactionAsync();
        try
        {
            _logger.LogInformation("Truncating BankruptcyCreditorsList table");
            await _targetCMPDbContext.BankruptcyCreditorsLists.ExecuteDeleteAsync();

            _logger.LogInformation("Inserting {Count} records into BankruptcyCreditorsList table", data.Count);
            await _targetCMPDbContext.BankruptcyCreditorsLists.AddRangeAsync(data);

            await _targetCMPDbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            _logger.LogError(ex, "Error occured during truncate and insert operation. Rolled back transaction");
            throw;
        }
    }
}
