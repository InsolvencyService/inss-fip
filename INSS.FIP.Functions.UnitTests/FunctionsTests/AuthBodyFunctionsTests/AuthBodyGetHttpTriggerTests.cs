using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FakeItEasy;
using INSS.FIP.Functions.Functions.AuthBody;
using INSS.FIP.Interfaces;
using INSS.FIP.Models.ResponseModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Xunit;

namespace INSS.FIP.Functions.UnitTests.FunctionsTests.AuthBodyFunctionsTests;

[Trait("Category", "AuthBody Function - Unit Tests")]
public class AuthBodyGetHttpTriggerTests
{
    private readonly ILogger<AuthBodyGetHttpTrigger> _fakeLogger = A.Fake<ILogger<AuthBodyGetHttpTrigger>>();
    private readonly IAuthBodyProvider _fakeAuthBodyService = A.Fake<IAuthBodyProvider>();
    private readonly AuthBodyGetHttpTrigger _authBodyGetHttpTrigger;

    public AuthBodyGetHttpTriggerTests()
    {
        _authBodyGetHttpTrigger = new AuthBodyGetHttpTrigger(_fakeLogger, _fakeAuthBodyService);
    }

    [Fact]
    public async Task AuthBodyGetHttpTriggerWithValidRequestReturnsOk()
    {
        // Arrange
        const HttpStatusCode expectedResult = HttpStatusCode.OK;
        var dummyFipApiAuthBodyResponseModels = A.CollectionOfDummy<FipApiAuthBodyResponseModel>(2);

        A.CallTo(() => _fakeAuthBodyService.GetAsync()).Returns(dummyFipApiAuthBodyResponseModels);

        // Act
        var result = await _authBodyGetHttpTrigger.Run(new DefaultHttpRequest(new DefaultHttpContext()));

        // Assert
        A.CallTo(() => _fakeAuthBodyService.GetAsync()).MustHaveHappenedOnceExactly();

        var statusResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal((int)expectedResult, statusResult.StatusCode);
    }

    [Fact]
    public async Task AuthBodyGetHttpTriggerWithNoDataReturnsNoContent()
    {
        // Arrange
        const HttpStatusCode expectedResult = HttpStatusCode.NoContent;
        var dummyFipApiAuthBodyResponseModels = A.CollectionOfDummy<FipApiAuthBodyResponseModel>(0);

        A.CallTo(() => _fakeAuthBodyService.GetAsync()).Returns(dummyFipApiAuthBodyResponseModels);

        // Act
        var result = await _authBodyGetHttpTrigger.Run(new DefaultHttpRequest(new DefaultHttpContext()));

        // Assert
        A.CallTo(() => _fakeAuthBodyService.GetAsync()).MustHaveHappenedOnceExactly();

        var statusResult = Assert.IsType<NoContentResult>(result);
        Assert.Equal((int)expectedResult, statusResult.StatusCode);
    }

    [Fact]
    public async Task AuthBodyGetHttpTriggerWithNullDataReturnsInternalServerError()
    {
        // Arrange
        const HttpStatusCode expectedResult = HttpStatusCode.InternalServerError;
        const IList<FipApiAuthBodyResponseModel>? nullFipApiAuthBodyResponseModels = default;

        A.CallTo(() => _fakeAuthBodyService.GetAsync()).Returns(nullFipApiAuthBodyResponseModels);

        // Act
        var result = await _authBodyGetHttpTrigger.Run(new DefaultHttpRequest(new DefaultHttpContext()));

        // Assert
        A.CallTo(() => _fakeAuthBodyService.GetAsync()).MustHaveHappenedOnceExactly();

        var statusResult = Assert.IsType<StatusCodeResult>(result);
        Assert.Equal((int)expectedResult, statusResult.StatusCode);
    }
}
