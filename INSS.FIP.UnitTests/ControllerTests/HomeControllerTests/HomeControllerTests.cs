using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace INSS.FIP.UnitTests.ControllerTests.HomeControllerTests;

[Trait("Category", "Home Controller - Unit Tests")]

public class HomeControllerTests : BaseHomeControllerTests
{
    [Fact]
    public void HomeControllerAccessibilityStatementReturnsSuccess()
    {
        // Arrange
        using var controller = BuildHomeController();

        // Act
        var result = controller.AccessibilityStatement();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public void HomeControllerContactReturnsSuccess()
    {
        // Arrange
        using var controller = BuildHomeController();

        // Act
        var result = controller.Contact();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public void HomeControllerCookeiesReturnsSuccess()
    {
        // Arrange
        using var controller = BuildHomeController();

        // Act
        var result = controller.Cookies();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public void HomeControllerControllerPrivacyPolicyReturnsSuccess()
    {
        // Arrange
        using var controller = BuildHomeController();

        // Act
        var result = controller.PrivacyPolicy();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public void HomeControllerControllerTermsAndConditionsReturnsSuccess()
    {
        // Arrange
        using var controller = BuildHomeController();

        // Act
        var result = controller.TermsAndConditions();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
    }
}
