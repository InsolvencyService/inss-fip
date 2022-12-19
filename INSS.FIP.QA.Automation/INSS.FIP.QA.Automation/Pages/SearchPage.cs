using NUnit.Framework;
using OpenQA.Selenium;
using INSS.FIP.QA.Automation.Helpers;
using INSS.FIP.QA.Automation.TestFramework;

namespace INSS.FIP.QA.Automation.Pages
{
    internal class SearchPage : ElementHelper
    {
        private static string expectedPageUrl { get; } = string.Concat(Constants.StartPageUrl, "search");
        private static string expectedPageTitle { get; } = "Search - Find an insolvency practitioner";
        private static string expectedPageHeader { get; } = "Search";
        private static By expectedPageHeaderElement { get; } = By.XPath("//*[@id='main-content']//h1");
        private static By searchButton { get; } = By.XPath("//button");
        private static By firstNameField { get; } = By.Id("FirstName");
        private static By lastNameField { get; } = By.Id("LastName");
        private static By companyField { get; } = By.Id("Company");
        private static By townField { get; } = By.Id("Town");
        private static By postcodeField { get; } = By.Id("Postcode");
        private static By MainErroMessageElement = By.XPath("//*[@id='main-content']/div[2]/div[1]/div/div/ul/li/a");
        private static By SubErroMessageElement = By.Id("organisationName-error");
        private static By homeBreadcrumb { get; } = By.LinkText("Home");
        private static By searchBreadcrumb { get; } = By.XPath("/html/body/div[2]/div/ol/li[2]");

        public static void verifyFIPSearchPage()
        {
            Assert.IsTrue(WebDriver.Url.Contains(expectedPageUrl));
            Assert.AreEqual(expectedPageTitle, WebDriver.Title);
            Assert.AreEqual(expectedPageHeader, WebDriver.FindElement(expectedPageHeaderElement).Text);
        }

        public static void VerifyErrorMessage(string expectedErrorMessage)
        {
            Assert.AreEqual(expectedErrorMessage, WebDriver.FindElement(MainErroMessageElement).Text);
            Assert.AreEqual(expectedErrorMessage, WebDriver.FindElement(SubErroMessageElement).Text);
        }

        public static void VerifySingleErrorMessage(string expectedErrorMessage)
        {
            Assert.AreEqual(expectedErrorMessage, WebDriver.FindElement(MainErroMessageElement).Text);           
        }
        
        public static void EnterFirstName(string FirstName)
        {
            EnterText(firstNameField, FirstName);
        }

        public static void EnterSurname(string Surname)
        {
            EnterText(lastNameField, Surname);
        }

        public static void CLickSearchButton()
        {
            ClickButton(searchButton);
        }

        public static void EnterTown(string Town)
        {
            EnterText(townField, Town);
        }

        public static void EnterPostcode(string Postcode)
        {
            EnterText(postcodeField, Postcode);
        }

        public static void EnterCompanyName(string CompanyName)
        {
            EnterText(companyField, CompanyName);
        }

        public static void ClickHomeBreadcrumb()
        {
            ClickElement(homeBreadcrumb);
        }

        public static void verifySearchPageNoResults(string ExpectedText)
        {
            Assert.IsTrue(WebDriver.Url.Contains(expectedPageUrl));
            Assert.AreEqual(expectedPageTitle, WebDriver.Title);
            Assert.AreEqual(expectedPageHeader, WebDriver.FindElement(expectedPageHeaderElement).Text);
            Assert.IsTrue(WebDriver.FindElement(By.XPath("//*[@id='main-content']/div[2]")).Text.Contains(ExpectedText));
        }

        public static void VerifyBreadcrumbText()
        {
            Assert.AreEqual("Home", WebDriver.FindElement(homeBreadcrumb).Text);
            Assert.AreEqual("Search", WebDriver.FindElement(searchBreadcrumb).Text);           
        }
    }
}
