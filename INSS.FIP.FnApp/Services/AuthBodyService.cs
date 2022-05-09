using AutoMapper;
using INSS.FIP.ApiModels.Models.ResponseModels;
using INSS.FIP.Repository.Models;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace INSS.FIP.FnApp.Services;

[ExcludeFromCodeCoverage]
public class AuthBodyService : IAuthBodyService
{
    private readonly IMapper _mapper;
    private readonly iirwebdbContext _iirwebdbContext;

    public AuthBodyService(
        IMapper mapper,
        iirwebdbContext iirwebdbContext)
    {
        _mapper = mapper.ThrowIfNullOrDefault();
        _iirwebdbContext = iirwebdbContext.ThrowIfNullOrDefault();
    }

    private IQueryable<CiIpAuthorisingBody> BaseQuery
    {
        get
        {
            var query = from a in _iirwebdbContext.CiIpAuthorisingBodies
                        select a;

            return query;
        }
    }

    public async Task<IList<FipApiAuthBodyResponseModel>?> GetAsync()
    {
        var query = BaseQuery;

        var results = (from a in query
                       orderby a.AuthBodyName
                       select _mapper.Map<FipApiAuthBodyResponseModel>(a)
                      ).ToList();

        return await Task.FromResult(results);
    }
}
