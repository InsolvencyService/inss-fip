using System.Diagnostics.CodeAnalysis;

namespace INSS.FIP.Common.Models.ClientOptionsModels;

[ExcludeFromCodeCoverage]
public abstract class ClientOptionsModel
{
    public Uri? BaseAddress { get; set; }

    public TimeSpan Timeout { get; set; } = new TimeSpan(0, 0, 60);

    public string? ApiKey { get; set; }
}
