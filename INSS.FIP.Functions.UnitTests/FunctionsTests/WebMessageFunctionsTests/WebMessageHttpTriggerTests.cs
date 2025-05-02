using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FakeItEasy;
using INSS.FIP.Functions.Functions.GetWebPageBannerMessage;
using INSS.FIP.Interfaces;
using INSS.FIP.Models.RequestModels.GetWebPageBannerMessage;
using INSS.FIP.Models.ResponseModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Xunit;

namespace INSS.FIP.Functions.UnitTests.FunctionsTests.GetWebPageBannerMessageFunctionsTests;

[Trait("Category", "GetWebPageBannerMessage Function - Unit Tests")]
public class GetWebPageBannerMessageHttpTriggerTests
{
    private readonly ILogger<GetWebPageBannerMessageGetHttpTrigger> _fakeLogger = A.Fake<ILogger<GetWebPageBannerMessageGetHttpTrigger>>();
    private readonly IGetWebPageBannerMessageProvider _fakeGetWebPageBannerMessageService = A.Fake<IGetWebPageBannerMessageProvider>();
    private readonly GetWebPageBannerMessageGetHttpTrigger _GetWebPageBannerMessageHttpTrigger;

    public GetWebPageBannerMessageHttpTriggerTests()
    {
        _GetWebPageBannerMessageHttpTrigger = new GetWebPageBannerMessageGetHttpTrigger(_fakeLogger, _fakeGetWebPageBannerMessageService);
    }

    [Fact]
    public async Task GetWebPageBannerMessageHttpTriggerWithValidRequestReturnsOk()
    {
        // Arrange
        const HttpStatusCode expectedResult = HttpStatusCode.OK;
        const string applicationPrefix = "fip";
        var dummyFipApiGetWebPageBannerMessageResponseModels = A.CollectionOfDummy<FipApiGetWebPageBannerMessageResponseModel>(2);

        A.CallTo(() => _fakeGetWebPageBannerMessageService.GetAsync(A<GetWebPageBannerMessageRequestModel>.Ignored)).Returns(dummyFipApiGetWebPageBannerMessageResponseModels);

        // Act
        var result = await _GetWebPageBannerMessageHttpTrigger.Run(new DefaultHttpContext().Request, applicationPrefix);

        // Assert
        A.CallTo(() => _fakeGetWebPageBannerMessageService.GetAsync(A<GetWebPageBannerMessageRequestModel>.Ignored)).MustHaveHappenedOnceExactly();

        var statusResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal((int)expectedResult, statusResult.StatusCode);
    }

    [Fact]
    public async Task GetWebPageBannerMessageHttpTriggerWithInvalidParamaterReturnsBadRequest()
    {
        // Arrange
        const HttpStatusCode expectedResult = HttpStatusCode.BadRequest;
        string applicationPrefix = string.Empty;
        var dummyFipApiGetWebPageBannerMessageResponseModels = A.CollectionOfDummy<FipApiGetWebPageBannerMessageResponseModel>(0);

        // Act
        var result = await _GetWebPageBannerMessageHttpTrigger.Run(new DefaultHttpContext().Request, applicationPrefix);

        // Assert
        A.CallTo(() => _fakeGetWebPageBannerMessageService.GetAsync(A<GetWebPageBannerMessageRequestModel>.Ignored)).MustNotHaveHappened();

        var statusResult = Assert.IsType<BadRequestResult>(result);
        Assert.Equal((int)expectedResult, statusResult.StatusCode);
    }

    [Fact]
    public async Task GetWebPageBannerMessageHttpTriggerWithNullDataReturnsInternalServerError()
    {
        // Arrange
        const HttpStatusCode expectedResult = HttpStatusCode.InternalServerError;
        const string applicationPrefix = "fip";
        IList<FipApiGetWebPageBannerMessageResponseModel>? nullFipApiGetWebPageBannerMessageResponseModels = default;

        A.CallTo(() => _fakeGetWebPageBannerMessageService.GetAsync(A<GetWebPageBannerMessageRequestModel>.Ignored)).Returns(nullFipApiGetWebPageBannerMessageResponseModels);

        // Act
        var result = await _GetWebPageBannerMessageHttpTrigger.Run(new DefaultHttpContext().Request, applicationPrefix);

        // Assert
        A.CallTo(() => _fakeGetWebPageBannerMessageService.GetAsync(A<GetWebPageBannerMessageRequestModel>.Ignored)).MustHaveHappenedOnceExactly();

        var statusResult = Assert.IsType<StatusCodeResult>(result);
        Assert.Equal((int)expectedResult, statusResult.StatusCode);
    }
}
