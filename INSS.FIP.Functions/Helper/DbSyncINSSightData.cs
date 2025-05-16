using AutoMapper;
using INSS.FIP.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace INSS.FIP.Functions.Helper;

public class DbSyncINSSightData<TSource, TTarget> : IDbSyncINSSightData<TSource, TTarget>
{
    private readonly IDataSourceProvider<TSource> _sourceProvider;
    private readonly IDataTargetRepository<TTarget> _targetRepository;
    private readonly ILogger<DbSyncINSSightData<TSource, TTarget>> _logger;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public DbSyncINSSightData(IDataSourceProvider<TSource> sourceProvider, IDataTargetRepository<TTarget> targetRepository, ILogger<DbSyncINSSightData<TSource, TTarget>> logger,
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
        var orderedData = OrderData(mappedData, orderByColumn);

        await _targetRepository.TruncateAndInsertAsync(orderedData);

        _logger.LogInformation("Successfully synchronized {Count} records for source: {SourceIdentifier}", mappedData.Count, viewName);
        return true;
    }

    private List<TTarget> OrderData(List<TTarget> data, string orderByColumn)
    {
        if (string.IsNullOrWhiteSpace(orderByColumn))
        {
            return data;
        }

        var parameter = Expression.Parameter(typeof(TTarget), "x");
        var property = Expression.Property(parameter, orderByColumn);
        var lambda = Expression.Lambda<Func<TTarget, object>>(Expression.Convert(property, typeof(object)), parameter);
        return data.OrderBy(lambda.Compile()).ToList();
    }
}
