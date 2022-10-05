using System.Net;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace INSS.FIP.Web.UnitTests.ControllerTests.HealthControllerTests;

[Trait("Category", "Health Controller Unit Tests")]
public class HealthControllerPingTests : BaseHealthControllerTests
{
    [Fact]
    public void HealthControllerPingReturnsSuccess()
    {
        // Arrange
        using var controller = BuildHealthController();

        // Act
        var result = controller.Ping();

        // Assert
        var statusResult = Assert.IsType<OkResult>(result);

        Assert.Equal((int)HttpStatusCode.OK, statusResult.StatusCode);
    }
}
