Feature: BotColision
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers



Scenario: A bot must not be able to move to the north when coliding with a south facing bot
	Given I have an 3 x 4 arena
	And I have a first bot with "South" orientation, deployed on 1 positions from the left and 1 positions from the top
	And I have a second bot with "North" orientation, deployed on 1 positions from the left and 2 positions from the top
	When I make the second bot move forward
	Then The second bot will remain at 1 positions from the left and 2 positions from the top
	And The second bot will remain with a "North" orientation

Scenario: A bot must not be able to move to the east when coliding with a west facing bot
	Given I have an 4 x 3 arena
	And I have a first bot with "West" orientation, deployed on 2 positions from the left and 1 positions from the top
	And I have a second bot with "East" orientation, deployed on 1 positions from the left and 1 positions from the top
	When I make the second bot move forward
	Then The second bot will remain at 1 positions from the left and 1 positions from the top
	And The second bot will remain with a "East" orientation

Scenario: A bot must not be able to move to the south when coliding with a north facing bot
	Given I have an 3 x 4 arena
	And I have a first bot with "South" orientation, deployed on 1 positions from the left and 1 positions from the top
	And I have a second bot with "North" orientation, deployed on 1 positions from the left and 2 positions from the top
	When I make the first bot move forward
	Then The first bot will remain at 1 positions from the left and 1 positions from the top
	And The first bot will remain with a "South" orientation

Scenario: A bot must not be able to move to the west when coliding with a east facing bot
	Given I have an 4 x 3 arena
	And I have a first bot with "West" orientation, deployed on 2 positions from the left and 1 positions from the top
	And I have a second bot with "East" orientation, deployed on 1 positions from the left and 1 positions from the top
	When I make the first bot move forward
	Then The first bot will remain at 2 positions from the left and 1 positions from the top
	And The first bot will remain with a "West" orientation