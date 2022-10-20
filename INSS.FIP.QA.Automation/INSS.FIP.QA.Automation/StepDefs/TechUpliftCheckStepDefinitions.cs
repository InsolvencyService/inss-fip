using System;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using INSS.FIP.QA.Automation.StepDefs;
using INSS.FIP.QA.Automation.Helpers;
using INSS.FIP.QA.Automation.TestFramework;
using INSS.FIP.QA.Automation.Pages;
using NUnit.Framework;


namespace INSS.FIP.QA.Automation.StepDefs
{
    [Binding]
    public class TechUpliftCheckStepDefinitions : ElementHelper
    {
     

        [Given(@"I navigate to Amazon Login page")]
        public void GivenINavigateToAmazonLoginPage()
        {           
            WebDriver.Navigate().GoToUrl(Constants.StartPageUrl);            
        }

        [Given(@"I navigate to the FIP Home Page")]
        public void GivenINavigateToTheFIPHomePage()
        {
            WebDriver.Navigate().GoToUrl(Constants.StartPageUrl);
        }

        [Then(@"the URL will be '([^']*)'")]
        public void ThenTheURLWillBe(string expectedURL)
        {
            FIPHomePage.verifyFIPHomePage();
        }

        [Given(@"SOmething Happens")]
        public void GivenSOmethingHappens()
        {
           //
        }

        [Then(@"again something happens")]
        public void ThenAgainSomethingHappens()
        {
            //
        }




    }
}
