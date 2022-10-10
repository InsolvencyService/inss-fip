using System;
using System.Configuration;
using TechTalk.SpecFlow;
using System.IO;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using TestFramework.Hooks;
using OpenQA.Selenium;

namespace INSS.FIP.QA.AutomationTests.StepDefs
{
    [Binding]
    public class Hooks
    {
        //Context _context;
        //static ExtentTest feature;
        //static ExtentTest scenario;
        //static ExtentReports report;
        //ScenarioContext _scenarioContext;
        public static IWebDriver WebDriver { get; private set; }

        [Before]
        public void SetUp()
        {           
            var browser = WebDriverFactory.Config["Browser"];
            WebDriver = WebDriverFactory.GetWebDriver(browser);
            WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(10);
            Console.WriteLine("This line has been executed in the Before block");
        }

        [After]
        public void CleanUp()
        {
            WebDriver.Close();
            Console.WriteLine("This line has been executed in the After block");
            WebDriver.Dispose();
        }



        //[AfterScenario]

        //public void ShutdownApplication()
        //{
        //    _context.ShutDownApplication();
        //}

        //[AfterStep]
        //public void StepsInTheReport()
        //{
        //    var typeOfStep = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();
        //    if (_scenarioContext == null)
        //    {
        //        if (typeOfStep.Equals("Given"))
        //            scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text);
        //        else if (typeOfStep.Equals("When"))
        //            scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text);
        //        else if (typeOfStep.Equals("Then"))
        //            scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text);
        //        else if (typeOfStep.Equals("And"))
        //            scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text);
        //    }
        //    if (_scenarioContext.TestError != null)
        //    {
        //        if (typeOfStep.Equals("Given"))
        //        {
        //            scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Fail(_scenarioContext.TestError.Message);
        //        }
        //        else if (typeOfStep.Equals("When"))
        //        {
        //            scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Fail(_scenarioContext.TestError.Message);
        //        }
        //        else if (typeOfStep.Equals("Then"))
        //        {
        //            scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Fail(_scenarioContext.TestError.Message);
        //        }
        //        else if (typeOfStep.Equals("And"))
        //        {
        //            scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text).Fail(_scenarioContext.TestError.Message);
        //        }
        //        if (_scenarioContext.ScenarioExecutionStatus.ToString().Equals("StepDefinitionPending"))
        //        {
        //            if (typeOfStep.Equals("Given"))
        //            {
        //                scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
        //            }
        //            else if (typeOfStep.Equals("When"))
        //            {
        //                scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
        //            }
        //            else if (typeOfStep.Equals("Then"))
        //            {
        //                scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
        //            }
        //            else if (typeOfStep.Equals("And"))
        //            {
        //                scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
        //            }
        //        }



        //    }
        //}
        //[AfterScenario]

        //public void CloseApplicationUnderTest()
        //{
        //    try
        //    {
        //        if (_scenarioContext.TestError != null)
        //        {
        //            string scenarioName = _scenarioContext.ScenarioInfo.Title;
        //            string directory = AppDomain.CurrentDomain.BaseDirectory + @"\Screenshots\";
        //            Console.WriteLine(directory);
        //            _context.TakeScreeenshotAtThePointOfTestFailure(directory, scenarioName);
        //        }
        //    }
        //    catch (Exception)
        //    {

        //    }
        //    finally
        //    {
        //        _context.ShutDownApplication();
        //    }
        //}



    }
}
