using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FakeItEasy;
using INSS.FIP.Interfaces;
using INSS.FIP.Models.ApiConnectorModels;
using INSS.FIP.Services;
using INSS.FIP.Web.UnitTests.FakeHttpHandlers;
using INSS.FIP.Web.UnitTests.TestModels;
using Microsoft.Extensions.Logging;
using Xunit;

namespace INSS.FIP.Web.UnitTests.ConnectorTests;

[Trait("Category", "FIP API connector Unit Tests")]
public class FipApiConnectorTests
{
    private readonly Uri dummyUri = new("https://somewhere.com", UriKind.Absolute);
    private readonly ILogger<ApiConnector> fakeLogger = A.Fake<ILogger<ApiConnector>>();
    private readonly IFakeHttpRequestSender fakeHttpRequestSender = A.Fake<IFakeHttpRequestSender>();
    private readonly IFipApiConnector fipApiConnector;

    public FipApiConnectorTests()
    {
        var httpClient = new HttpClient(new FakeHttpMessageHandler(fakeHttpRequestSender));

        fipApiConnector = new FipApiConnector(fakeLogger, httpClient);
    }

    [Fact]
    public async Task FipApiConnectorGetReturnsOkStatusCodeForValidUrl()
    {
        // arrange
        const HttpStatusCode expectedResult = HttpStatusCode.OK;
        const string expectedName = "Expected name string";
        const string expectedResponse = "{\"name\":\"" + expectedName + "\"}";
        using var httpResponse = new HttpResponseMessage { StatusCode = expectedResult, Content = new StringContent(expectedResponse) };


        A.CallTo(() => fakeHttpRequestSender.Send(A<HttpRequestMessage>.Ignored)).Returns(httpResponse);

        // act
        var result = await fipApiConnector.ProcessAsync<TestApiModel>(new ApiConnectorRequestModel { Uri = dummyUri });

        // assert
        A.CallTo(() => fakeHttpRequestSender.Send(A<HttpRequestMessage>.Ignored)).MustHaveHappenedOnceExactly();
        Assert.Equal(expectedResult, result.StatusCode);
        Assert.True(result.IsSuccessStatusCode);
        Assert.NotNull(result.Payload);
        Assert.Equal(expectedName, result.Payload!.Name);
    }

    [Theory]
    [InlineData(HttpStatusCode.NoContent, true)]
    [InlineData(HttpStatusCode.BadRequest, false)]
    public async Task FipApiConnectorGetReturnsBadStatusCode(HttpStatusCode expectedResult, bool isSuccessStatusCode)
    {
        // arrange
        const string expectedName = "Expected name string";
        const string expectedResponse = "{\"name\":\"" + expectedName + "\"}";
        using var httpResponse = new HttpResponseMessage { StatusCode = expectedResult, Content = new StringContent(expectedResponse) };


        A.CallTo(() => fakeHttpRequestSender.Send(A<HttpRequestMessage>.Ignored)).Returns(httpResponse);

        // act
        var result = await fipApiConnector.ProcessAsync<TestApiModel>(new ApiConnectorRequestModel { Uri = dummyUri });

        // assert
        A.CallTo(() => fakeHttpRequestSender.Send(A<HttpRequestMessage>.Ignored)).MustHaveHappenedOnceExactly();
        Assert.Equal(expectedResult, result.StatusCode);
        Assert.Equal(isSuccessStatusCode, result.IsSuccessStatusCode);
        Assert.Null(result.Payload);
    }

    [Fact]
    public async Task FipApiConnectorGetCatchesExceptionFromBadUri()
    {
        // arrange
        const HttpStatusCode expectedResult = HttpStatusCode.InternalServerError;
        const string expectedName = "Expected name string";
        const string expectedResponse = "{\"name\":\"" + expectedName + "\"}";
        using var httpResponse = new HttpResponseMessage { StatusCode = expectedResult, Content = new StringContent(expectedResponse) };


        A.CallTo(() => fakeHttpRequestSender.Send(A<HttpRequestMessage>.Ignored)).Returns(httpResponse);

        // act
        var result = await fipApiConnector.ProcessAsync<TestApiModel>(new ApiConnectorRequestModel ());

        // assert
        A.CallTo(() => fakeHttpRequestSender.Send(A<HttpRequestMessage>.Ignored)).MustNotHaveHappened();
        Assert.Equal(expectedResult, result.StatusCode);
        Assert.False(result.IsSuccessStatusCode);
        Assert.Null(result.Payload);
    }
}
