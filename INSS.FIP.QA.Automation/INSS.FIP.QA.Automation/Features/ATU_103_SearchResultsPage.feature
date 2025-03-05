Feature: ATU_103 SearchResultsPage
	As a user
	I need the FIP search results screen updated in line with the new prototype screen [URL]
	So that I can view FIP search results using this new screen

Background: 
Given I create an Insolvency Practitioner record


@CitizenSearchResultsPage @Regression
Scenario: ATU_103 Verify the Search results page
Given I navigate to the Search results page
Then the URL, page title and page heading will be displayed for the Search results page


@CitizenSearchResultsPage @Regression
Scenario: ATU_103 Verify the breadcrumbs on the Search results page
Given I navigate to the Search results page
When I press the Home breadcrumb on the Search results page
Then the FIP Start page will be displayed and the URL, page title and the page heading will be as per requirements
Given I navigate back to the Search results page
And I click the Search breadcrumb on the Search results page
Then I am shown the Search page

@CitizenSearchResultsPage @Regression
Scenario: ATU_103 Verify the correct details are shown in the table when searching by First name
Given I navigate to the Search results by searching by first name
Then I am shown the search results in a table for the record I searched for

@CitizenSearchResultsPage @Regression
Scenario: ATU_103 Verify the correct details are shown in the table when searching by Surname
Given I navigate to the Search results by searching by Surname
Then I am shown the search results in a table for the record I searched for

@CitizenSearchResultsPage @Regression
Scenario: ATU_103 Verify the correct details are shown in the table when searching by Company Name
Given I navigate to the Search results by searching by Company Name
Then I am shown the search results in a table for the record I searched for

@CitizenSearchResultsPage @Regression
Scenario: ATU_103 Verify the correct details are shown in the table when searching by Town or City
Given I navigate to the Search results by searching by Town or city
Then I am shown the search results in a table for the record I searched for

@CitizenSearchResultsPage @Regression
Scenario: ATU_103 Verify the correct details are shown in the table when searching by Postcode
Given I navigate to the Search results by searching by Postcode
Then I am shown the search results in a table for the record I searched for

@CitizenSearchResultsPage @Regression
Scenario: ATU_103 Verify the correct details are shown in the table when searching by mutliple fields
Given I navigate to the Search results by searching by First name, Surname, Company name and Postcode
Then I am shown the search results in a table for the record I searched for