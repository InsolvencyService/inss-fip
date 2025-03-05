using System.Diagnostics.CodeAnalysis;

namespace INSS.FIP.Web.ViewModels;

[ExcludeFromCodeCoverage]
public class SearchResultsViewModel
{
    public IEnumerable<BreadcrumbItemViewModel>? Breadcrumbs { get; set; }

    public PagedViewModel? Paged { get; set; }

    public IEnumerable<SearchResultViewModel>? SearchResults { get; set; }
}
