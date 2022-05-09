using INSS.FIP.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace INSS.FIP.UnitTests.ControllerTests.HomeControllerTests;

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
