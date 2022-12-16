using NUnit.Framework;
using OpenQA.Selenium;
using INSS.FIP.QA.Automation.Helpers;
using INSS.FIP.QA.Automation.TestFramework;

namespace INSS.FIP.QA.Automation.Pages
{
    internal class IPDetailsPage : ElementHelper
    {
        private static string expectedPageUrl { get; } = string.Concat(Constants.StartPageUrl, "insolvency-practitioner-details");
        private static string expectedPageTitle { get; } = "Insolvency practitioner details - Find an insolvency practitioner";
        private static string expectedPageHeader { get; } = "Insolvency practitioner details";

        private static By expectedPageHeaderElement { get; } = By.XPath("//*[@id='main-content']//h1");
        private static By homeBreadcrumb { get; } = By.LinkText("Home");
        private static By searchBreadcrumb { get; } = By.LinkText("Search");
        private static By searchResultsBreadcrumb { get; } = By.LinkText("Search results");
        private static By InsolvencyPractitionerDetailsBreadcrumb { get; } = By.XPath("/html/body/div[2]/div/ol/li[4]");
        private static By FindAnotherInsolvencyPractitionerButton { get; } = By.XPath("//*[@class='govuk-button']");
        private static By IPDetailsElement { get; } = By.XPath(" //*[@id='main-content']//div[1]/dl");
       
        public static void verifyIPDetailsPage()
        {
            Assert.IsTrue(WebDriver.Url.Contains(expectedPageUrl));
            Assert.AreEqual(expectedPageTitle, WebDriver.Title);
            Assert.AreEqual(expectedPageHeader, WebDriver.FindElement(expectedPageHeaderElement).Text);
        }

        public static void ClickHomeBreadcrumb()
        {
            ClickElement(homeBreadcrumb);           
        }
        public static void ClickSearchBreadcrumb()
        {
            ClickElement(searchBreadcrumb);
        }
        public static void ClickSearchResultsBreadcrumb()
        {
            ClickElement(searchResultsBreadcrumb);
        }

        public static void ClickFIndAnotherInsolvencyPractitionerButton()
        {
            ClickElement(FindAnotherInsolvencyPractitionerButton);
        }

        public static void VerifyBreadcrumbText()
        {
            Assert.AreEqual("Home", WebDriver.FindElement(homeBreadcrumb).Text);
            Assert.AreEqual("Search", WebDriver.FindElement(searchBreadcrumb).Text);
            Assert.AreEqual("Search results", WebDriver.FindElement(searchResultsBreadcrumb).Text);
            Assert.AreEqual("Insolvency practitioner details", WebDriver.FindElement(InsolvencyPractitionerDetailsBreadcrumb).Text);
        }
        

        public static void verifyIPDetailsforIPPractitioner()
        {
           Assert.IsTrue(WebDriver.FindElement(IPDetailsElement).Text.Contains(Constants.firstName));
            Assert.IsTrue(WebDriver.FindElement(IPDetailsElement).Text.Contains(Constants.surname));
            Assert.IsTrue(WebDriver.FindElement(IPDetailsElement).Text.Contains(Constants.companyName));
            Assert.IsTrue(WebDriver.FindElement(IPDetailsElement).Text.Contains(Constants.IPNumber));
            Assert.IsTrue(WebDriver.FindElement(IPDetailsElement).Text.Contains(Constants.authorisingBody));
            Assert.IsTrue(WebDriver.FindElement(IPDetailsElement).Text.Contains(Constants.telephone));
            Assert.IsTrue(WebDriver.FindElement(IPDetailsElement).Text.Contains(Constants.fax));
            Assert.IsTrue(WebDriver.FindElement(IPDetailsElement).Text.Contains(Constants.email));
        }
    }
}
