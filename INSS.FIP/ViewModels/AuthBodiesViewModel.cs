using System.Diagnostics.CodeAnalysis;

namespace INSS.FIP.ViewModels;

[ExcludeFromCodeCoverage]
public class AuthBodiesViewModel
{
    public IEnumerable<BreadcrumbItemViewModel>? Breadcrumbs { get; set; }

    public IEnumerable<AuthBodyViewModel>? AuthBodies { get; set; }
}
