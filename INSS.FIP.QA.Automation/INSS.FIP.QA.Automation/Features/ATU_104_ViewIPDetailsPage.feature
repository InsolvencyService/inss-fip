Feature: ATU_104 View IP details screen
	As a user
	I need the IP details screen updated in line with the new prototype screen [URL]
	So that I can view each IP details record using this new screen

Background: 	
Given I create an Insolvency Practitioner record
And I navigate to the Insolvency Practitioner details page


@IPDetailsScreen @Regression
Scenario: ATU_104 Verify the Search results page and Find another IP button
Then the URL, page title and page heading will be displayed for the IP Details page
When I click the FInd another Insolvency Practitioner button
Then I am shown the Search page


@IPDetailsScreen @Regression
Scenario: ATU_104 Verify the IP details screen breadcrumbs
Then the breadcrumb text will be as expected on the IP Details Screen
Given I click the Home breadcrumb on the IP Details Screen
Then the FIP Start page will be displayed and the URL, page title and the page heading will be as per requirements
Given I navigate back to the Insolvency Practitioner details page
And I click the Search results breadcrumb on the IP Details Screen
Then the Search results page is displayed
Given I navigate back to the Insolvency Practitioner details page
And I click the Search breadcrumb on the IP Details Screen
Then I am shown the Search page

@IPDetailsScreen @Regression
Scenario: ATU_104 Verify the details for the Insolvency Practitioner on the Insolvency Practitioner Details page
Then the details for the Insolvency Practitioner are displayed



