using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using INSS.FIP.Data;
using INSS.FIP.Interfaces;
using INSS.FIP.Models.RequestModels.WebMessage;
using INSS.FIP.Models.ResponseModels;

namespace INSS.FIP.DataAccess;

[ExcludeFromCodeCoverage]
public class WebMessageProvider : IWebMessageProvider
{
    private readonly IMapper _mapper;
    private readonly iirwebdbContext _iirwebdbContext;

    public WebMessageProvider(
        IMapper mapper,
        iirwebdbContext iirwebdbContext)
    {
        _mapper = mapper;
        _iirwebdbContext = iirwebdbContext;
    }

    private IQueryable<WebMessage> BaseQuery
    {
        get
        {
            var query = from a in _iirwebdbContext.WebMessages
                        select a;

            return query;
        }
    }

    public async Task<IList<FipApiWebMessageResponseModel>> GetAsync(WebMessageRequestModel request)
    {
        var query = BaseQuery;

        var results = (from a in query
                       where a.Application.StartsWith(request.ApplicationPrefix!)
                       select _mapper.Map<FipApiWebMessageResponseModel>(a)
                      ).ToList();

        return await Task.FromResult(results);
    }
}
