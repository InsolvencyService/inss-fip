using Microsoft.AspNetCore.Mvc;

namespace INSS.FIP.Web.Controllers;

public class HealthController : Controller
{
    public IActionResult Ping()
    {
        return new OkResult();
    }
}
