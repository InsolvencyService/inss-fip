using System.Net.Http;

namespace INSS.FIP.UnitTests.FakeHttpHandlers;

public interface IFakeHttpRequestSender
{
    HttpResponseMessage Send(HttpRequestMessage request);
}
