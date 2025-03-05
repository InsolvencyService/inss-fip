using NUnit.Framework;
using OpenQA.Selenium;
using INSS.FIP.QA.Automation.Helpers;
using INSS.FIP.QA.Automation.TestFramework;

namespace INSS.FIP.QA.Automation.Pages
{
    internal class TermsAndConditionsPage : ElementHelper
    {
        private static string expectedPageUrl { get; } = string.Concat(Constants.StartPageUrl, "home/terms-and-conditions");
        private static string expectedPageTitle { get; } = "Terms and conditions - Find an insolvency practitioner";
        private static string expectedPageHeader { get; } = "Terms and conditions";
        private static By expectedPageHeaderElement { get; } = By.XPath("//*[@id='main-content']//h1");
        private static string generalEnquiryPageURL = "https://www.insolvencydirect.bis.gov.uk/ExternalOnlineForms/GeneralEnquiry.aspx";
        private static By homeBreadcrumb { get; } = By.LinkText("Home");

        public static void verifyFIPTermsAndConditionsPage()
        {
            Assert.IsTrue(WebDriver.Url.Contains(expectedPageUrl));
            Assert.AreEqual(expectedPageTitle, WebDriver.Title);
            Assert.AreEqual(expectedPageHeader, WebDriver.FindElement(expectedPageHeaderElement).Text);
        }

        public static void clickTellTheInsolvencyServiceLink()
        {
            WebDriver.FindElement(By.PartialLinkText("the Insolvency Service")).Click();
        }
        public static void verifyGeneralEnquiryPage()
        {
            Assert.AreEqual(generalEnquiryPageURL, WebDriver.Url);
        }

        public static void CLickHomeBreadcrumb()
        {
            ClickElement(homeBreadcrumb);
        }
    }
}
