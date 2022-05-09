using FakeItEasy;
using INSS.FIP.Models.DomainModels;
using INSS.FIP.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Xunit;

namespace INSS.FIP.UnitTests.ControllerTests.IpControllerTests;

[Trait("Category", "IP Controller - Index Unit Tests")]
public class IpControllerIndexTests : BaseIpController
{
    [Fact]
    public async Task IpControllerIndexReturnsSuccess()
    {
        // Arrange
        var serviceWebMessagesGetResults = A.CollectionOfDummy<WebMessageDomainModel>(2);
        using var controller = BuildIpController();

        A.CallTo(() => _fakeWebMessageService.GetAsync(A<string>.Ignored)).Returns(serviceWebMessagesGetResults);

        // Act
        var result = await controller.Index();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<SpecialMessageViewModel>(viewResult.ViewData.Model);

        Assert.NotNull(model);

        A.CallTo(() => _fakeWebMessageService.GetAsync(A<string>.Ignored)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _fakeMapper.Map(A<WebMessageDomainModel>.Ignored, A<SpecialMessageViewModel>.Ignored)).MustHaveHappenedOnceExactly();
    }
}
