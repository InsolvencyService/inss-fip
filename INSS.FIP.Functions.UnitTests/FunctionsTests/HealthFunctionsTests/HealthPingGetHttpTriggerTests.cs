using INSS.FIP.Functions.Functions.Health;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace INSS.FIP.Functions.UnitTests.FunctionsTests.HealthFunctionsTests;

[Trait("Category", "Health Function - Unit Tests")]
public class HealthPingGetHttpTriggerTests
{
    [Fact]
    public void HealthPingGetHttpTriggerTestsReturnsOk()
    {
        // Arrange

        // Act
        var result = HealthPingGetHttpTrigger.Run(new DefaultHttpContext().Request);

        // Assert
        Assert.IsType<OkResult>(result);
    }
}
