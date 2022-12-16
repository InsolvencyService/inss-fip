using System;
using TechTalk.SpecFlow;
using INSS.FIP.QA.Automation.Helpers;
using INSS.FIP.QA.Automation.TestFramework;
using INSS.FIP.QA.Automation.Pages;

namespace INSS.FIP.QA.Automation
{
    [Binding]
    public class ATU_214FIPSearchScreenStepDefinitions : ElementHelper
    {
        [Given(@"I navigate to the FIP search Page")]
        public void GivenINavigateToTheFIPSearchPage()
        {
            WebDriver.Navigate().GoToUrl(Constants.StartPageUrl);
            StartPage.ClickStartButton();
        }

        [Then(@"the FIP search page will be displayed and the URL, page title and the page heading will be as per requirements")]
        public void ThenTheFIPSearchPageWillBeDisplayedAndTheURLPageTitleAndThePageHeadingWillBeAsPerRequirements()
        {
            SearchPage.verifyFIPSearchPage();
        }

        [When(@"I press the Search button without entering any search values")]
        public void WhenIPressTheSearchButtonWithoutEnteringAnySearchValues()
        {
            throw new PendingStepException();
        }

        [Then(@"I am shown the following error message")]
        public void ThenIAmShownTheFollowingErrorMessage()
        {
            throw new PendingStepException();
        }

        [When(@"I enter valid values but I enter an invalid First name")]
        public void WhenIEnterValidValuesButIEnterAnInvalidFirstName()
        {
            throw new PendingStepException();
        }

        [When(@"I press the Search button")]
        public void WhenIPressTheSearchButton()
        {
            SearchPage.CLickSearchButton();
        }

        [Then(@"I am shown the following error message at the top of the page ""([^""]*)""")]
        public void ThenIAmShownTheFollowingErrorMessageAtTheTopOfThePage(string p0)
        {
            throw new PendingStepException();
        }

        [When(@"I enter valid values but I enter an invalid Last name")]
        public void WhenIEnterValidValuesButIEnterAnInvalidLastName()
        {
            throw new PendingStepException();
        }

        [When(@"I enter valid values but I enter an invalid Company name")]
        public void WhenIEnterValidValuesButIEnterAnInvalidCompanyName()
        {
            throw new PendingStepException();
        }

        [When(@"I enter valid values but I enter an invalid Town name")]
        public void WhenIEnterValidValuesButIEnterAnInvalidTownName()
        {
            throw new PendingStepException();
        }

        [When(@"I enter valid values but I enter an invalid Postcode")]
        public void WhenIEnterValidValuesButIEnterAnInvalidPostcode()
        {
            throw new PendingStepException();
        }

        [When(@"I press the Home breadcrumb on the FIP Search page")]
        public void WhenIPressTheHomeBreadcrumbOnTheFIPSearchPage()
        {
            SearchPage.ClickHomeBreadcrumb();
        }

        [When(@"I search for values which return no results")]
        public void WhenISearchForValuesWhichReturnNoResults()
        {
            SearchPage.EnterFirstName("RUBBISHABCD");
            SearchPage.CLickSearchButton();
        }

        [Then(@"the search page will show a statement stating '([^']*)'")]
        public void ThenTheSearchPageWillShowAStatementStating(string ExpectedText)
        {
            SearchPage.verifySearchPageNoResults(ExpectedText);
        }
    }
}
