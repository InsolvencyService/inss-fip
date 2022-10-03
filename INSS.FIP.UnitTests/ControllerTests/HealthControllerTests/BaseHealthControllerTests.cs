using INSS.FIP.Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace INSS.FIP.UnitTests.ControllerTests.HealthControllerTests;

public class BaseHealthControllerTests
{
    protected static HealthController BuildHealthController()
    {
        var controller = new HealthController()
        {
            ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext(),
            },
        };

        return controller;
    }
}
