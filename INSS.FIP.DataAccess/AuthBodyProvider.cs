using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using INSS.FIP.Data;
using INSS.FIP.Interfaces;
using INSS.FIP.Models.ResponseModels;
using Microsoft.Extensions.Configuration;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace INSS.FIP.DataAccess;

[ExcludeFromCodeCoverage]
public class AuthBodyProvider : IAuthBodyProvider
{
    private readonly IMapper _mapper;
    private readonly iirwebdbContext _iirwebdbContext;
    private readonly IConfiguration _configuration;

    public AuthBodyProvider(
        IMapper mapper,
        iirwebdbContext iirwebdbContext,
        IConfiguration configuration)
    {
        _mapper = mapper;
        _iirwebdbContext = iirwebdbContext;
        _configuration = configuration;
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

    private IQueryable<Data.FCMCDataSource.FindIpAuthBody> BaseQueryFCMCView
    {
        get
        {
            var query = from a in _iirwebdbContext.FindIpAuthBodies
                        select a;

            return query;
        }
    }

    public async Task<IList<FipApiAuthBodyResponseModel>> GetAsync()
    {
        var results = new List<FipApiAuthBodyResponseModel>();

        if (Convert.ToBoolean(_configuration["usingFCMCtableView"]))
        {
            results = (from a in BaseQueryFCMCView
                       orderby a.AuthBodyName
                       select _mapper.Map<FipApiAuthBodyResponseModel>(a)
              ).ToList();
        }
        else 
        {
            results = (from a in BaseQuery
                       orderby a.AuthBodyName
                       select _mapper.Map<FipApiAuthBodyResponseModel>(a)
              ).ToList();
        }

        return await Task.FromResult(results);
    }
}
