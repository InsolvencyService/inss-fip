using System;
using TechTalk.SpecFlow;
using INSS.FIP.QA.Automation.Helpers;
using INSS.FIP.QA.Automation.TestFramework;
using INSS.FIP.QA.Automation.Pages;

namespace INSS.FIP.QA.Automation
{
    [Binding]
    public class ATU_211UpdateTermsAndConditionsPageStepDefinitions : ElementHelper
    {
        [Given(@"I navigate to the Terms and conditions page")]
        public void GivenINavigateToTheTermsAndConditionsPage()
        {
            WebDriver.Navigate().GoToUrl(Constants.StartPageUrl);
            StartPage.ClickLink("Terms and conditions - footer");
        }

        [Then(@"the Terms and conditions page is displayed")]
        public void ThenTheTermsAndConditionsPageIsDisplayed()
        {
            TermsAndConditionsPage.verifyFIPTermsAndConditionsPage();
        }

        [When(@"I click the Home breadcrumb on the T&C page")]
        public void WhenIClickTheHomeBreadcrumbOnTheTCPage()
        {
            TermsAndConditionsPage.CLickHomeBreadcrumb();
        }

  
        [When(@"I click the tell the Insolvency Service link on the T&C page")]
        public void WhenIClickTheTellTheInsolvencyServiceLinkOnTheTCPage()
        {
            TermsAndConditionsPage.clickTellTheInsolvencyServiceLink();
        }

        [Then(@"I am navigated to the General Enquiry page")]
        public void ThenIAmNavigatedToTheGeneralEnquiryPage()
        {
            TermsAndConditionsPage.verifyGeneralEnquiryPage();
        }
    }
}
