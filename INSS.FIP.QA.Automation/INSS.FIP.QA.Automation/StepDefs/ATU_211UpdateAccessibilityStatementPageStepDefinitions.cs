using System;
using TechTalk.SpecFlow;
using INSS.FIP.QA.Automation.Helpers;
using INSS.FIP.QA.Automation.TestFramework;
using INSS.FIP.QA.Automation.Pages;

namespace INSS.FIP.QA.Automation
{
    [Binding]
    public class ATU_211UpdateAccessibilityStatementPageStepDefinitions : ElementHelper
    {
        [When(@"I click the ""([^""]*)"" link on the Accessibility Statement page")]
        public void WhenIClickTheLinkOnTheAccessibilityStatementPage(string Link)
        {
            AccessibilityStatementPage.ClickLink(Link);
        }

        [Then(@"the Accessibility statement page will be displayed and the URL, page title and H(.*) will be as per requirements")]
        public void ThenTheAccessibilityStatementPageWillBeDisplayedAndTheURLPageTitleAndHWillBeAsPerRequirements(int p0)
        {
            AccessibilityStatementPage.verifyAccessibilityStatementPage();
        }

        [Given(@"I navigate to the Accessibility Statement Page")]
        public void GivenINavigateToTheAccessibilityStatementPage()
        {
            WebDriver.Navigate().GoToUrl(Constants.StartPageUrl);
            StartPage.ClickLink("Accessibility statement - footer");
        }
    }
}
