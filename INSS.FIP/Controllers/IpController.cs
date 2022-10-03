using AutoMapper;
using INSS.FIP.Interfaces;
using INSS.FIP.Models.RequestModels;
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

    public IActionResult Search()
    {
        SessionSearchResults = default;

        var searchParametersViewModel = new SearchParametersViewModel()
        {
            Breadcrumbs = BreadcrumbHelpers.BuildBreadcrumbs(),
        };

        return View(searchParametersViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Search([Bind("PageSize,PageNumber,FirstName,LastName,Company,Town,IpNumber,County")] SearchParametersViewModel searchParametersViewModel)
    {
        searchParametersViewModel.Breadcrumbs = BreadcrumbHelpers.BuildBreadcrumbs();

        if (ModelState.IsValid)
        {
            SessionSearchParametersViewModel = searchParametersViewModel;

            return await Results(searchParametersViewModel.PageNumber);
        }

        return View(searchParametersViewModel);
    }

    public async Task<IActionResult> Results()
    {
        var searchParametersViewModel = SessionSearchParametersViewModel ?? new SearchParametersViewModel();

        return await Results(searchParametersViewModel.PageNumber);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Results([Bind("PageNumber")] int pageNumber)
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
                var pagedViewModel = new PagedViewModel(searchParametersViewModel.PageNumber, searchParametersViewModel.PageSize, searchResults.Count());
                var searchResultsViewModel = new SearchResultsViewModel
                {
                    Breadcrumbs = BreadcrumbHelpers.BuildBreadcrumbs(true),
                    Paged = pagedViewModel,
                    SearchResults = searchResults.Skip(pagedViewModel.SkipCount).Take(pagedViewModel.PageSize),
                };

                SessionSearchResults = searchResults;

                return View(nameof(Results), searchResultsViewModel);
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

        return View(nameof(Search), searchParametersViewModel);
    }

    [Route("IP/IP/{ipNumber}")]
    public async Task<IActionResult> IP([Bind("IpNumber")] int ipNumber)
    {
        var insolvencyPractitionerDomainModel = await _insolvencyPractitionerService.IpGetByIpNumberAsync(ipNumber);

        if (insolvencyPractitionerDomainModel != null)
        {
            var searchResulViewModel = _mapper.Map<InsolvencyPractitionerViewModel>(insolvencyPractitionerDomainModel);

            searchResulViewModel.Breadcrumbs = BreadcrumbHelpers.BuildBreadcrumbs(true, true);

            return View(searchResulViewModel);
        }

        return NotFound();
    }
}
