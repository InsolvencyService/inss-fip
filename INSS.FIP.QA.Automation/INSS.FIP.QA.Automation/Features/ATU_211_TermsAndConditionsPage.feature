Feature: ATU-211 Update Terms and Conditions Page

	As a user
	I need the terms and conditions updated (currently located here: www.insolvencydirect.bis.gov.uk/eiir/disclaimer.asp)
	So that they reflect any changes recently made to the service, and also appear with a styling consistent to the rest of the service


Background: 
Given I navigate to the Terms and conditions page

@TermsAndConditions @Regression
Scenario: ATU-211 Verify the Terms and conditions page
	Then the Terms and conditions page is displayed

##This will fail until the breadcrumbs are added to the page
@TermsAndConditions @Regression
Scenario: ATU-211 Verify the breadcrumb navigation on the T&C page
	When I click the Home breadcrumb on the T&C page
	Then the FIP Start page will be displayed and the URL, page title and the page heading will be as per requirements

@TermsAndConditions @Regression
Scenario: ATU-211 Verify the navigation of the tell the Insolvency Service link
	When I click the tell the Insolvency Service link on the T&C page
	Then I am navigated to the General Enquiry page
