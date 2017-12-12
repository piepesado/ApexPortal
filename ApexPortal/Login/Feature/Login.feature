Feature: Login
	As a Agency Agent user,
	I would like to be able to login into APEX Portal

@login
Scenario: Login as a Agency Agent
	Given that I navigate to the APEX Portal Url
	And I enter jimmie.carr@travelleaders.com as the username
	And I enter the password
	And I enter 94326 as the CID
	When I click on Login button
	Then I should land on Apex hompage for Agency Agent role
