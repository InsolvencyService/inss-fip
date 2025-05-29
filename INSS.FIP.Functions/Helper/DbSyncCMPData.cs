using AutoMapper;
using INSS.FIP.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace INSS.FIP.Functions.Helper;

public class DbSyncCMPData<TSource, TTarget> : IDbSyncData<TSource, TTarget> where TTarget : IComparable<TTarget>
{
    private readonly IDataSourceProvider<TSource> _sourceProvider;
    private readonly IDataTargetRepository<TTarget> _targetRepository;
    private readonly ILogger<DbSyncCMPData<TSource, TTarget>> _logger;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public DbSyncCMPData(IDataSourceProvider<TSource> sourceProvider, IDataTargetRepository<TTarget> targetRepository, ILogger<DbSyncCMPData<TSource, TTarget>> logger,
        IMapper mapper, IConfiguration configuration)
    {
        _sourceProvider = sourceProvider;
        _targetRepository = targetRepository;
        _logger = logger;
        _mapper = mapper;
        _configuration = configuration;
    }

    public async Task<bool> SynchronizeBankruptcyCreditorsAsync(string orderByColumn)
    {
        string viewName = _configuration["CMPDataViewName"]!;
        _logger.LogInformation("Starting synchronization for source: {SourceIdentifier}", viewName);

        var sourceData = await _sourceProvider.GetDataFromViewAsync();

        var mappedData = _mapper.Map<List<TTarget>>(sourceData);

        var orderedData = mappedData;
        orderedData.Sort();

        await _targetRepository.TruncateAndInsertAsync(orderedData);

        _logger.LogInformation("Successfully synchronized {Count} records for source: {SourceIdentifier}", mappedData.Count, viewName);
        return true;
    }
}
