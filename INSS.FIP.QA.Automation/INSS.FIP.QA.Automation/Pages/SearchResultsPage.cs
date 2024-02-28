using NUnit.Framework;
using OpenQA.Selenium;
using INSS.FIP.QA.Automation.Helpers;
using INSS.FIP.QA.Automation.TestFramework;

namespace INSS.FIP.QA.Automation.Pages
{
    internal class SearchResultsPage : ElementHelper
    {
        private static string expectedPageUrl { get; } = string.Concat(Constants.StartPageUrl, "search-results");
        private static string expectedPageTitle { get; } = "Search results - Find an insolvency practitioner";
        private static string expectedPageHeader { get; } = "Search results";

        private static By expectedPageHeaderElement { get; } = By.XPath("//*[@id='main-content']//h1");
      
        private static By SearchResultsFirstRecordLink { get; } = By.XPath("//*[@id='main-content']//td[1]/a");
        private static By homeBreadcrumb { get; } = By.LinkText("Home");
        private static By searchBreadcrumb { get; } = By.LinkText("Search");
        private static By searchResultsBreadcrumb { get; } = By.XPath("/html/body/div[2]/div/ol/li[3]");
        private static By NameElement { get; } = By.XPath("//*[@id='main-content']/div[3]/form/table/tbody[1]/tr/td[1]");
                                                 
        private static By CompanyElement { get; } = By.XPath("//*[@id='main-content']//table/tbody[1]/tr/td[2]");
        private static By TownElement { get; } = By.XPath("//*[@id='main-content']//table/tbody[1]/tr/td[3]");
        private static By PostcodeElement { get; } = By.XPath("//*[@id='main-content']//table/tbody[1]/tr/td[4]");


        public static void verifyFIPSearchResultsPage()
        {
            Assert.IsTrue(WebDriver.Url.Contains(expectedPageUrl));
            Assert.AreEqual(expectedPageTitle, WebDriver.Title);
            Assert.AreEqual(expectedPageHeader, WebDriver.FindElement(expectedPageHeaderElement).Text);
        }
        public static void ClickFirstRecord()
        {
            ClickElement(SearchResultsFirstRecordLink);
        }

        public static void ClickHomeBreadcrumb()
        {
            ClickElement(homeBreadcrumb);
        }
        public static void ClickSearchBreadcrumb()
        {
            ClickElement(searchBreadcrumb);
        }

        public static void verifySearchResultsRecord()
        {
            string FullName = Constants.title + " " + Constants.firstName + " " + Constants.surname;
            Assert.AreEqual(FullName, WebDriver.FindElement(NameElement).Text);
            Assert.AreEqual(Constants.companyName, WebDriver.FindElement(CompanyElement).Text);
            Assert.AreEqual(Constants.town, WebDriver.FindElement(TownElement).Text);
            Assert.AreEqual(Constants.postcode, WebDriver.FindElement(PostcodeElement).Text);
        }

        public static void VerifyBreadcrumbText()
        {
            Assert.AreEqual("Home", WebDriver.FindElement(homeBreadcrumb).Text);
            Assert.AreEqual("Search", WebDriver.FindElement(searchBreadcrumb).Text);
            Assert.AreEqual("Search results", WebDriver.FindElement(searchResultsBreadcrumb).Text);
        }
    }
}
