Feature: BotVision
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers



Scenario: A bot must be able to view what lies before, but not what lies behind when facing north
	Given I have an 3 x 7 arena
	And I have a first bot with "South" orientation, deployed on 1 positions from the left and 1 positions from the top
	And I have a second bot with "North" orientation, deployed on 1 positions from the left and 3 positions from the top
	And I have a third bot with "North" orientation, deployed on 1 positions from the left and 5 positions from the top
	When I investigate the vision of the second bot
	Then The first bot will be visible on 1 positions from the left and 1 positions from the top
	And The third bot will not be visible

Scenario: A bot must be able to view what lies before, but not what lies behind when facing east
	Given I have an 7 x 3 arena
	And I have a first bot with "West" orientation, deployed on 5 positions from the left and 1 positions from the top
	And I have a second bot with "East" orientation, deployed on 3 positions from the left and 1 positions from the top
	And I have a third bot with "East" orientation, deployed on 1 positions from the left and 1 positions from the top
	When I investigate the vision of the second bot
	Then The first bot will be visible on 5 positions from the left and 1 positions from the top
	And The third bot will not be visible

Scenario: A bot must be able to view what lies before, but not what lies behind when facing south
	Given I have an 3 x 7 arena
	And I have a first bot with "North" orientation, deployed on 1 positions from the left and 5 positions from the top
	And I have a second bot with "South" orientation, deployed on 1 positions from the left and 3 positions from the top
	And I have a third bot with "South" orientation, deployed on 1 positions from the left and 1 positions from the top
	When I investigate the vision of the second bot
	Then The first bot will be visible on 1 positions from the left and 5 positions from the top
	And The third bot will not be visible

Scenario: A bot must be able to view what lies before, but not what lies behind when facing west
	Given I have an 7 x 3 arena
	And I have a first bot with "East" orientation, deployed on 1 positions from the left and 1 positions from the top
	And I have a second bot with "West" orientation, deployed on 3 positions from the left and 1 positions from the top
	And I have a third bot with "West" orientation, deployed on 5 positions from the left and 1 positions from the top
	When I investigate the vision of the second bot
	Then The first bot will be visible on 1 positions from the left and 1 positions from the top
	And The third bot will not be visible