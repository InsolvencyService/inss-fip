using INSS.FIP.ApiModels.Models.ResponseModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Net.Mime;

namespace INSS.FIP.FnApp.Functions.Health;

public class HealthPingGetHttpTrigger
{
    [FunctionName("HealthPing")]
    [OpenApiOperation(operationId: "HealthPing", tags: new[] { "HealthPing" }, Summary = "Returns gealth ping", Description = "Returns health ping.", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.OK,Summary ="OK", Description = "Ping response")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Required for HttpTrigger signature")]
    public static IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Health/Ping")] HttpRequest req)
    {
        return new OkResult();
    }
}
