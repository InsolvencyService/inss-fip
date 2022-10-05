using System.Diagnostics.CodeAnalysis;

namespace INSS.FIP.Web.ViewModels;

[ExcludeFromCodeCoverage]
public class PagedViewModel
{
    public int PageNumber { get; set; }

    public int PageSize { get; set; }

    public int TotalPages { get; set; }

    public int TotalResults { get; set; }

    public int SkipCount
    {
        get
        {
            return (PageNumber - 1) * PageSize;
        }
    }

    public PagedViewModel(int pageNumber, int pageSize, int totalResults)
    {
        PageNumber = pageNumber;
        PageSize = pageSize > 0 ? pageSize : totalResults;
        TotalPages = pageSize > 0 ? (totalResults + pageSize) / pageSize : 1;
        TotalResults = totalResults;

        PageNumber = PageNumber < 1 ? 1 : PageNumber > TotalPages ? TotalPages : PageNumber;
    }
}