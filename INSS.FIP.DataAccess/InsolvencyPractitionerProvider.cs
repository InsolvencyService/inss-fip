using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using INSS.FIP.Data;
using INSS.FIP.Interfaces;
using INSS.FIP.Models.RequestModels.InsolvencyPractitioner;
using INSS.FIP.Models.ResponseModels;
using Microsoft.EntityFrameworkCore;

namespace INSS.FIP.DataAccess;

[ExcludeFromCodeCoverage]
public class InsolvencyPractitionerProvider : IInsolvencyPractitionerProvider
{
    private readonly IMapper _mapper;
    private readonly iirwebdbContext _iirwebdbContext;

    public InsolvencyPractitionerProvider(
        IMapper mapper,
        iirwebdbContext iirwebdbContext)
    {
        _mapper = mapper;
        _iirwebdbContext = iirwebdbContext;
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

    public async Task<IList<FipApiSearchResultResponseModel>> SearchAsync(IpSearchRequestModel ipSearchRequestModel)
    {
        var query = BaseQuery;

        if (ipSearchRequestModel.IpNumber.HasValue)
        {
            query = query.Where(x => x.IpNo == ipSearchRequestModel.IpNumber.Value);
        }
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
        if (!string.IsNullOrWhiteSpace(ipSearchRequestModel.County))
        {
            query = query.Where(x => (x.RegisteredAddressLine3 != null && x.RegisteredAddressLine3.StartsWith(ipSearchRequestModel.County)) ||
                                     (x.RegisteredAddressLine4 != null && x.RegisteredAddressLine4.StartsWith(ipSearchRequestModel.County)) ||
                                     (x.RegisteredAddressLine5 != null && x.RegisteredAddressLine5.StartsWith(ipSearchRequestModel.County)));
        }

        query = query.OrderBy(o => o.RegisteredFirmName);

        var results = (from a in query
                       select _mapper.Map<FipApiSearchResultResponseModel>(a)
                      ).ToList();

        return await Task.FromResult(results);
    }

    public async Task<FipApiInsolvencyPractitionerWithAuthResponseModel> GetByIpNumberAsync(IpGetByIpNumberRequestModel ipGetByIpNumberRequestModel)
    {
        var ipNumber = ipGetByIpNumberRequestModel.IpNumber;
        var data = await (from cip in _iirwebdbContext.CiIps
                    join cab in _iirwebdbContext.CiIpAuthorisingBodies
                        on cip.LicensingBody equals cab.AuthBodyCode into jab
                        from x in jab.DefaultIfEmpty()
                    where cip.IpNo == ipNumber && cip.IncludeOnInternet == "Y"
                    select new { IP = cip, IpAb = x }).FirstOrDefaultAsync();


        var ip = _mapper.Map<CiIp, FipApiInsolvencyPractitionerResponseModel>(data?.IP);
        var authBody = _mapper.Map<CiIpAuthorisingBody, FipApiAuthBodyResponseModel>(data?.IpAb);

        var result = new FipApiInsolvencyPractitionerWithAuthResponseModel { IP = ip, AuthorisingBody = authBody };

        return result;
    }
}
