using System.Net.Http;

namespace INSS.FIP.Web.UnitTests.FakeHttpHandlers;

public interface IFakeHttpRequestSender
{
    HttpResponseMessage Send(HttpRequestMessage request);
}
