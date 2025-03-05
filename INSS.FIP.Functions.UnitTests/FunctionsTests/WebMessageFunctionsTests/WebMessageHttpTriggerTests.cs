using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FakeItEasy;
using INSS.FIP.Functions.Functions.WebMessage;
using INSS.FIP.Interfaces;
using INSS.FIP.Models.RequestModels.WebMessage;
using INSS.FIP.Models.ResponseModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Xunit;

namespace INSS.FIP.Functions.UnitTests.FunctionsTests.WebMessageFunctionsTests;

[Trait("Category", "WebMessage Function - Unit Tests")]
public class WebMessageHttpTriggerTests
{
    private readonly ILogger<WebMessageGetHttpTrigger> _fakeLogger = A.Fake<ILogger<WebMessageGetHttpTrigger>>();
    private readonly IWebMessageProvider _fakeWebMessageService = A.Fake<IWebMessageProvider>();
    private readonly WebMessageGetHttpTrigger _WebMessageHttpTrigger;

    public WebMessageHttpTriggerTests()
    {
        _WebMessageHttpTrigger = new WebMessageGetHttpTrigger(_fakeLogger, _fakeWebMessageService);
    }

    [Fact]
    public async Task WebMessageHttpTriggerWithValidRequestReturnsOk()
    {
        // Arrange
        const HttpStatusCode expectedResult = HttpStatusCode.OK;
        const string applicationPrefix = "fip";
        var dummyFipApiWebMessageResponseModels = A.CollectionOfDummy<FipApiWebMessageResponseModel>(2);

        A.CallTo(() => _fakeWebMessageService.GetAsync(A<WebMessageRequestModel>.Ignored)).Returns(dummyFipApiWebMessageResponseModels);

        // Act
        var result = await _WebMessageHttpTrigger.Run(new DefaultHttpRequest(new DefaultHttpContext()), applicationPrefix);

        // Assert
        A.CallTo(() => _fakeWebMessageService.GetAsync(A<WebMessageRequestModel>.Ignored)).MustHaveHappenedOnceExactly();

        var statusResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal((int)expectedResult, statusResult.StatusCode);
    }

    [Fact]
    public async Task WebMessageHttpTriggerWithInvalidParamaterReturnsBadRequest()
    {
        // Arrange
        const HttpStatusCode expectedResult = HttpStatusCode.BadRequest;
        string applicationPrefix = string.Empty;
        var dummyFipApiWebMessageResponseModels = A.CollectionOfDummy<FipApiWebMessageResponseModel>(0);

        // Act
        var result = await _WebMessageHttpTrigger.Run(new DefaultHttpRequest(new DefaultHttpContext()), applicationPrefix);

        // Assert
        A.CallTo(() => _fakeWebMessageService.GetAsync(A<WebMessageRequestModel>.Ignored)).MustNotHaveHappened();

        var statusResult = Assert.IsType<BadRequestResult>(result);
        Assert.Equal((int)expectedResult, statusResult.StatusCode);
    }

    [Fact]
    public async Task WebMessageHttpTriggerWithNullDataReturnsInternalServerError()
    {
        // Arrange
        const HttpStatusCode expectedResult = HttpStatusCode.InternalServerError;
        const string applicationPrefix = "fip";
        IList<FipApiWebMessageResponseModel>? nullFipApiWebMessageResponseModels = default;

        A.CallTo(() => _fakeWebMessageService.GetAsync(A<WebMessageRequestModel>.Ignored)).Returns(nullFipApiWebMessageResponseModels);

        // Act
        var result = await _WebMessageHttpTrigger.Run(new DefaultHttpRequest(new DefaultHttpContext()), applicationPrefix);

        // Assert
        A.CallTo(() => _fakeWebMessageService.GetAsync(A<WebMessageRequestModel>.Ignored)).MustHaveHappenedOnceExactly();

        var statusResult = Assert.IsType<StatusCodeResult>(result);
        Assert.Equal((int)expectedResult, statusResult.StatusCode);
    }
}
