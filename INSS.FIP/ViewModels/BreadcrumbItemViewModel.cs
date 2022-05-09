using System.Diagnostics.CodeAnalysis;

namespace INSS.FIP.ViewModels;

[ExcludeFromCodeCoverage]
public class BreadcrumbItemViewModel
{
    public string Href { get; set; } = string.Empty;

    public string Text { get; set; } = string.Empty;
}
