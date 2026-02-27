using AutoMapper;
using INSS.FIP.Data.CMPDataSource;
using INSS.FIP.Data.CMPDataSource.Interfaces;
using INSS.FIP.Interfaces.CMP;
using INSS.FIP.Models.CentrallyManagedPartyModels;
using Microsoft.Extensions.Logging;

namespace INSS.FIP.DataAccess;

public class BankruptcyCreditorsProvider : IDataTargetProvider<CentrallyManagedPartyModel>
{
    private readonly IBankruptcyCreditorsRepository _bankruptcyCreditorsRepo; 
    private readonly ILogger<BankruptcyCreditorsProvider> _logger;
    private readonly IMapper _mapper;

    public BankruptcyCreditorsProvider(IBankruptcyCreditorsRepository bankruptcyCreditorsRepo, ILogger<BankruptcyCreditorsProvider> logger, IMapper mapper)
    {
        _bankruptcyCreditorsRepo = bankruptcyCreditorsRepo;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task TruncateAndInsertAsync(List<CentrallyManagedPartyModel> data)
    {
        data.Sort();

        data = RemoveDuplicates(data);

        var mappedData = _mapper.Map<List<BankruptcyCreditorsList>>(data);

        //Assign an ID to each record based on its position in the sorted list to maintain consistent ordering in the database
        for (int i=0; i < mappedData.Count; i++)
        {
            mappedData[i].Id = i + 1;
        }

        await _bankruptcyCreditorsRepo.BeginTransactionAsync();

        try
        {
            await _bankruptcyCreditorsRepo.DeleteAllBankruptcyCreditorsListAsync();
            await _bankruptcyCreditorsRepo.AddBankruptcyCreditorsListAsync(mappedData);

            await _bankruptcyCreditorsRepo.SaveBankruptcyCreditorsListChangesAsync();
            await _bankruptcyCreditorsRepo.CommitTransactionAsync();
        }
        catch (Exception ex)
        {
            await _bankruptcyCreditorsRepo.RollbackTransactionAsync();
            throw new CMPSyncDatabaseException("Error saving data to BankruptcyCreditorsList table",ex) ;
        }
    }

    private List<CentrallyManagedPartyModel> RemoveDuplicates(List<CentrallyManagedPartyModel> data)
    {
        var dict = new Dictionary<string, CentrallyManagedPartyModel>();

        foreach(var item in data)
        {
            if (!dict.ContainsKey($"{item.SourceRef ?? ""}_{item.Name}"))
            {
                dict.Add($"{item.SourceRef ?? ""}_{item.Name}", item);
            }
        }

        return dict.Values.ToList();
    }
}
