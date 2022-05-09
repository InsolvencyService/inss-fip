using System.Diagnostics.CodeAnalysis;

namespace INSS.FIP.Common.Models.PollyModels;

[ExcludeFromCodeCoverage]
public class RetryPolicyOptions
{
    public int Count { get; set; } = 3;

    public int BackoffPower { get; set; } = 2;
}
