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

[Trait("Category", "AuthBody Service - Unit Tests")]
public class AuthBodyServiceTests
{
    private readonly ILogger<AuthBodyService> _fakeLogger = A.Fake<ILogger<AuthBodyService>>();
    private readonly IMapper _fakeMapper = A.Fake<IMapper>();
    private readonly IFipApiConnector _fakeFipApiConnector = A.Fake<IFipApiConnector>();
    private readonly AuthBodyService authBodyService;

    public AuthBodyServiceTests()
    {
        authBodyService = new AuthBodyService(_fakeLogger, _fakeMapper, _fakeFipApiConnector);
    }

    [Fact]
    public async Task AuthBodyServiceGetAsyncWithSuccessfulApiCallReturnsSuccess()
    {
        // Arrange
        const int expectedCount = 2;
        var dummyApiConnectorResponseModel = A.Dummy<ApiConnectorResponseModel<IList<FipApiAuthBodyResponseModel>>>();
        dummyApiConnectorResponseModel.IsSuccessStatusCode = true;
        dummyApiConnectorResponseModel.Payload = A.CollectionOfDummy<FipApiAuthBodyResponseModel>(expectedCount);

        A.CallTo(() => _fakeFipApiConnector.ProcessAsync<IList<FipApiAuthBodyResponseModel>>(A<ApiConnectorRequestModel>.Ignored)).Returns(dummyApiConnectorResponseModel);
        A.CallTo(() => _fakeMapper.Map<IList<AuthBodyDomainModel>>(A<IList<FipApiAuthBodyResponseModel>>.Ignored)).Returns(A.CollectionOfDummy<AuthBodyDomainModel>(expectedCount));

        // Act
        var result = await authBodyService.GetAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedCount, result!.Count);

        A.CallTo(() => _fakeFipApiConnector.ProcessAsync<IList<FipApiAuthBodyResponseModel>>(A<ApiConnectorRequestModel>.Ignored)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _fakeMapper.Map<IList<AuthBodyDomainModel>>(A<IList<FipApiAuthBodyResponseModel>>.Ignored)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task AuthBodyServiceGetAsyncWithUnsuccessfulApiCallReturnsNull()
    {
        // Arrange
        var dummyApiConnectorResponseModel = A.Dummy<ApiConnectorResponseModel<IList<FipApiAuthBodyResponseModel>>>();
        dummyApiConnectorResponseModel.IsSuccessStatusCode = false;

        A.CallTo(() => _fakeFipApiConnector.ProcessAsync<IList<FipApiAuthBodyResponseModel>>(A<ApiConnectorRequestModel>.Ignored)).Returns(dummyApiConnectorResponseModel);

        // Act
        var result = await authBodyService.GetAsync();

        // Assert
        Assert.Null(result);

        A.CallTo(() => _fakeFipApiConnector.ProcessAsync<IList<FipApiAuthBodyResponseModel>>(A<ApiConnectorRequestModel>.Ignored)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _fakeMapper.Map<IList<AuthBodyDomainModel>>(A<IList<FipApiAuthBodyResponseModel>>.Ignored)).MustNotHaveHappened();
    }
}
