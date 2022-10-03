using AutoMapper;
using FakeItEasy;
using INSS.FIP.Interfaces;
using INSS.FIP.Services;
using INSS.FIP.Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace INSS.FIP.UnitTests.ControllerTests.AuthBodyControllerTests;

public class BaseAuthBodyController
{
    protected readonly IMapper _fakeMapper = A.Fake<IMapper>();
    protected readonly IAuthBodyService _fakeAuthBodyService = A.Fake<IAuthBodyService>();

    protected AuthBodyController BuildAuthBodyController()
    {
        var controller = new AuthBodyController(_fakeMapper, _fakeAuthBodyService)
        {
            ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext(),
            },
        };

        return controller;
    }
}