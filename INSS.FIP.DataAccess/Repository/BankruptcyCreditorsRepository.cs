using AutoMapper;
using INSS.FIP.Data;
using INSS.FIP.Data.CMPDataSource;
using INSS.FIP.Data.FCMCDataSource;
using INSS.FIP.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace INSS.FIP.DataAccess.Repository;

public class BankruptcyCreditorsRepository : IDataTargetProvider<BankruptcyCreditorsList>
{
    private readonly targetCMPDbContext _targetCMPDbContext; 
    private readonly ILogger<BankruptcyCreditorsRepository> _logger;
    private readonly IMapper _mapper;

    public BankruptcyCreditorsRepository(targetCMPDbContext targetCMPDbContext, ILogger<BankruptcyCreditorsRepository> logger, IMapper mapper)
    {
        _targetCMPDbContext = targetCMPDbContext;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task TruncateAndInsertAsync(List<BankruptcyCreditorsList> data)
    {
        using var transaction = await _targetCMPDbContext.Database.BeginTransactionAsync();

        var orderedData = data;
        orderedData.Sort();

        try
        {
            _logger.LogInformation("Truncating BankruptcyCreditorsList table");
            await _targetCMPDbContext.BankruptcyCreditorsLists.ExecuteDeleteAsync();

            _logger.LogInformation("Inserting {Count} records into BankruptcyCreditorsList table", orderedData.Count);
            await _targetCMPDbContext.BankruptcyCreditorsLists.AddRangeAsync(orderedData);

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
