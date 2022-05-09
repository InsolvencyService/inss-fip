using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace INSS.FIP.Common.Models.ApiConnectorModels;

[ExcludeFromCodeCoverage]
public class ApiConnectorResponseModel<TModel>
    where TModel : class
{
    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.InternalServerError;

    public bool IsSuccessStatusCode { get; set; }

    public string? ErrorReasonPhrase { get; set; }

    public Uri? RedirectUri { get; set; }

    public TModel? Payload { get; set; }
}
