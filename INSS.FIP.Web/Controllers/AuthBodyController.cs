using AutoMapper;
using INSS.FIP.Interfaces;
using INSS.FIP.Web.Helpers;
using INSS.FIP.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace INSS.FIP.Web.Controllers;

public class AuthBodyController : Controller
{
    private readonly IMapper _mapper;
    private readonly IAuthBodyService _authBodyService;

    public AuthBodyController(IMapper mapper, IAuthBodyService authBodyService)
    {
        _mapper = mapper;
        _authBodyService = authBodyService;
    }

    public async Task<IActionResult> Index(int? ipNumber = default, string? ipName = default)
    {
        var authBodyDomainModels = await _authBodyService.GetAsync();

        var authBodiesViewModel = new AuthBodiesViewModel
        {
            Breadcrumbs = BreadcrumbHelpers.BuildBreadcrumbs(ipNumber.HasValue, ipNumber.HasValue, ipNumber.HasValue, ipNumber, ipName),

            AuthBodies = from a in authBodyDomainModels
                         select _mapper.Map<AuthBodyViewModel>(a),
        };

        return View(authBodiesViewModel);
    }
}
