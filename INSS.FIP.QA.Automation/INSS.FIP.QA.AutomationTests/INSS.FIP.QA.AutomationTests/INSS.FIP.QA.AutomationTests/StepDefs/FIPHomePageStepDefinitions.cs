using System;
using TechTalk.SpecFlow;
using TestFramework.Hooks;
using OpenQA.Selenium;
using INSS.FIP.QA.AutomationTests.StepDefs;

namespace INSS.FIP.QA.AutomationTests.StepDefs
{
    [Binding]
    public class FIPHomePageStepDefinitions : Hooks
    {
     

        [Given(@"I navigate to FIP page")]
        public void GivenINavigateToFIPPage()
        {
            string webPage = "https://app-uksouth-sit-fip-frontend.azurewebsites.net/";
            WebDriver.Navigate().GoToUrl(webPage);
            //WebDriver.Navigate().GoToUrl(baseUrl);
        }
    }
}
