using Microsoft.AspNetCore.Mvc;

namespace INSS.FIP.Web.Controllers;

public class HomeController : Controller
{

    [Route("/home/accessibility-statement")]
    public IActionResult AccessibilityStatement()
    {
        return View();
    }

    public IActionResult Contact()
    {
        return View();
    }

    public IActionResult Cookies()
    {
        return View();
    }

    [Route("/home/privacy-policy")]
    public IActionResult PrivacyPolicy()
    {
        return View();
    }

    [Route("/home/terms-and-conditions")]
    public IActionResult TermsAndConditions()
    {
        return View();
    }
}
