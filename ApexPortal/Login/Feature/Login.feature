Feature: Login
	As a Agency Agent user,
	I would like to be able to login into APEX Portal

@login
Scenario Outline: Login as a Agency Agent By Example
	Given that I navigate to the APEX Portal Url
	And I have entered <userame> as username
	And I have entered the <password> as password
	And I have entered <cid> as the CID number
	When I click on Login button
	Then I should land on Apex hompage for Agency Agent role
	
	Examples:
	| username                      | password | cid   |
	| jimmie.carr@travelleaders.com | zaq1ZAQ! | 94326 |
