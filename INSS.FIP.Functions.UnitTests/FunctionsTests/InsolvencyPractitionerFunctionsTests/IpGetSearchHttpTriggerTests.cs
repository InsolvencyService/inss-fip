using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using FakeItEasy;
using INSS.FIP.Functions.Functions.InsolvencyPractitioner;
using INSS.FIP.Interfaces;
using INSS.FIP.Models.RequestModels;
using INSS.FIP.Models.RequestModels.InsolvencyPractitioner;
using INSS.FIP.Models.ResponseModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Xunit;

namespace INSS.FIP.Functions.UnitTests.FunctionsTests.InsolvencyPractitionerFunctionsTests;

[Trait("Category", "IP search Function - Unit Tests")]
public class IpGetSearchHttpTriggerTests
{
    private readonly ILogger<IpGetSearchHttpTrigger> _fakeLogger = A.Fake<ILogger<IpGetSearchHttpTrigger>>();
    private readonly IMapper _fakeMapper = A.Fake<IMapper>();
    private readonly IInsolvencyPractitionerProvider _fakeInsolvencyPractitionerService = A.Fake<IInsolvencyPractitionerProvider>();
    private readonly IpGetSearchHttpTrigger _ipGetSearchHttpTrigger;

    public IpGetSearchHttpTriggerTests()
    {
        _ipGetSearchHttpTrigger = new IpGetSearchHttpTrigger(_fakeLogger, _fakeMapper, _fakeInsolvencyPractitionerService);
    }

    [Fact]
    public async Task IpGetSearchHttpTriggerWithValidRequestReturnsOk()
    {
        // Arrange
        const HttpStatusCode expectedResult = HttpStatusCode.OK;
        var dummyFipApiSearchResultResponseModels = A.CollectionOfDummy<FipApiSearchResultResponseModel>(2);
        var request = new DefaultHttpRequest(new DefaultHttpContext());
        request.HttpContext.Request.QueryString = new QueryString("?LastName=smith");
        var ipSearchRequestModel = new IpSearchRequestModel { LastName = "smith" };

        A.CallTo(() => _fakeMapper.Map<IpSearchRequestModel>(A<FipApiSearchRequestModel>.Ignored)).Returns(ipSearchRequestModel);
        A.CallTo(() => _fakeInsolvencyPractitionerService.SearchAsync(A<IpSearchRequestModel>.Ignored)).Returns(dummyFipApiSearchResultResponseModels);

        // Act
        var result = await _ipGetSearchHttpTrigger.Run(request);

        // Assert
        A.CallTo(() => _fakeMapper.Map<IpSearchRequestModel>(A<FipApiSearchRequestModel>.Ignored)).Returns(A.Dummy<IpSearchRequestModel>());
        A.CallTo(() => _fakeInsolvencyPractitionerService.SearchAsync(A<IpSearchRequestModel>.Ignored)).MustHaveHappenedOnceExactly();

        var statusResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal((int)expectedResult, statusResult.StatusCode);
    }

    [Fact]
    public async Task IpGetSearchHttpTriggerWithWithInvalidParamatersReturnsBadRequest()
    {
        // Arrange
        const HttpStatusCode expectedResult = HttpStatusCode.BadRequest;
        var request = new DefaultHttpRequest(new DefaultHttpContext());

        // Act
        var result = await _ipGetSearchHttpTrigger.Run(request);

        // Assert
        A.CallTo(() => _fakeMapper.Map<IpSearchRequestModel>(A<FipApiSearchRequestModel>.Ignored)).Returns(A.Dummy<IpSearchRequestModel>());
        A.CallTo(() => _fakeInsolvencyPractitionerService.SearchAsync(A<IpSearchRequestModel>.Ignored)).MustNotHaveHappened();

        var statusResult = Assert.IsType<BadRequestResult>(result);
        Assert.Equal((int)expectedResult, statusResult.StatusCode);
    }

    [Fact]
    public async Task IpGetSearchHttpTriggerWithNullDataReturnsInternalServerError()
    {
        // Arrange
        const HttpStatusCode expectedResult = HttpStatusCode.InternalServerError;
        IList<FipApiSearchResultResponseModel>? nullFipApiSearchResultResponseModels = default;
        var request = new DefaultHttpRequest(new DefaultHttpContext());
        request.HttpContext.Request.QueryString = new QueryString("?LastName=smith");
        var ipSearchRequestModel = new IpSearchRequestModel { LastName = "smith" };

        A.CallTo(() => _fakeMapper.Map<IpSearchRequestModel>(A<FipApiSearchRequestModel>.Ignored)).Returns(ipSearchRequestModel);
        A.CallTo(() => _fakeInsolvencyPractitionerService.SearchAsync(A<IpSearchRequestModel>.Ignored)).Returns(nullFipApiSearchResultResponseModels);

        // Act
        var result = await _ipGetSearchHttpTrigger.Run(request);

        // Assert
        A.CallTo(() => _fakeMapper.Map<IpSearchRequestModel>(A<FipApiSearchRequestModel>.Ignored)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _fakeInsolvencyPractitionerService.SearchAsync(A<IpSearchRequestModel>.Ignored)).MustHaveHappenedOnceExactly();

        var statusResult = Assert.IsType<StatusCodeResult>(result);
        Assert.Equal((int)expectedResult, statusResult.StatusCode);
    }
}
