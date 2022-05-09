using AutoMapper;
using INSS.FIP.ApiModels.Models.ResponseModels;
using INSS.FIP.FnApp.Models.RequestModels.WebMessage;
using INSS.FIP.Repository.Models;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace INSS.FIP.FnApp.Services;

[ExcludeFromCodeCoverage]
public class WebMessageService : IWebMessageService
{
    private readonly IMapper _mapper;
    private readonly iirwebdbContext _iirwebdbContext;

    public WebMessageService(
        IMapper mapper,
        iirwebdbContext iirwebdbContext)
    {
        _mapper = mapper.ThrowIfNullOrDefault();
        _iirwebdbContext = iirwebdbContext.ThrowIfNullOrDefault();
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

    public async Task<IList<FipApiWebMessageResponseModel>?> GetAsync(WebMessageRequestModel request)
    {
        var query = BaseQuery;

        var results = (from a in query
                       where a.Application.StartsWith(request.ApplicationPrefix!)
                       select _mapper.Map<FipApiWebMessageResponseModel>(a)
                      ).ToList();

        return await Task.FromResult(results);
    }
}
