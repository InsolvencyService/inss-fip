Feature: ATU_250 View FIP start screen
	As a user
	I need the FIP start screen updated in line with the new prototype screen 
	So that I can access the updated FIP service using this new screen


Background: 
Given I navigate to the FIP Start Page

@StartPage @Regression
Scenario: ATU_250 Verify the FIP start page URL, title and page heading
Then the FIP Start page will be displayed and the URL, page title and the page heading will be as per requirements

@StartPage @Regression
Scenario: ATU_250 Verify the 'Start search' button takes the user to the Search screen
When I click the 'Start search' button
Then I am taken to the Search page

@StartPage @Regression
Scenario: ATU_250 Verify the navigation of the Terms and conditions footer link on the FIP start page
When I click the "Terms and conditions - footer" link
Then the following URL is displayed "https://app-uksouth-sit-fip-frontend.azurewebsites.net/home/terms-and-conditions"

@StartPage @Regression
Scenario: ATU_250 Verify the navigation of the Privacy footer link on the FIP start page
When I click the "Privacy - footer" link
Then the following URL is displayed "https://app-uksouth-sit-fip-frontend.azurewebsites.net/home/privacy-policy"

@StartPage @Regression
Scenario: ATU_250 Verify the navigation of the Accessibility Statement footer link on the FIP start page
When I click the "Accessibility statement - footer" link
Then the following URL is displayed "https://app-uksouth-sit-fip-frontend.azurewebsites.net/home/accessibility-statement"


@StartPage @Regression
Scenario: ATU_250 Verify the (Get help from the Insolvency Service) link in the Related content section on the FIP start page
When I click the "Related content - Get help from the Insolvency Service" link
Then the following URL is displayed "https://www.gov.uk/get-help-insolvency-service"

@StartPage @Regression
Scenario: ATU_250 Verify the (Find out more about bankruptcy and insolvency) link in the Related content section on the FIP start page
When I click the "Related content - Find out more about bankruptcy and insolvency" link
Then the following URL is displayed "https://www.gov.uk/browse/tax/court-claims-debt-bankruptcy"

@StartPage @Regression
Scenario: ATU_250 Verify the (Give feedback about the Individual Insolvency Register) link in the Related content section on the FIP start page
When I click the "Related content - Give feedback about the Individual Insolvency Register" link
Then the following URL is displayed "https://www.insolvencydirect.bis.gov.uk/ExternalOnlineForms/GeneralEnquiry.aspx"


