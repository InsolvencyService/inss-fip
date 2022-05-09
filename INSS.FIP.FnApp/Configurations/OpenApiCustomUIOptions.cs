using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Configurations;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace INSS.FIP.FnApp.Configurations;

[ExcludeFromCodeCoverage]
public class OpenApiCustomUIOptions : DefaultOpenApiCustomUIOptions
{
    public OpenApiCustomUIOptions(Assembly assembly) : base(assembly) { }
}