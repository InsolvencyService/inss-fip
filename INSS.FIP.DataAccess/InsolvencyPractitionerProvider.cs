using AutoMapper;
using INSS.FIP.Data;
using INSS.FIP.Interfaces;
using INSS.FIP.Models.RequestModels.InsolvencyPractitioner;
using INSS.FIP.Models.ResponseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace INSS.FIP.DataAccess;

[ExcludeFromCodeCoverage]
public class InsolvencyPractitionerProvider : IInsolvencyPractitionerProvider
{
    private readonly IMapper _mapper;
    private readonly iirwebdbContext _iirwebdbContext;
    private readonly IConfiguration _configuration;

    public InsolvencyPractitionerProvider(
        IMapper mapper,
        iirwebdbContext iirwebdbContext,
        IConfiguration configuration)
    {
        _mapper = mapper;
        _iirwebdbContext = iirwebdbContext;
        _configuration = configuration;
    }

    private IQueryable<CiIp> BaseQuery
    {
        get
        {
            var query = from a in _iirwebdbContext.CiIps
                        where a.IncludeOnInternet == "Y"
                        select a;

            return query;
        }
    }

    private IQueryable<FindIp> BaseQueryFCMCView
    {

        get
        {
            var query = from a in _iirwebdbContext.FindIps
                        where a.IncludeOnInternet == "Yes"
                        select a;

            return query;
        }
    }

    public async Task<IList<FipApiSearchResultResponseModel>> SearchAsync(IpSearchRequestModel ipSearchRequestModel)
    {
        if (Convert.ToBoolean(_configuration["usingFCMCtableView"]))
        {
            var results = await SearchAsyncUsingFCMCView(ipSearchRequestModel, BaseQueryFCMCView);
            return await Task.FromResult(results);

        }
        else
        {
            var results = await SearchAsync(ipSearchRequestModel, BaseQuery);
            return await Task.FromResult(results);
        }
    }
    private async Task<IList<FipApiSearchResultResponseModel>> SearchAsyncUsingFCMCView(IpSearchRequestModel ipSearchRequestModel, IQueryable<FindIp> query)
    {
        if (!string.IsNullOrWhiteSpace(ipSearchRequestModel.LastName))
        {
            query = query.Where(x => x.Surname.StartsWith(ipSearchRequestModel.LastName));
        }
        if (!string.IsNullOrWhiteSpace(ipSearchRequestModel.FirstName))
        {
            query = query.Where(x => x.Forenames.StartsWith(ipSearchRequestModel.FirstName));
        }
        if (!string.IsNullOrWhiteSpace(ipSearchRequestModel.Company))
        {
            query = query.Where(x => x.RegisteredFirmName != null && x.RegisteredFirmName.StartsWith(ipSearchRequestModel.Company));
        }
        if (!string.IsNullOrWhiteSpace(ipSearchRequestModel.Town))
        {
            query = query.Where(x => (x.RegisteredAddressLine3 != null && x.RegisteredAddressLine3.StartsWith(ipSearchRequestModel.Town)) ||
                                     (x.RegisteredAddressLine4 != null && x.RegisteredAddressLine4.StartsWith(ipSearchRequestModel.Town)) ||
                                     (x.RegisteredAddressLine5 != null && x.RegisteredAddressLine5.StartsWith(ipSearchRequestModel.Town)));
        }
        if (!string.IsNullOrWhiteSpace(ipSearchRequestModel.Postcode))
        {
            query = query.Where(x => x.RegisteredPostCode.StartsWith(ipSearchRequestModel.Postcode));
        }

        query = query.OrderBy(o => o.RegisteredFirmName);
       
       var results = await query
            .Select(a => _mapper.Map<FipApiSearchResultResponseModel>(a))
            .ToListAsync(); // Use ToListAsync to fetch results asynchronously

        return results;

    }

    private async Task<IList<FipApiSearchResultResponseModel>> SearchAsync(IpSearchRequestModel ipSearchRequestModel, IQueryable<CiIp> query )
    {
        if (!string.IsNullOrWhiteSpace(ipSearchRequestModel.LastName))
        {
            query = query.Where(x => x.Surname.StartsWith(ipSearchRequestModel.LastName));
        }
        if (!string.IsNullOrWhiteSpace(ipSearchRequestModel.FirstName))
        {
            query = query.Where(x => x.Forenames.StartsWith(ipSearchRequestModel.FirstName));
        }
        if (!string.IsNullOrWhiteSpace(ipSearchRequestModel.Company))
        {
            query = query.Where(x => x.RegisteredFirmName != null && x.RegisteredFirmName.StartsWith(ipSearchRequestModel.Company));
        }
        if (!string.IsNullOrWhiteSpace(ipSearchRequestModel.Town))
        {
            query = query.Where(x => (x.RegisteredAddressLine3 != null && x.RegisteredAddressLine3.StartsWith(ipSearchRequestModel.Town)) ||
                                     (x.RegisteredAddressLine4 != null && x.RegisteredAddressLine4.StartsWith(ipSearchRequestModel.Town)) ||
                                     (x.RegisteredAddressLine5 != null && x.RegisteredAddressLine5.StartsWith(ipSearchRequestModel.Town)));
        }
        if (!string.IsNullOrWhiteSpace(ipSearchRequestModel.Postcode))
        {
            query = query.Where(x => x.RegisteredPostCode.StartsWith(ipSearchRequestModel.Postcode));
        }

        query = query.OrderBy(o => o.RegisteredFirmName);

        var results = await query
             .Select(a => _mapper.Map<FipApiSearchResultResponseModel>(a))
             .ToListAsync(); // Use ToListAsync to fetch results asynchronously

        return results;
    }

    public async Task<FipApiInsolvencyPractitionerWithAuthResponseModel> GetByIpNumberAsync(IpGetByIpNumberRequestModel ipGetByIpNumberRequestModel)
    {
        var ipNumber = ipGetByIpNumberRequestModel.IpNumber;
        var data = await (from cip in _iirwebdbContext.FindIps
                    join cab in _iirwebdbContext.CiIpAuthorisingBodies
                        on cip.LicensingBody equals cab.AuthBodyCode into jab
                        from x in jab.DefaultIfEmpty()
                    where Convert.ToInt32( cip.IpNo ) == ipNumber && cip.IncludeOnInternet == "Y"
                    select new { IP = cip, IpAb = x }).FirstOrDefaultAsync();


        var ip = _mapper.Map<FindIp, FipApiInsolvencyPractitionerResponseModel>(data?.IP);
        var authBody = _mapper.Map<CiIpAuthorisingBody, FipApiAuthBodyResponseModel>(data?.IpAb);

        var result = new FipApiInsolvencyPractitionerWithAuthResponseModel { IP = ip, AuthorisingBody = authBody };

        return result;
    }
}
