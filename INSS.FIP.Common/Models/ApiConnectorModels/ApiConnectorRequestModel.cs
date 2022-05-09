using System.Diagnostics.CodeAnalysis;
using System.Net.Mime;

namespace INSS.FIP.Common.Models.ApiConnectorModels;

[ExcludeFromCodeCoverage]
public class ApiConnectorRequestModel
{
    public HttpMethod Method { get; set; } = HttpMethod.Get;

    public Uri Uri { get; set; } = new Uri("/", UriKind.Relative);

    public string AcceptHeader { get; set; } = MediaTypeNames.Application.Json;
}
