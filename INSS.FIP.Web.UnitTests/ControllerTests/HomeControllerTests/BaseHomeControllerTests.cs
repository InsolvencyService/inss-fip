using INSS.FIP.Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace INSS.FIP.Web.UnitTests.ControllerTests.HomeControllerTests;

public abstract class BaseHomeControllerTests
{
    protected static HomeController BuildHomeController()
    {
        var controller = new HomeController()
        {
            ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext(),
            },
        };

        return controller;
    }
}
