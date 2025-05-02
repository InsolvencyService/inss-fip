using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using INSS.FIP.Data;
using INSS.FIP.Interfaces;
using INSS.FIP.Models.RequestModels.GetWebPageBannerMessage;
using INSS.FIP.Models.ResponseModels;

namespace INSS.FIP.DataAccess;

[ExcludeFromCodeCoverage]
public class GetWebPageBannerMessageProvider : IGetWebPageBannerMessageProvider
{
    private readonly IMapper _mapper;
    private readonly iirwebdbContext _iirwebdbContext;

    public GetWebPageBannerMessageProvider(
        IMapper mapper,
        iirwebdbContext iirwebdbContext)
    {
        _mapper = mapper;
        _iirwebdbContext = iirwebdbContext;
    }

    private IQueryable<GetWebPageBannerMessage> BaseQuery
    {
        get
        {
            var query = from a in _iirwebdbContext.GetWebPageBannerMessages
                        select a;

            return query;
        }
    }

    public async Task<IList<FipApiGetWebPageBannerMessageResponseModel>> GetAsync(GetWebPageBannerMessageRequestModel request)
    {
        var query = BaseQuery;

        var results = (from a in query
                       where a.Application.StartsWith(request.ApplicationPrefix!)
                       select _mapper.Map<FipApiGetWebPageBannerMessageResponseModel>(a)
                      ).ToList();

        return await Task.FromResult(results);
    }
}
