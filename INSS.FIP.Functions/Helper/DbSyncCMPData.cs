using AutoMapper;
using INSS.FIP.Interfaces.CMP;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace INSS.FIP.Functions.Helper;

public class DbSyncCMPData<TData> : IDbSyncData<TData>
{
    private readonly IDataSourceProvider<TData> _sourceProvider;
    private readonly IDataTargetProvider<TData> _targetProvider;
    private readonly ILogger<DbSyncCMPData<TData>> _logger;

    public DbSyncCMPData(IDataSourceProvider<TData> sourceProvider, IDataTargetProvider<TData> targetProvider, ILogger<DbSyncCMPData<TData>> logger)
    {
        _sourceProvider = sourceProvider;
        _targetProvider = targetProvider;
        _logger = logger;
    }

    public async Task<bool> SynchronizeDataAsync()
    {
        var sourceData = await _sourceProvider.GetDataFromViewAsync();

        await _targetProvider.TruncateAndInsertAsync(sourceData);

        return true;
    }
}
