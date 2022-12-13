using AutoMapper;
using INSS.FIP.Interfaces;
using INSS.FIP.Models.RequestModels;
using INSS.FIP.Web.Constants;
using INSS.FIP.Web.Extensions;
using INSS.FIP.Web.Helpers;
using INSS.FIP.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace INSS.FIP.Web.Controllers;

public class IpController : Controller
{
    private readonly ILogger<IpController> _logger;
    private readonly IMapper _mapper;
    private readonly IInsolvencyPractitionerService _insolvencyPractitionerService;
    private readonly IWebMessageService _webMessageService;

    public IpController(ILogger<IpController> logger, 
        IMapper mapper, 
        IInsolvencyPractitionerService insolvencyPractitionerService,
        IWebMessageService webMessageService)
    {
        _logger = logger;
        _mapper = mapper;
        _insolvencyPractitionerService = insolvencyPractitionerService;
        _webMessageService = webMessageService;
    }

    public SearchParametersViewModel? SessionSearchParametersViewModel
    {
        get
        {
            return HttpContext.Session.GetObject<SearchParametersViewModel>(nameof(SessionSearchParametersViewModel));
        }

        set
        {
            HttpContext.Session.SetObject(nameof(SessionSearchParametersViewModel), value);
        }
    }

    public IEnumerable<SearchResultViewModel>? SessionSearchResults
    {
        get
        {
            return HttpContext.Session.GetObject<IEnumerable<SearchResultViewModel>>(nameof(SessionSearchResults));
        }

        set
        {
            HttpContext.Session.SetObject(nameof(SessionSearchResults), value);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var specialMessageViewModel = new SpecialMessageViewModel();
        var webMessageDomainModels = await _webMessageService.GetAsync("fip");
        var webMessageDomainModel = webMessageDomainModels?.FirstOrDefault();

        if (webMessageDomainModel != null)
        {
            _mapper.Map(webMessageDomainModel, specialMessageViewModel);
        }

        return View(specialMessageViewModel);
    }

    [HttpGet("search")]
    public IActionResult Search()
    {
        SessionSearchResults = default;

        var searchParametersViewModel = new SearchParametersViewModel
        {
            Breadcrumbs = BreadcrumbHelpers.BuildBreadcrumbs()
        };

        return View(searchParametersViewModel);
    }

    [HttpPost("search")]
    [ValidateAntiForgeryToken]
    public IActionResult Search([FromForm]SearchParametersViewModel searchParametersViewModel)
    {
        searchParametersViewModel.Breadcrumbs = BreadcrumbHelpers.BuildBreadcrumbs();

        if (ModelState.IsValid)
        {
            SessionSearchParametersViewModel = searchParametersViewModel;

            return RedirectToAction("Results", new { searchParametersViewModel.PageNumber });
        }

        GetSortedErrors();

        return View(searchParametersViewModel);
    }

    [HttpGet("search-results/{pageNumber?}")]
    public async Task<IActionResult> Results(int pageNumber = 1)
    {
        var searchParametersViewModel = SessionSearchParametersViewModel ?? new SearchParametersViewModel();
        searchParametersViewModel.PageNumber = pageNumber;
        SessionSearchParametersViewModel = searchParametersViewModel;

        if (ModelState.IsValid)
        {
            var searchResults = SessionSearchResults;

            if (searchResults == null || !searchResults.Any())
            {
                var searchResultDomainModels = await _insolvencyPractitionerService.SearchAsync(_mapper.Map<FipApiSearchRequestModel>(searchParametersViewModel));

                if (searchResultDomainModels != null)
                {
                    searchResults = (from a in searchResultDomainModels
                                     select _mapper.Map<SearchResultViewModel>(a)
                                    ).ToList();
                }
            }

            if (searchResults != null && searchResults.Any())
            {
                var pagedViewModel = new PagedViewModel(
                    searchParametersViewModel.PageNumber,
                    searchParametersViewModel.PageSize,
                    searchResults.Count(),
                    "search-results");

                var searchResultsViewModel = new SearchResultsViewModel
                {
                    Breadcrumbs = BreadcrumbHelpers.BuildBreadcrumbs(true, page: pageNumber),
                    Paged = pagedViewModel,
                    SearchResults = searchResults.Skip(pagedViewModel.SkipCount).Take(pagedViewModel.PageSize),
                };

                SessionSearchResults = searchResults;

                return View("Results", searchResultsViewModel);
            }

            if (searchResults == null)
            {
                ModelState.AddModelError(string.Empty, "An error occurred in the search, please try again");
            }
            else if(!searchResults.Any())
            {
                searchParametersViewModel.ShowWarningMessage = "No matches found";
            }
        }

        SessionSearchResults = default;

        GetSortedErrors();

        return View("Search", searchParametersViewModel);
    }

    [HttpGet("insolvency-practitioner-details/{ipNumber}/{pageNumber?}")]
    public async Task<IActionResult> Details(int ipNumber, int pageNumber = 1)
    {
        var insolvencyPractitionerDomainModel = await _insolvencyPractitionerService.IpGetByIpNumberAsync(ipNumber);

        if (insolvencyPractitionerDomainModel != null)
        {
            var searchResultViewModel = _mapper.Map<InsolvencyPractitionerViewModel>(insolvencyPractitionerDomainModel);

            searchResultViewModel.Breadcrumbs = BreadcrumbHelpers.BuildBreadcrumbs(true, true, page : pageNumber);

            searchResultViewModel.PageNumber = pageNumber;

            return View(searchResultViewModel);
        }

        return NotFound();
    }

    private void GetSortedErrors()
    {
        if (ModelState.ErrorCount > 0)
        {
            ViewBag.SortedErrors = ModelState
                .Select(m => new
                {
                    Key = m.Key,
                    Order = ValidationOrder.SearchFieldValidationOrder.IndexOf(m.Key),
                    Error = m.Value
                })
                .SelectMany(m => m.Error.Errors.Select(e => new
                {
                    m.Key,
                    m.Order,
                    e.ErrorMessage
                }))
                .OrderBy(m => m.Order);
        }
    }
}