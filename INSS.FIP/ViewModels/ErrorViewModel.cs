using System.Diagnostics.CodeAnalysis;

namespace INSS.FIP.ViewModels;

[ExcludeFromCodeCoverage]
public class ErrorViewModel
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}