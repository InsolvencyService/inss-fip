using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Configurations;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;
using System.Diagnostics.CodeAnalysis;

namespace INSS.FIP.FnApp.Configurations;

[ExcludeFromCodeCoverage]
public class OpenApiConfigurationOptions : DefaultOpenApiConfigurationOptions
{
    public override OpenApiInfo Info { get; set; } = new OpenApiInfo()
    {
        Version = GetOpenApiDocVersion(),
        Title = GetOpenApiDocTitle(),
        Description = "FIP API - Find Insolvency Practitioner.",
        License = new OpenApiLicense()
        {
            Name = "MIT",
            Url = new Uri("http://opensource.org/licenses/MIT"),
        }
    };

    public override OpenApiVersionType OpenApiVersion { get; set; } = GetOpenApiVersion();
}