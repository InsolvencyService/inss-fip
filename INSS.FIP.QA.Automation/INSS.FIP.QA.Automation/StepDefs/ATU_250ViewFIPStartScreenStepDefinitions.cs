using TechTalk.SpecFlow;
using INSS.FIP.QA.Automation.Helpers;
using INSS.FIP.QA.Automation.TestFramework;
using INSS.FIP.QA.Automation.Pages;

namespace INSS.FIP.QA.Automation
{
    [Binding]
    public class ATU_250ViewFIPStartScreenStepDefinitions : ElementHelper
    {
        [Given(@"I navigate to the FIP Start Page")]
        public void GivenINavigateToTheFIPStartPage()
        {
            WebDriver.Navigate().GoToUrl(Constants.StartPageUrl);
        }

        [Then(@"the FIP Start page will be displayed and the URL, page title and the page heading will be as per requirements")]
        public void ThenTheFIPStartPageWillBeDisplayedAndTheURLPageTitleAndThePageHeadingWillBeAsPerRequirements()
        {
            StartPage.verifyFIPStartPage();
        }

        [When(@"I click the '([^']*)' button")]
        public void WhenIClickTheButton(string p0)
        {
            StartPage.ClickStartButton();
        }

        [When(@"I click the ""([^""]*)"" link")]
        public void WhenIClickTheLink(string Link)
        {
            StartPage.ClickLink(Link);
        }

        [Then(@"the following URL is displayed ""([^""]*)""")]
        public void ThenTheFollowingURLIsDisplayed(string URL)
        {
            StartPage.verifyPageURL(URL);
        }

        [Then(@"I am taken to the Search page")]
        public void ThenIAmTakenToTheSearchPage()
        {
            SearchPage.verifyFIPSearchPage();
        }

    }
}
