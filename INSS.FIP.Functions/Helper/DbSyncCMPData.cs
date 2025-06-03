using AutoMapper;
using INSS.FIP.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace INSS.FIP.Functions.Helper;

public class DbSyncCMPData<TSource, TTarget> : IDbSyncData<TSource, TTarget> where TTarget : IComparable<TTarget>
{
    private readonly IDataSourceProvider<TSource> _sourceProvider;
    private readonly IDataTargetProvider<TTarget> _targetRepository;
    private readonly ILogger<DbSyncCMPData<TSource, TTarget>> _logger;
    private readonly IMapper _mapper;

    public DbSyncCMPData(IDataSourceProvider<TSource> sourceProvider, IDataTargetProvider<TTarget> targetRepository, ILogger<DbSyncCMPData<TSource, TTarget>> logger,
        IMapper mapper)
    {
        _sourceProvider = sourceProvider;
        _targetRepository = targetRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<bool> SynchronizeBankruptcyCreditorsAsync(string orderByColumn)
    {
        var sourceData = await _sourceProvider.GetDataFromViewAsync();

        var mappedData = _mapper.Map<List<TTarget>>(sourceData);

        await _targetRepository.TruncateAndInsertAsync(mappedData);

        return true;
    }
}
