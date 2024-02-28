Feature: ATU_214 FIP search screen
	As a user
	I need to be able to search the FIP DB
	So that I can enter the required information to search the FIP DB and find an IP 


Background: 
Given I navigate to the FIP search Page

@SearchPage @Regression
Scenario: ATU_214 Verify the FIP search page URL, title and page heading
Then the FIP search page will be displayed and the URL, page title and the page heading will be as per requirements

@SearchPage @Regression
Scenario: ATU_214 Verify the FIP search page error messages
When I press the Search button without entering any search values
Then I am shown the single error message "Enter either first name, last name, company, town or city, or postcode"
##When I enter valid values but I enter an invalid First name
##And I press the Search button 
##Then I am shown the following error message at the top of the page "No valid search values have been entered"
##When I enter valid values but I enter an invalid Last name
##And I press the Search button 
##Then I am shown the following error message
##When I enter valid values but I enter an invalid Company name
##And I press the Search button 
##Then I am shown the following error message
##When I enter valid values but I enter an invalid Town name
##And I press the Search button 
##Then I am shown the following error message
When I enter valid values but I enter an invalid Postcode
And I press the Search button 
Then I am shown the following error message "Enter a real postcode"

@SearchPage @Regression
Scenario: ATU_214 Verify the FIP search page breadcrumb navigation
When I press the Home breadcrumb on the FIP Search page
Then the FIP Start page will be displayed and the URL, page title and the page heading will be as per requirements

@SearchPage @Regression
Scenario: ATU_214 Verify the FIP search page says 'No matches found' when the search returns no records
When I search for values which return no results
Then the search page will show a statement stating 'No matches found' 
