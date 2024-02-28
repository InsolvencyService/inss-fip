using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FakeItEasy;
using INSS.FIP.Interfaces;
using INSS.FIP.Models.ApiConnectorModels;
using INSS.FIP.Models.DomainModels;
using INSS.FIP.Models.ResponseModels;
using INSS.FIP.Services;
using Microsoft.Extensions.Logging;
using Xunit;

namespace INSS.FIP.Web.UnitTests.ServicesTests;

[Trait("Category", "WebMessage Service - Unit Tests")]
public class WebMessageServiceTests
{
    private readonly ILogger<WebMessageService> _fakeLogger = A.Fake<ILogger<WebMessageService>>();
    private readonly IMapper _fakeMapper = A.Fake<IMapper>();
    private readonly IFipApiConnector _fakeFipApiConnector = A.Fake<IFipApiConnector>();
    private readonly WebMessageService WebMessageService;

    public WebMessageServiceTests()
    {
        WebMessageService = new WebMessageService(_fakeLogger, _fakeMapper, _fakeFipApiConnector);
    }

    [Fact]
    public async Task WebMessageServiceGetAsyncWithSuccessfulApiCallReturnsSuccess()
    {
        // Arrange
        const int expectedCount = 2;
        var dummyApiConnectorResponseModel = A.Dummy<ApiConnectorResponseModel<IList<FipApiWebMessageResponseModel>>>();
        dummyApiConnectorResponseModel.IsSuccessStatusCode = true;
        dummyApiConnectorResponseModel.Payload = A.CollectionOfDummy<FipApiWebMessageResponseModel>(expectedCount);

        A.CallTo(() => _fakeFipApiConnector.ProcessAsync<IList<FipApiWebMessageResponseModel>>(A<ApiConnectorRequestModel>.Ignored)).Returns(dummyApiConnectorResponseModel);
        A.CallTo(() => _fakeMapper.Map<IList<WebMessageDomainModel>>(A<IList<FipApiWebMessageResponseModel>>.Ignored)).Returns(A.CollectionOfDummy<WebMessageDomainModel>(expectedCount));

        // Act
        var result = await WebMessageService.GetAsync("fip");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedCount, result!.Count);

        A.CallTo(() => _fakeFipApiConnector.ProcessAsync<IList<FipApiWebMessageResponseModel>>(A<ApiConnectorRequestModel>.Ignored)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _fakeMapper.Map<IList<WebMessageDomainModel>>(A<IList<FipApiWebMessageResponseModel>>.Ignored)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task WebMessageServiceGetAsyncWithUnsuccessfulApiCallReturnsNull()
    {
        // Arrange
        var dummyApiConnectorResponseModel = A.Dummy<ApiConnectorResponseModel<IList<FipApiWebMessageResponseModel>>>();
        dummyApiConnectorResponseModel.IsSuccessStatusCode = false;

        A.CallTo(() => _fakeFipApiConnector.ProcessAsync<IList<FipApiWebMessageResponseModel>>(A<ApiConnectorRequestModel>.Ignored)).Returns(dummyApiConnectorResponseModel);

        // Act
        var result = await WebMessageService.GetAsync("fip");

        // Assert
        Assert.Null(result);

        A.CallTo(() => _fakeFipApiConnector.ProcessAsync<IList<FipApiWebMessageResponseModel>>(A<ApiConnectorRequestModel>.Ignored)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _fakeMapper.Map<IList<WebMessageDomainModel>>(A<IList<FipApiWebMessageResponseModel>>.Ignored)).MustNotHaveHappened();
    }
}
