﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (https://www.specflow.org/).
//      SpecFlow Version:3.9.0.0
//      SpecFlow Generator Version:3.9.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace INSS.FIP.QA.Automation.Features
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("ATU_211 Update Accessibility Statement page")]
    public partial class ATU_211UpdateAccessibilityStatementPageFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
        private string[] _featureTags = ((string[])(null));
        
#line 1 "ATU_211_AccessibilityStatementPage.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Features", "ATU_211 Update Accessibility Statement page", "\tAs Application Services\r\n    I need the accessibility statement updated\r\n    So " +
                    "that it reflects any changes made that impact user accessibility ", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.OneTimeTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<NUnit.Framework.TestContext>(NUnit.Framework.TestContext.CurrentContext);
        }
        
        public virtual void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        public virtual void FeatureBackground()
        {
#line 7
#line hidden
#line 8
testRunner.Given("I navigate to the Accessibility Statement Page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("ATU_211 Verify the Accessibility statement page URL, title and page heading")]
        [NUnit.Framework.CategoryAttribute("AccessibilityStatement")]
        [NUnit.Framework.CategoryAttribute("Regression")]
        public virtual void ATU_211VerifyTheAccessibilityStatementPageURLTitleAndPageHeading()
        {
            string[] tagsOfScenario = new string[] {
                    "AccessibilityStatement",
                    "Regression"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("ATU_211 Verify the Accessibility statement page URL, title and page heading", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 11
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 7
this.FeatureBackground();
#line hidden
#line 12
testRunner.Then("the Accessibility statement page will be displayed and the URL, page title and H1" +
                        " will be as per requirements", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("ATU_211 Verify the navigation of the Home breadcrumb on the Accessibility stateme" +
            "nt page")]
        [NUnit.Framework.CategoryAttribute("AccessibilityStatement")]
        [NUnit.Framework.CategoryAttribute("Regression")]
        public virtual void ATU_211VerifyTheNavigationOfTheHomeBreadcrumbOnTheAccessibilityStatementPage()
        {
            string[] tagsOfScenario = new string[] {
                    "AccessibilityStatement",
                    "Regression"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("ATU_211 Verify the navigation of the Home breadcrumb on the Accessibility stateme" +
                    "nt page", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 16
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 7
this.FeatureBackground();
#line hidden
#line 17
testRunner.When("I click the \"Home\" link on the Accessibility Statement page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 18
testRunner.Then("the FIP Start page will be displayed and the URL, page title and the page heading" +
                        " will be as per requirements", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("ATU_211 Verify the navigation of the \'how to make your device easier to use\' link" +
            "")]
        [NUnit.Framework.CategoryAttribute("AccessibilityStatement")]
        [NUnit.Framework.CategoryAttribute("Regression")]
        public virtual void ATU_211VerifyTheNavigationOfTheHowToMakeYourDeviceEasierToUseLink()
        {
            string[] tagsOfScenario = new string[] {
                    "AccessibilityStatement",
                    "Regression"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("ATU_211 Verify the navigation of the \'how to make your device easier to use\' link" +
                    "", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 21
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 7
this.FeatureBackground();
#line hidden
#line 22
testRunner.When("I click the \"how to make your device easier to use\" link on the Accessibility Sta" +
                        "tement page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 23
testRunner.Then("the following URL is displayed \"https://mcmw.abilitynet.org.uk/\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("ATU_211 Verify the navigation of the \'general enquiry form\' link")]
        [NUnit.Framework.CategoryAttribute("AccessibilityStatement")]
        [NUnit.Framework.CategoryAttribute("Regression")]
        public virtual void ATU_211VerifyTheNavigationOfTheGeneralEnquiryFormLink()
        {
            string[] tagsOfScenario = new string[] {
                    "AccessibilityStatement",
                    "Regression"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("ATU_211 Verify the navigation of the \'general enquiry form\' link", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 26
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 7
this.FeatureBackground();
#line hidden
#line 27
testRunner.When("I click the \"general enquiry form\" link on the Accessibility Statement page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 28
testRunner.Then("the following URL is displayed \"https://www.insolvencydirect.bis.gov.uk/externalo" +
                        "nlineforms/GeneralEnquiry.aspx\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("ATU_211 Verify the navigation of the \'Find out about call charges\' link")]
        [NUnit.Framework.CategoryAttribute("AccessibilityStatement")]
        [NUnit.Framework.CategoryAttribute("Regression")]
        public virtual void ATU_211VerifyTheNavigationOfTheFindOutAboutCallChargesLink()
        {
            string[] tagsOfScenario = new string[] {
                    "AccessibilityStatement",
                    "Regression"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("ATU_211 Verify the navigation of the \'Find out about call charges\' link", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 31
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 7
this.FeatureBackground();
#line hidden
#line 32
testRunner.When("I click the \"Find out about call charges\" link on the Accessibility Statement pag" +
                        "e", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 33
testRunner.Then("the following URL is displayed \"https://www.gov.uk/call-charges\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("ATU_211 Verify the navigation of the \'contact the Equality Advisory and Support S" +
            "ervice (EASS)\' link")]
        [NUnit.Framework.CategoryAttribute("AccessibilityStatement")]
        [NUnit.Framework.CategoryAttribute("Regression")]
        public virtual void ATU_211VerifyTheNavigationOfTheContactTheEqualityAdvisoryAndSupportServiceEASSLink()
        {
            string[] tagsOfScenario = new string[] {
                    "AccessibilityStatement",
                    "Regression"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("ATU_211 Verify the navigation of the \'contact the Equality Advisory and Support S" +
                    "ervice (EASS)\' link", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 36
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 7
this.FeatureBackground();
#line hidden
#line 37
testRunner.When("I click the \"contact the Equality Advisory and Support Service (EASS)\" link on th" +
                        "e Accessibility Statement page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 38
testRunner.Then("the following URL is displayed \"https://www.equalityadvisoryservice.com/\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("ATU_211 Verify the navigation of the \'The Public Sector Bodies (Websites and Mobi" +
            "le Applications) (No. 2) Accessibility Regulations 2018\' link")]
        [NUnit.Framework.CategoryAttribute("AccessibilityStatement")]
        [NUnit.Framework.CategoryAttribute("Regression")]
        public virtual void ATU_211VerifyTheNavigationOfTheThePublicSectorBodiesWebsitesAndMobileApplicationsNo_2AccessibilityRegulations2018Link()
        {
            string[] tagsOfScenario = new string[] {
                    "AccessibilityStatement",
                    "Regression"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("ATU_211 Verify the navigation of the \'The Public Sector Bodies (Websites and Mobi" +
                    "le Applications) (No. 2) Accessibility Regulations 2018\' link", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 41
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 7
this.FeatureBackground();
#line hidden
#line 42
testRunner.When("I click the \"The Public Sector Bodies (Websites and Mobile Applications) (No. 2) " +
                        "Accessibility Regulations 2018\" link on the Accessibility Statement page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 43
testRunner.Then("the following URL is displayed \"https://www.legislation.gov.uk/uksi/2018/852/cont" +
                        "ents/made\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("ATU_211 Verify the navigation of the \'Web Content Accessibility Guidelines (WCAG)" +
            " version 2.1\' link")]
        [NUnit.Framework.CategoryAttribute("AccessibilityStatement")]
        [NUnit.Framework.CategoryAttribute("Regression")]
        public virtual void ATU_211VerifyTheNavigationOfTheWebContentAccessibilityGuidelinesWCAGVersion2_1Link()
        {
            string[] tagsOfScenario = new string[] {
                    "AccessibilityStatement",
                    "Regression"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("ATU_211 Verify the navigation of the \'Web Content Accessibility Guidelines (WCAG)" +
                    " version 2.1\' link", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 46
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 7
this.FeatureBackground();
#line hidden
#line 47
testRunner.When("I click the \"Web Content Accessibility Guidelines (WCAG) version 2.1\" link on the" +
                        " Accessibility Statement page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 48
testRunner.Then("the following URL is displayed \"https://www.w3.org/TR/WCAG21/\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
