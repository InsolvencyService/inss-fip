using NUnit.Framework;
using OpenQA.Selenium;
using INSS.FIP.QA.Automation.Helpers;
using System;
using System.Threading;

namespace INSS.FIP.QA.Automation.Pages
{
    internal class FIPHomePage : ElementHelper
    {
        private static string pageUrl { get; } = "https://app-uksouth-sit-fip-frontend.azurewebsites.net/";
        private static string pageTitle { get; } = "Search - Find an insolvency practitioner";
        private static string pageHeader { get; } = "Default page template";

        public static void verifyFIPHomePage()
        {
            Assert.IsTrue(WebDriver.Url.Contains(pageUrl));
            Assert.AreEqual(pageTitle, WebDriver.Title);
        }

    }
}
