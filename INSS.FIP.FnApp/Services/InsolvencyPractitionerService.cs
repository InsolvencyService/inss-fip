using AutoMapper;
using INSS.FIP.ApiModels.Models.ResponseModels;
using INSS.FIP.FnApp.Models.RequestModels.InsolvencyPractitioner;
using INSS.FIP.Repository.Models;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace INSS.FIP.FnApp.Services;

[ExcludeFromCodeCoverage]
public class InsolvencyPractitionerService : IInsolvencyPractitionerService
{
    private readonly IMapper _mapper;
    private readonly iirwebdbContext _iirwebdbContext;

    public InsolvencyPractitionerService(
        IMapper mapper,
        iirwebdbContext iirwebdbContext)
    {
        _mapper = mapper.ThrowIfNullOrDefault();
        _iirwebdbContext = iirwebdbContext.ThrowIfNullOrDefault();
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

    public async Task<IList<FipApiSearchResultResponseModel>?> SearchAsync(IpSearchRequestModel ipSearchRequestModel)
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

    public async Task<FipApiInsolvencyPractitionerResponseModel?> GetByIpNumberAsync(IpGetByIpNumberRequestModel ipGetByIpNumberRequestModel)
    {
        var query = BaseQuery;

        var data = query.FirstOrDefault(f => f.IpNo == ipGetByIpNumberRequestModel.IpNumber);

        var result = _mapper.Map<FipApiInsolvencyPractitionerResponseModel>(data);

        return await Task.FromResult(result);
    }
}
