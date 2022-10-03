using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using INSS.FIP.Data;
using INSS.FIP.Interfaces;
using INSS.FIP.Models.ResponseModels;

namespace INSS.FIP.DataAccess;

[ExcludeFromCodeCoverage]
public class AuthBodyProvider : IAuthBodyProvider
{
    private readonly IMapper _mapper;
    private readonly iirwebdbContext _iirwebdbContext;

    public AuthBodyProvider(
        IMapper mapper,
        iirwebdbContext iirwebdbContext)
    {
        _mapper = mapper;
        _iirwebdbContext = iirwebdbContext;
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

    public async Task<IList<FipApiAuthBodyResponseModel>> GetAsync()
    {
        var query = BaseQuery;

        var results = (from a in query
                       orderby a.AuthBodyName
                       select _mapper.Map<FipApiAuthBodyResponseModel>(a)
                      ).ToList();

        return await Task.FromResult(results);
    }
}
