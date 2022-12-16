using System;
using TechTalk.SpecFlow;
using INSS.FIP.QA.Automation.Helpers;
using INSS.FIP.QA.Automation.TestFramework;
using INSS.FIP.QA.Automation.Pages;


namespace INSS.FIP.QA.Automation
{
    [Binding]
    public class ATU_103SearchResultsPageStepDefinitions : ElementHelper
    {

        string SearchResultsPageURL = "";

        [Given(@"I navigate to the Search results page")]
        public void GivenINavigateToTheSearchResultsPage()
        {
            WebDriver.Navigate().GoToUrl(Constants.StartPageUrl);
            StartPage.ClickStartButton();
            SearchPage.EnterFirstName(Constants.firstName);
            SearchPage.CLickSearchButton();
            SearchResultsPageURL = WebDriver.Url;
        }

        [Then(@"the URL, page title and page heading will be displayed for the Search results page")]
        public void ThenTheURLPageTitleAndPageHeadingWillBeDisplayedForTheSearchResultsPage()
        {
            SearchResultsPage.verifyFIPSearchResultsPage();
            SearchResultsPage.VerifyBreadcrumbText();
        }

        [When(@"I press the Home breadcrumb on the Search results page")]
        public void WhenIPressTheHomeBreadcrumbOnTheSearchResultsPage()
        {
            SearchResultsPage.ClickHomeBreadcrumb();
        }

        [Given(@"I navigate back to the Search results page")]
        public void GivenINavigateBackToTheSearchResultsPage()
        {
            WebDriver.Navigate().GoToUrl(SearchResultsPageURL);
        }

        [Given(@"I click the Search breadcrumb on the Search results page")]
        public void GivenIClickTheSearchBreadcrumbOnTheSearchResultsPage()
        {
            SearchResultsPage.ClickSearchBreadcrumb();
        }

        [Given(@"I navigate to the Search results by searching by first name")]
        public void GivenINavigateToTheSearchResultsBySearchingByFirstName()
        {
            WebDriver.Navigate().GoToUrl(Constants.StartPageUrl);
            StartPage.ClickStartButton();
            SearchPage.EnterFirstName(Constants.firstName);
            SearchPage.CLickSearchButton();
        }

        [Given(@"I navigate to the Search results by searching by Surname")]
        public void GivenINavigateToTheSearchResultsBySearchingBySurname()
        {
            WebDriver.Navigate().GoToUrl(Constants.StartPageUrl);
            StartPage.ClickStartButton();
            SearchPage.EnterSurname(Constants.surname);
            SearchPage.CLickSearchButton();
        }

        [Given(@"I navigate to the Search results by searching by Company Name")]
        public void GivenINavigateToTheSearchResultsBySearchingByCompanyName()
        {
            WebDriver.Navigate().GoToUrl(Constants.StartPageUrl);
            StartPage.ClickStartButton();
            SearchPage.EnterCompanyName(Constants.companyName);
            SearchPage.CLickSearchButton();
        }

        [Given(@"I navigate to the Search results by searching by Town or city")]
        public void GivenINavigateToTheSearchResultsBySearchingByTownOrCity()
        {
            WebDriver.Navigate().GoToUrl(Constants.StartPageUrl);
            StartPage.ClickStartButton();
            SearchPage.EnterTown(Constants.town);
            SearchPage.CLickSearchButton();
        }

        [Given(@"I navigate to the Search results by searching by Postcode")]
        public void GivenINavigateToTheSearchResultsBySearchingByPostcode()
        {
            WebDriver.Navigate().GoToUrl(Constants.StartPageUrl);
            StartPage.ClickStartButton();
            SearchPage.EnterPostcode(Constants.postcode);
            SearchPage.CLickSearchButton();
        }

        [Then(@"I am shown the search results in a table for the record I searched for")]
        public void ThenIAmShownTheSearchResultsInATableForTheRecordISearchedFor()
        {
            SearchResultsPage.verifySearchResultsRecord();
        }

        [Given(@"I navigate to the Search results by searching by First name, Surname, Company name and Postcode")]
        public void GivenINavigateToTheSearchResultsBySearchingByFirstNameSurnameCompanyNameAndPostcode()
        {
            WebDriver.Navigate().GoToUrl(Constants.StartPageUrl);
            StartPage.ClickStartButton();
            SearchPage.EnterFirstName(Constants.firstName);
            SearchPage.EnterSurname(Constants.surname);
            SearchPage.EnterCompanyName(Constants.companyName);
            SearchPage.EnterTown(Constants.town);
            SearchPage.EnterPostcode(Constants.postcode);
            SearchPage.CLickSearchButton();
        }

    }
}
