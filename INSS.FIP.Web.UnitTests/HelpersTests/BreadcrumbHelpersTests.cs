using INSS.FIP.Web.Helpers;
using Xunit;

namespace INSS.FIP.Web.UnitTests.HelpersTests;

[Trait("Category", "Breadcrumb helper - Unit Tests")]
public class BreadcrumbHelpersTests
{
    [Theory]
    [InlineData(1, false, false, false, default, default)]
    [InlineData(2, true, false, false, default, default)]
    [InlineData(2, false, true, false, default, default)]
    [InlineData(2, false, false, true, default, default)]
    [InlineData(4, true, true, true, default, default)]
    [InlineData(4, true, true, true, 123, "a name")]
    public void BreadcrumbHelpersBuildBreadcrumbsReturnsSuccess(int expectedCount, bool showSearch, bool showResults, bool showIp, int? ipNumber, string? ipName)
    {
        // Arrange

        // Act
        var result = BreadcrumbHelpers.BuildBreadcrumbs(showSearch, showResults, showIp, ipNumber, ipName);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedCount, result.Count);
    }
}
