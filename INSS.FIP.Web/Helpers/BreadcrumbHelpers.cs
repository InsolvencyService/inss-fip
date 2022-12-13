using INSS.FIP.Web.ViewModels;

namespace INSS.FIP.Web.Helpers;

public static class BreadcrumbHelpers
{
    public static IList<BreadcrumbItemViewModel> BuildBreadcrumbs(bool showSearch = false, bool showResults = false, bool showIp = false, int page = 1, int? ipNumber = default, string? ipName = default)
    {
        var breadcrumbs = new List<BreadcrumbItemViewModel> {
            new BreadcrumbItemViewModel{ Text = "Home", Href = "/" },
        };

        if (showSearch)
        {
            breadcrumbs.Add(new BreadcrumbItemViewModel { Text = "Search", Href = "/search" });
        }

        if (showResults)
        {
            breadcrumbs.Add(new BreadcrumbItemViewModel { Text = "Search results", Href = $"/search-results/{page}" });
        }

        if (showIp)
        {
            breadcrumbs.Add(new BreadcrumbItemViewModel { Text = ipName ?? "Insolvency practitioner", Href = $"/insolvency-practitioner-details/{ipNumber}/{page}" });
        }

        return breadcrumbs;
    }
}