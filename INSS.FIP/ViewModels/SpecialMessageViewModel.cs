using System.Diagnostics.CodeAnalysis;

namespace INSS.FIP.ViewModels;

[ExcludeFromCodeCoverage]
public class SpecialMessageViewModel
{
    public bool HideSearch { get; set; }

    public bool ShowMessage { get; set; }

    public string? Message { get; set; }

}
