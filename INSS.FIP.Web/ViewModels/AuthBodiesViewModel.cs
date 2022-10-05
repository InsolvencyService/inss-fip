using System.Diagnostics.CodeAnalysis;

namespace INSS.FIP.Web.ViewModels;

[ExcludeFromCodeCoverage]
public class AuthBodiesViewModel
{
    public IEnumerable<BreadcrumbItemViewModel>? Breadcrumbs { get; set; }

    public IEnumerable<AuthBodyViewModel>? AuthBodies { get; set; }
}
