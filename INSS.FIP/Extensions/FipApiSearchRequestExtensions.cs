using INSS.FIP.ApiModels.Models.RequestModels;
using Microsoft.AspNetCore.Http.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace INSS.FIP.Extensions;

[ExcludeFromCodeCoverage]
public static class FipApiSearchRequestExtensions
{
    public static QueryString ToQueryString(this FipApiSearchRequestModel model)
    {
        var qb = new QueryBuilder();

        if (model.IpNumber != null)
        {
            qb.Add(nameof(FipApiSearchRequestModel.IpNumber), $"{model.IpNumber}");
        }

        if (model.Company != null)
        {
            qb.Add(nameof(FipApiSearchRequestModel.Company), $"{model.Company}");
        }

        if (model.County != null)
        {
            qb.Add(nameof(FipApiSearchRequestModel.County), $"{model.County}");
        }

        if (model.FirstName != null)
        {
            qb.Add(nameof(FipApiSearchRequestModel.FirstName), $"{model.FirstName}");
        }

        if (model.LastName != null)
        {
            qb.Add(nameof(FipApiSearchRequestModel.LastName), $"{model.LastName}");
        }

        if (model.Town != null)
        {
            qb.Add(nameof(FipApiSearchRequestModel.Town), $"{model.Town}");
        }

        return qb.ToQueryString();
    }
}