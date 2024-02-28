using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Configurations;

namespace INSS.FIP.Functions.Configurations;

[ExcludeFromCodeCoverage]
public class OpenApiCustomUIOptions : DefaultOpenApiCustomUIOptions
{
    public OpenApiCustomUIOptions(Assembly assembly) : base(assembly) { }
}