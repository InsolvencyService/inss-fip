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
        data.Sort();

        var mappedData = _mapper.Map<List<BankruptcyCreditorsList>>(data);

        //Assign an ID to each record based on its position in the sorted list to maintain consistent ordering in the database
        for (int i=0; i < mappedData.Count; i++)
        {
            mappedData[i].Id = i + 1;
        }

        using var transaction = await _targetCMPDbContext.Database.BeginTransactionAsync();

        try
        {
            await _targetCMPDbContext.BankruptcyCreditorsLists.ExecuteDeleteAsync();
            await _targetCMPDbContext.BankruptcyCreditorsLists.AddRangeAsync(mappedData);

            await _targetCMPDbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw new CMPSyncDatabaseException("Error saving data to BankruptcyCreditorsList table",ex) ;
        }
    }
}
