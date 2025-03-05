using AutoMapper;
using FakeItEasy;
using INSS.FIP.Interfaces;
using INSS.FIP.Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace INSS.FIP.Web.UnitTests.ControllerTests.IpControllerTests;

public abstract class BaseIpController
{
    protected readonly ILogger<IpController> _fakeLogger = A.Fake<ILogger<IpController>>();
    protected readonly IMapper _fakeMapper = A.Fake<IMapper>();
    protected readonly IInsolvencyPractitionerService _fakeInsolvencyPractitionerService = A.Fake<IInsolvencyPractitionerService>();
    protected readonly IWebMessageService _fakeWebMessageService = A.Fake<IWebMessageService>();

    protected IpController BuildIpController()
    {
        var httpContext = new DefaultHttpContext
        {
            Session = A.Fake<ISession>()
        };

        var controller = new IpController(_fakeLogger, _fakeMapper, _fakeInsolvencyPractitionerService, _fakeWebMessageService)
        {
            ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext,
            },
        };

        return controller;
    }
}
