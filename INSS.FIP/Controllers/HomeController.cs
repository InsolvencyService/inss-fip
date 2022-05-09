using INSS.FIP.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace INSS.FIP.Controllers;

public class HomeController : Controller
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

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
