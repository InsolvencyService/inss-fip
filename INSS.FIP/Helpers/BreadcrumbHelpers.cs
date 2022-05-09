using INSS.FIP.ViewModels;

namespace INSS.FIP.Helpers;

public static class BreadcrumbHelpers
{
    public static IList<BreadcrumbItemViewModel> BuildBreadcrumbs(bool showSearch = false, bool showResults = false, bool showIp = false, int? ipNumber = default, string? ipName = default)
    {
        var breadcrumbs = new List<BreadcrumbItemViewModel> {
            new BreadcrumbItemViewModel{ Text = "Home", Href = "/IP" },
        };

        if (showSearch)
        {
            breadcrumbs.Add(new BreadcrumbItemViewModel { Text = "Search", Href = "/IP/Search" });
        }

        if (showResults)
        {
            breadcrumbs.Add(new BreadcrumbItemViewModel { Text = "Search results", Href = "/IP/Results" });
        }

        if (showIp)
        {
            breadcrumbs.Add(new BreadcrumbItemViewModel { Text = ipName ?? "Insolvency practitioner", Href = $"/IP/IP/{ipNumber}" });
        }

        return breadcrumbs;
    }
}
