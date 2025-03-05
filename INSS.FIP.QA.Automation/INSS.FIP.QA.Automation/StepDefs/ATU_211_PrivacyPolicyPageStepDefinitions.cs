using System;
using TechTalk.SpecFlow;
using INSS.FIP.QA.Automation.Helpers;
using INSS.FIP.QA.Automation.TestFramework;
using INSS.FIP.QA.Automation.Pages;

namespace INSS.FIP.QA.Automation
{
    [Binding]
    public class ATU_211_PrivacyPolicyPageStepDefinitions : ElementHelper
    {
        [Given(@"I navigate to the Privacy policy page")]
        public void GivenINavigateToThePrivacyPolicyPage()
        {
            WebDriver.Navigate().GoToUrl(Constants.StartPageUrl);
            StartPage.ClickLink("Privacy - footer");
        }


        [Then(@"the Privacy policy page is displayed with the correct URL, page title and header")]
        public void ThenThePrivacyPolicyPageIsDisplayedWithTheCorrectURLPageTitleAndHeader()
        {
            PrivacyPolicyPage.verifyPrivacyPage();
        }

        [Given(@"I click the ""([^""]*)"" breadcrumb on the Privacy policy page")]
        [Given(@"I click the ""([^""]*)"" link on the Privacy policy page")]
        public void GivenIClickTheLinkOnThePrivacyPolicyPage(string Link)
        {
            PrivacyPolicyPage.ClickLink(Link);
        }
    }
}
