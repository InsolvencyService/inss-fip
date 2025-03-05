Feature: ATU_211 Update Accessibility Statement page
	As Application Services
    I need the accessibility statement updated
    So that it reflects any changes made that impact user accessibility 


Background: 
Given I navigate to the Accessibility Statement Page

@AccessibilityStatement @Regression
Scenario: ATU_211 Verify the Accessibility statement page URL, title and page heading
Then the Accessibility statement page will be displayed and the URL, page title and H1 will be as per requirements

##This test will fail until the breadcrumbs have been added to this page
@AccessibilityStatement @Regression
Scenario: ATU_211 Verify the navigation of the Home breadcrumb on the Accessibility statement page
When I click the "Home" link on the Accessibility Statement page
Then the FIP Start page will be displayed and the URL, page title and the page heading will be as per requirements

@AccessibilityStatement @Regression
Scenario: ATU_211 Verify the navigation of the 'how to make your device easier to use' link
When I click the "how to make your device easier to use" link on the Accessibility Statement page
Then the following URL is displayed "https://mcmw.abilitynet.org.uk/"

@AccessibilityStatement @Regression
Scenario: ATU_211 Verify the navigation of the 'general enquiry form' link
When I click the "general enquiry form" link on the Accessibility Statement page
Then the following URL is displayed "https://www.insolvencydirect.bis.gov.uk/externalonlineforms/GeneralEnquiry.aspx"

@AccessibilityStatement @Regression
Scenario: ATU_211 Verify the navigation of the 'Find out about call charges' link
When I click the "Find out about call charges" link on the Accessibility Statement page
Then the following URL is displayed "https://www.gov.uk/call-charges"

@AccessibilityStatement @Regression
Scenario: ATU_211 Verify the navigation of the 'contact the Equality Advisory and Support Service (EASS)' link
When I click the "contact the Equality Advisory and Support Service (EASS)" link on the Accessibility Statement page
Then the following URL is displayed "https://www.equalityadvisoryservice.com/"

@AccessibilityStatement @Regression
Scenario: ATU_211 Verify the navigation of the 'The Public Sector Bodies (Websites and Mobile Applications) (No. 2) Accessibility Regulations 2018' link
When I click the "The Public Sector Bodies (Websites and Mobile Applications) (No. 2) Accessibility Regulations 2018" link on the Accessibility Statement page
Then the following URL is displayed "https://www.legislation.gov.uk/uksi/2018/852/contents/made"

@AccessibilityStatement @Regression
Scenario: ATU_211 Verify the navigation of the 'Web Content Accessibility Guidelines (WCAG) version 2.1' link
When I click the "Web Content Accessibility Guidelines (WCAG) version 2.1" link on the Accessibility Statement page
Then the following URL is displayed "https://www.w3.org/TR/WCAG21/"

