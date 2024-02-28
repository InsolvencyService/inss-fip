using INSS.FIP.Web.Helpers;
using INSS.FIP.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace INSS.FIP.Web.Controllers;

public class HomeController : Controller
{
    [Route("/home/accessibility-statement")]
    public IActionResult AccessibilityStatement()
    {
        var model = BuildModel();

        return View(model);
    }

    [Route("/home/privacy-policy")]
    public IActionResult PrivacyPolicy()
    {
        var model = BuildModel();

        return View(model);
    }

    [Route("/home/terms-and-conditions")]
    public IActionResult TermsAndConditions()
    {
        var model = BuildModel();

        return View(model);
    }

    private static InfoPagesViewModel BuildModel()
    {
        return new InfoPagesViewModel
        {
            Breadcrumbs = BreadcrumbHelpers.BuildBreadcrumbs().ToList()
        };
    }
}