using System.Net;
using System.Threading.Tasks;
using FakeItEasy;
using INSS.FIP.Functions.Functions.InsolvencyPractitioner;
using INSS.FIP.Interfaces;
using INSS.FIP.Models.RequestModels.InsolvencyPractitioner;
using INSS.FIP.Models.ResponseModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Xunit;

namespace INSS.FIP.Functions.UnitTests.FunctionsTests.InsolvencyPractitionerFunctionsTests;

[Trait("Category", "IP Get by IP Number Function - Unit Tests")]
public class IpGetByIpNumberHttpTriggerTests
{
    private readonly ILogger<IpGetByIpNumberHttpTrigger> _fakeLogger = A.Fake<ILogger<IpGetByIpNumberHttpTrigger>>();
    private readonly IInsolvencyPractitionerProvider _fakeInsolvencyPractitionerService = A.Fake<IInsolvencyPractitionerProvider>();
    private readonly IpGetByIpNumberHttpTrigger _ipGetByIpNumberHttpTrigger;

    public IpGetByIpNumberHttpTriggerTests()
    {
        _ipGetByIpNumberHttpTrigger = new IpGetByIpNumberHttpTrigger(_fakeLogger, _fakeInsolvencyPractitionerService);
    }

    [Fact]
    public async Task IpGetByIpNumberHttpTriggerWithValidRequestReturnsOk()
    {
        // Arrange
        const HttpStatusCode expectedResult = HttpStatusCode.OK;
        const int ipNumber = 123;
        var dummyFipApiInsolvencyPractitionerResponseModel = A.Dummy<FipApiInsolvencyPractitionerWithAuthResponseModel>();

        A.CallTo(() => _fakeInsolvencyPractitionerService.GetByIpNumberAsync(A<IpGetByIpNumberRequestModel>.Ignored)).Returns(dummyFipApiInsolvencyPractitionerResponseModel);

        // Act
        var result = await _ipGetByIpNumberHttpTrigger.Run(new DefaultHttpRequest(new DefaultHttpContext()), ipNumber);

        // Assert
        A.CallTo(() => _fakeInsolvencyPractitionerService.GetByIpNumberAsync(A<IpGetByIpNumberRequestModel>.Ignored)).MustHaveHappenedOnceExactly();

        var statusResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal((int)expectedResult, statusResult.StatusCode);
    }

    [Fact]
    public async Task IpGetByIpNumberHttpTriggerWithInvalidParamaterReturnsBadRequest()
    {
        // Arrange
        const HttpStatusCode expectedResult = HttpStatusCode.BadRequest;
        const int ipNumber = 0;
        var dummyFipApiInsolvencyPractitionerResponseModel = A.Dummy<FipApiInsolvencyPractitionerResponseModel>();

        // Act
        var result = await _ipGetByIpNumberHttpTrigger.Run(new DefaultHttpRequest(new DefaultHttpContext()), ipNumber);

        // Assert
        A.CallTo(() => _fakeInsolvencyPractitionerService.GetByIpNumberAsync(A<IpGetByIpNumberRequestModel>.Ignored)).MustNotHaveHappened();

        var statusResult = Assert.IsType<BadRequestResult>(result);
        Assert.Equal((int)expectedResult, statusResult.StatusCode);
    }

    [Fact]
    public async Task IpGetByIpNumberHttpTriggerWithNullDataReturnsNoContent()
    {
        // Arrange
        const HttpStatusCode expectedResult = HttpStatusCode.NoContent;
        const int ipNumber = 123;
        FipApiInsolvencyPractitionerWithAuthResponseModel? nullFipApiInsolvencyPractitionerResponseModel = default;

        A.CallTo(() => _fakeInsolvencyPractitionerService.GetByIpNumberAsync(A<IpGetByIpNumberRequestModel>.Ignored)).Returns(nullFipApiInsolvencyPractitionerResponseModel);

        // Act
        var result = await _ipGetByIpNumberHttpTrigger.Run(new DefaultHttpRequest(new DefaultHttpContext()), ipNumber);

        // Assert
        A.CallTo(() => _fakeInsolvencyPractitionerService.GetByIpNumberAsync(A<IpGetByIpNumberRequestModel>.Ignored)).MustHaveHappenedOnceExactly();

        var statusResult = Assert.IsType<NoContentResult>(result);
        Assert.Equal((int)expectedResult, statusResult.StatusCode);
    }
}
