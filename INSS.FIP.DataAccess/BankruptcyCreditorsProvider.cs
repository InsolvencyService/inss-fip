using AutoMapper;
using INSS.FIP.Data;
using INSS.FIP.Data.CMPDataSource;
using INSS.FIP.Data.FCMCDataSource;
using INSS.FIP.Interfaces;
using INSS.FIP.Models.CentrallyManagedPartyModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace INSS.FIP.DataAccess;

public class BankruptcyCreditorsProvider : IDataTargetProvider<CentrallyManagedPartyModel>
{
    private readonly targetCMPDbContext _targetCMPDbContext; 
    private readonly ILogger<BankruptcyCreditorsProvider> _logger;
    private readonly IMapper _mapper;

    public BankruptcyCreditorsProvider(targetCMPDbContext targetCMPDbContext, ILogger<BankruptcyCreditorsProvider> logger, IMapper mapper)
    {
        _targetCMPDbContext = targetCMPDbContext;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task TruncateAndInsertAsync(List<CentrallyManagedPartyModel> data)
    {

        var mappedData = _mapper.Map<List<BankruptcyCreditorsList>>(data);
        mappedData.Sort();

        using var transaction = await _targetCMPDbContext.Database.BeginTransactionAsync();

        try
        {
            _logger.LogInformation("Truncating BankruptcyCreditorsList table");
            await _targetCMPDbContext.BankruptcyCreditorsLists.ExecuteDeleteAsync();

            _logger.LogInformation("Inserting {Count} records into BankruptcyCreditorsList table", mappedData.Count);
            await _targetCMPDbContext.BankruptcyCreditorsLists.AddRangeAsync(mappedData);

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
