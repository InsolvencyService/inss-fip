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

[Trait("Category", "GetWebPageBannerMessage Service - Unit Tests")]
public class GetWebPageBannerMessageServiceTests
{
    private readonly ILogger<GetWebPageBannerMessageService> _fakeLogger = A.Fake<ILogger<GetWebPageBannerMessageService>>();
    private readonly IMapper _fakeMapper = A.Fake<IMapper>();
    private readonly IFipApiConnector _fakeFipApiConnector = A.Fake<IFipApiConnector>();
    private readonly GetWebPageBannerMessageService GetWebPageBannerMessageService;

    public GetWebPageBannerMessageServiceTests()
    {
        GetWebPageBannerMessageService = new GetWebPageBannerMessageService(_fakeLogger, _fakeMapper, _fakeFipApiConnector);
    }

    [Fact]
    public async Task GetWebPageBannerMessageServiceGetAsyncWithSuccessfulApiCallReturnsSuccess()
    {
        // Arrange
        const int expectedCount = 2;
        var dummyApiConnectorResponseModel = A.Dummy<ApiConnectorResponseModel<IList<FipApiGetWebPageBannerMessageResponseModel>>>();
        dummyApiConnectorResponseModel.IsSuccessStatusCode = true;
        dummyApiConnectorResponseModel.Payload = A.CollectionOfDummy<FipApiGetWebPageBannerMessageResponseModel>(expectedCount);

        A.CallTo(() => _fakeFipApiConnector.ProcessAsync<IList<FipApiGetWebPageBannerMessageResponseModel>>(A<ApiConnectorRequestModel>.Ignored)).Returns(dummyApiConnectorResponseModel);
        A.CallTo(() => _fakeMapper.Map<IList<GetWebPageBannerMessageDomainModel>>(A<IList<FipApiGetWebPageBannerMessageResponseModel>>.Ignored)).Returns(A.CollectionOfDummy<GetWebPageBannerMessageDomainModel>(expectedCount));

        // Act
        var result = await GetWebPageBannerMessageService.GetAsync("fip");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedCount, result!.Count);

        A.CallTo(() => _fakeFipApiConnector.ProcessAsync<IList<FipApiGetWebPageBannerMessageResponseModel>>(A<ApiConnectorRequestModel>.Ignored)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _fakeMapper.Map<IList<GetWebPageBannerMessageDomainModel>>(A<IList<FipApiGetWebPageBannerMessageResponseModel>>.Ignored)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task GetWebPageBannerMessageServiceGetAsyncWithUnsuccessfulApiCallReturnsNull()
    {
        // Arrange
        var dummyApiConnectorResponseModel = A.Dummy<ApiConnectorResponseModel<IList<FipApiGetWebPageBannerMessageResponseModel>>>();
        dummyApiConnectorResponseModel.IsSuccessStatusCode = false;

        A.CallTo(() => _fakeFipApiConnector.ProcessAsync<IList<FipApiGetWebPageBannerMessageResponseModel>>(A<ApiConnectorRequestModel>.Ignored)).Returns(dummyApiConnectorResponseModel);

        // Act
        var result = await GetWebPageBannerMessageService.GetAsync("fip");

        // Assert
        Assert.Null(result);

        A.CallTo(() => _fakeFipApiConnector.ProcessAsync<IList<FipApiGetWebPageBannerMessageResponseModel>>(A<ApiConnectorRequestModel>.Ignored)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _fakeMapper.Map<IList<GetWebPageBannerMessageDomainModel>>(A<IList<FipApiGetWebPageBannerMessageResponseModel>>.Ignored)).MustNotHaveHappened();
    }
}
