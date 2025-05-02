using System.Threading.Tasks;
using FakeItEasy;
using INSS.FIP.Models.DomainModels;
using INSS.FIP.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace INSS.FIP.Web.UnitTests.ControllerTests.IpControllerTests;

[Trait("Category", "IP Controller - Index Unit Tests")]
public class IpControllerIndexTests : BaseIpController
{
    [Fact]
    public async Task IpControllerIndexReturnsSuccess()
    {
        // Arrange
        var serviceGetWebPageBannerMessagesGetResults = A.CollectionOfDummy<GetWebPageBannerMessageDomainModel>(2);
        using var controller = BuildIpController();

        A.CallTo(() => _fakeGetWebPageBannerMessageService.GetAsync(A<string>.Ignored)).Returns(serviceGetWebPageBannerMessagesGetResults);

        // Act
        var result = await controller.Index();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<SpecialMessageViewModel>(viewResult.ViewData.Model);

        Assert.NotNull(model);

        A.CallTo(() => _fakeGetWebPageBannerMessageService.GetAsync(A<string>.Ignored)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _fakeMapper.Map(A<GetWebPageBannerMessageDomainModel>.Ignored, A<SpecialMessageViewModel>.Ignored)).MustHaveHappenedOnceExactly();
    }
}
