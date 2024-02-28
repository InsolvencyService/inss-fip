using System;
using TechTalk.SpecFlow;
using INSS.FIP.QA.Automation.Helpers;
using INSS.FIP.QA.Automation.TestFramework;
using INSS.FIP.QA.Automation.Pages;
using INSS.FIP.QA.Automation.Data;


namespace INSS.FIP.QA.Automation
{
    [Binding]
    public class ATU_104ViewIPDetailsScreenStepDefinitions :ElementHelper
    {
        string IPDetailsPageURL = "";


        [Given(@"I navigate to the Insolvency Practitioner details page")]
        public void GivenINavigateToTheInsolvencyPractitionerDetailsPage()
        {
            WebDriver.Navigate().GoToUrl(Constants.StartPageUrl);
            StartPage.ClickStartButton();
            SearchPage.EnterFirstName(Constants.firstName);
            SearchPage.CLickSearchButton();
            SearchResultsPage.ClickFirstRecord();
            IPDetailsPageURL = WebDriver.Url;
        }

        [Then(@"the URL, page title and page heading will be displayed for the IP Details page")]
        public void ThenTheURLPageTitleAndPageHeadingWillBeDisplayedForTheIPDetailsPage()
        {
            IPDetailsPage.verifyIPDetailsPage();
        }


        [Then(@"the breadcrumb text will be as expected on the IP Details Screen")]
        public void ThenTheBreadcrumbTextWillBeAsExpectedOnTheIPDetailsScreen()
        {
            IPDetailsPage.VerifyBreadcrumbText();
        }

        [Given(@"I click the Home breadcrumb on the IP Details Screen")]
        [Then(@"I click the Home breadcrumb on the IP Details Screen")]
        public void ThenIClickTheHomeBreadcrumbOnTheIPDetailsScreen()
        {
            IPDetailsPage.ClickHomeBreadcrumb();
        }

         [Given(@"I create an Insolvency Practitioner record")]
        public void GivenICreateAnInsolvencyPractitionerRecord()
        {
            SqlQueries.DeleteCI_CPRecord();
            SqlQueries.createCI_IPRecord();
        }

        [Given(@"I navigate back to the Insolvency Practitioner details page")]
        public void GivenINavigateBackToTheInsolvencyPractitionerDetailsPage()
        {
            WebDriver.Navigate().GoToUrl(IPDetailsPageURL);
        }

        [Given(@"I click the Search results breadcrumb on the IP Details Screen")]
        public void GivenIClickTheSearchResultsBreadcrumbOnTheIPDetailsScreen()
        {
            IPDetailsPage.ClickSearchResultsBreadcrumb();
        }

        [Then(@"the Search results page is displayed")]
        public void ThenTheSearchResultsPageIsDisplayed()
        {
            SearchResultsPage.verifyFIPSearchResultsPage();
        }


        [Given(@"I click the Search breadcrumb on the IP Details Screen")]
        public void GivenIClickTheSearchBreadcrumbOnTheIPDetailsScreen()
        {
            IPDetailsPage.ClickSearchBreadcrumb();
        }
        [Then(@"I am shown the Search page")]
        public void ThenIAmShownTheSearchPage()
        {
            SearchPage.verifyFIPSearchPage();
        }

        [When(@"I click the FInd another Insolvency Practitioner button")]
        public void WhenIClickTheFIndAnotherInsolvencyPractitionerButton()
        {
            IPDetailsPage.ClickFIndAnotherInsolvencyPractitionerButton();
        }

        [Then(@"the details for the Insolvency Practitioner are displayed")]
        public void ThenTheDetailsForTheInsolvencyPractitionerAreDisplayed()
        {
            IPDetailsPage.verifyIPDetailsforIPPractitioner();
        }
    }
}
