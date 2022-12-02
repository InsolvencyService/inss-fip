using NUnit.Framework;
using OpenQA.Selenium;
using INSS.FIP.QA.Automation.Helpers;
using INSS.FIP.QA.Automation.TestFramework;

namespace INSS.FIP.QA.Automation.Pages
{
    internal class SearchPage : ElementHelper
    {
        private static string expectedPageUrl { get; } = string.Concat(Constants.StartPageUrl, "IP/Search");
        private static string expectedPageTitle { get; } = "Search the directory - Find an insolvency practitioner";
        private static string expectedPageHeader { get; } = "Search the directory";
        private static By expectedPageHeaderElement { get; } = By.XPath("//*[@id='main-content']//h1");

        public static void verifyFIPSearchPage()
        {
            Assert.IsTrue(WebDriver.Url.Contains(expectedPageUrl));
            Assert.AreEqual(expectedPageTitle, WebDriver.Title);
            Assert.AreEqual(expectedPageHeader, WebDriver.FindElement(expectedPageHeaderElement).Text);
        }

    }
}
