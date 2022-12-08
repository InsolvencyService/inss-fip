using INSS.FIP.Web.ViewModels;

namespace INSS.FIP.Web.Helpers;

public static class BreadcrumbHelpers
{
    public static IList<BreadcrumbItemViewModel> BuildBreadcrumbs(bool showSearch = false, bool showResults = false, bool showIp = false, int? ipNumber = default, string? ipName = default)
    {
        var breadcrumbs = new List<BreadcrumbItemViewModel> {
            new BreadcrumbItemViewModel{ Text = "home", Href = "/ip" },
        };

        if (showSearch)
        {
            breadcrumbs.Add(new BreadcrumbItemViewModel { Text = "Search", Href = "/ip/search" });
        }

        if (showResults)
        {
            breadcrumbs.Add(new BreadcrumbItemViewModel { Text = "Search results", Href = "/ip/results" });
        }

        if (showIp)
        {
            breadcrumbs.Add(new BreadcrumbItemViewModel { Text = ipName ?? "Insolvency practitioner", Href = $"/ip/ip/{ipNumber}" });
        }

        return breadcrumbs;
    }
}
