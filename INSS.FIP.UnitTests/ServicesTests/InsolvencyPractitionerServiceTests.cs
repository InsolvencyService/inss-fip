using AutoMapper;
using FakeItEasy;
using INSS.FIP.Services;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using INSS.FIP.Interfaces;
using INSS.FIP.Models.ApiConnectorModels;
using INSS.FIP.Models.DomainModels;
using INSS.FIP.Models.RequestModels;
using INSS.FIP.Models.ResponseModels;
using Xunit;

namespace INSS.FIP.UnitTests.ServicesTests;

[Trait("Category", "InsolvencyPractitioner Service - Unit Tests")]
public class InsolvencyPractitionerServiceTests
{
    private readonly ILogger<InsolvencyPractitionerService> _fakeLogger = A.Fake<ILogger<InsolvencyPractitionerService>>();
    private readonly IMapper _fakeMapper = A.Fake<IMapper>();
    private readonly IFipApiConnector _fakeFipApiConnector = A.Fake<IFipApiConnector>();
    private readonly InsolvencyPractitionerService InsolvencyPractitionerService;

    public InsolvencyPractitionerServiceTests()
    {
        InsolvencyPractitionerService = new InsolvencyPractitionerService(_fakeLogger, _fakeMapper, _fakeFipApiConnector);
    }

    [Fact]
    public async Task InsolvencyPractitionerServiceSearchAsyncWithSuccessfulApiCallReturnsSuccess()
    {
        // Arrange
        var fipApiSearchRequestModel = new FipApiSearchRequestModel { LastName = "smith" };
        const int expectedCount = 2;
        var dummyApiConnectorResponseModel = A.Dummy<ApiConnectorResponseModel<IList<FipApiSearchResultResponseModel>>>();
        dummyApiConnectorResponseModel.IsSuccessStatusCode = true;
        dummyApiConnectorResponseModel.Payload = A.CollectionOfDummy<FipApiSearchResultResponseModel>(expectedCount);

        A.CallTo(() => _fakeFipApiConnector.ProcessAsync<IList<FipApiSearchResultResponseModel>>(A<ApiConnectorRequestModel>.Ignored)).Returns(dummyApiConnectorResponseModel);
        A.CallTo(() => _fakeMapper.Map<IList<SearchResultDomainModel>>(A<IList<FipApiSearchResultResponseModel>>.Ignored)).Returns(A.CollectionOfDummy<SearchResultDomainModel>(expectedCount));

        // Act
        var result = await InsolvencyPractitionerService.SearchAsync(fipApiSearchRequestModel);

        // Assert
        Assert.NotNull(result);

        A.CallTo(() => _fakeFipApiConnector.ProcessAsync<IList<FipApiSearchResultResponseModel>>(A<ApiConnectorRequestModel>.Ignored)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _fakeMapper.Map<IList<SearchResultDomainModel>>(A<IList<FipApiSearchResultResponseModel>>.Ignored)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task InsolvencyPractitionerServiceSearchAsyncWithUnsuccessfulApiCallReturnsNull()
    {
        // Arrange
        var fipApiSearchRequestModel = new FipApiSearchRequestModel { LastName = "smith" };
        var dummyApiConnectorResponseModel = A.Dummy<ApiConnectorResponseModel<IList<FipApiSearchResultResponseModel>>>();
        dummyApiConnectorResponseModel.IsSuccessStatusCode = false;

        A.CallTo(() => _fakeFipApiConnector.ProcessAsync<IList<FipApiSearchResultResponseModel>>(A<ApiConnectorRequestModel>.Ignored)).Returns(dummyApiConnectorResponseModel);

        // Act
        var result = await InsolvencyPractitionerService.SearchAsync(fipApiSearchRequestModel);

        // Assert
        Assert.Null(result);

        A.CallTo(() => _fakeFipApiConnector.ProcessAsync<IList<FipApiSearchResultResponseModel>>(A<ApiConnectorRequestModel>.Ignored)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _fakeMapper.Map<IList<SearchResultDomainModel>>(A<IList<FipApiSearchResultResponseModel>>.Ignored)).MustNotHaveHappened();
    }

    [Fact]
    public async Task InsolvencyPractitionerServiceIpGetByIpNumberAsyncWithSuccessfulApiCallReturnsSuccess()
    {
        // Arrange
        var dummyApiConnectorResponseModel = A.Dummy<ApiConnectorResponseModel<FipApiInsolvencyPractitionerResponseModel>>();
        dummyApiConnectorResponseModel.IsSuccessStatusCode = true;
        dummyApiConnectorResponseModel.Payload = A.Dummy<FipApiInsolvencyPractitionerResponseModel>();

        A.CallTo(() => _fakeFipApiConnector.ProcessAsync<FipApiInsolvencyPractitionerResponseModel>(A<ApiConnectorRequestModel>.Ignored)).Returns(dummyApiConnectorResponseModel);
        A.CallTo(() => _fakeMapper.Map<InsolvencyPractitionerDomainModel>(A<FipApiInsolvencyPractitionerResponseModel>.Ignored)).Returns(A.Dummy<InsolvencyPractitionerDomainModel>());

        // Act
        var result = await InsolvencyPractitionerService.IpGetByIpNumberAsync(123);

        // Assert
        Assert.NotNull(result);

        A.CallTo(() => _fakeFipApiConnector.ProcessAsync<FipApiInsolvencyPractitionerResponseModel>(A<ApiConnectorRequestModel>.Ignored)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _fakeMapper.Map<InsolvencyPractitionerDomainModel>(A<FipApiInsolvencyPractitionerResponseModel>.Ignored)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task InsolvencyPractitionerServiceIpGetByIpNumberAsyncWithUnsuccessfulApiCallReturnsNull()
    {
        // Arrange
        var dummyApiConnectorResponseModel = A.Dummy<ApiConnectorResponseModel<FipApiInsolvencyPractitionerResponseModel>>();
        dummyApiConnectorResponseModel.IsSuccessStatusCode = false;

        A.CallTo(() => _fakeFipApiConnector.ProcessAsync<FipApiInsolvencyPractitionerResponseModel>(A<ApiConnectorRequestModel>.Ignored)).Returns(dummyApiConnectorResponseModel);

        // Act
        var result = await InsolvencyPractitionerService.IpGetByIpNumberAsync(123);

        // Assert
        Assert.Null(result);

        A.CallTo(() => _fakeFipApiConnector.ProcessAsync<FipApiInsolvencyPractitionerResponseModel>(A<ApiConnectorRequestModel>.Ignored)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _fakeMapper.Map<InsolvencyPractitionerDomainModel>(A<FipApiInsolvencyPractitionerResponseModel>.Ignored)).MustNotHaveHappened();
    }
}
