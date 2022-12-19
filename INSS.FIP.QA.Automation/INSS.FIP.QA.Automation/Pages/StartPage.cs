using NUnit.Framework;
using OpenQA.Selenium;
using INSS.FIP.QA.Automation.Helpers;
using INSS.FIP.QA.Automation.TestFramework;

namespace INSS.FIP.QA.Automation.Pages
{
    internal class StartPage : ElementHelper
    {
        private static string expectedPageUrl { get; } =Constants.StartPageUrl;
        private static string expectedPageTitle { get; } = "Find an insolvency practitioner - Find an insolvency practitioner";
        private static string expectedPageHeader { get; } = "Find an insolvency practitioner";
        private static By expectedPageHeaderElement { get; } = By.XPath("//*[@id='main-content']//h1");
        private static By startSearchButton { get; } = By.XPath("//*[@href='/IP/Search']");
        private static By privacyFooterLink { get; } = By.XPath("//footer//li[1]/a");
        private static By accessibilityFooterLink { get; } = By.XPath("//footer//li[2]/a");
        private static By termsAndConditionsFooterLink { get; } = By.XPath("//footer//li[3]/a");
        private static By GetHelpFromTheInsolvencyLink { get; } = By.PartialLinkText("Get help");
        private static By FindOutMoreLink { get; } = By.PartialLinkText("Find out more");
        private static By GiveFeedbackLink { get; } = By.PartialLinkText("Give feedback");

        public static void verifyFIPStartPage()
        {
            Assert.IsTrue(WebDriver.Url.Contains(expectedPageUrl));
            Assert.AreEqual(expectedPageTitle, WebDriver.Title);
            Assert.AreEqual(expectedPageHeader, WebDriver.FindElement(expectedPageHeaderElement).Text);
        }

        public static void ClickStartButton()
        {
            ClickElement(startSearchButton);
        }

        public static void ClickLink(string Linkname)
        {
            switch (Linkname)
            {
                case "Terms and conditions - footer":
                    ClickElement(termsAndConditionsFooterLink);
                    break;
                case "Privacy - footer":
                    ClickElement(privacyFooterLink);
                    break;
                case "Accessibility statement - footer":
                    ClickElement(accessibilityFooterLink);
                    break;
                case "Related content - Get help from the Insolvency Service":
                    ClickElement(GetHelpFromTheInsolvencyLink);
                    break;
                case "Related content - Find out more about bankruptcy and insolvency":
                    ClickElement(FindOutMoreLink);
                    break;
                case "Related content - Give feedback about the Individual Insolvency Register":
                    ClickElement(GiveFeedbackLink);
                    break;
            }
        }

        public static void verifyPageURL(string URL)
        {
            Assert.AreEqual(URL, WebDriver.Url);
        }
    }
}
