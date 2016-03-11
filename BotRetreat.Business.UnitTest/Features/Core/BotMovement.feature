Feature: BotMovement
	In order to move a bot
	I want to check if movements are done correcltly



Scenario: A bot must be able to move forward when facing north
	Given I have a bot with "North" orientation
	And That bot is deployed on a 20 x 20 arena
	And That bot is located on 5 positions from the left and 10 positions from the top
	When I make the bot move forward
	Then The orientation will still be "North"
	And The bot will be located on 5 positions from the left and 9 positions from the top

Scenario: A bot must be able to move forward when facing east
	Given I have a bot with "East" orientation
	And That bot is deployed on a 20 x 20 arena
	And That bot is located on 5 positions from the left and 10 positions from the top
	When I make the bot move forward
	Then The orientation will still be "East"
	And The bot will be located on 6 positions from the left and 10 positions from the top

Scenario: A bot must be able to move forward when facing south
	Given I have a bot with "South" orientation
	And That bot is deployed on a 20 x 20 arena
	And That bot is located on 5 positions from the left and 10 positions from the top
	When I make the bot move forward
	Then The orientation will still be "South"
	And The bot will be located on 5 positions from the left and 11 positions from the top

Scenario: A bot must be able to move forward when facing west
	Given I have a bot with "West" orientation
	And That bot is deployed on a 20 x 20 arena
	And That bot is located on 5 positions from the left and 10 positions from the top
	When I make the bot move forward
	Then The orientation will still be "West"
	And The bot will be located on 4 positions from the left and 10 positions from the top



Scenario: A bot must be blocked to move forward when facing the north arena edge
	Given I have a bot with "North" orientation
	And That bot is deployed on a 10 x 10 arena
	And That bot is located on 5 positions from the left and 0 positions from the top
	When I make the bot move forward
	Then The orientation will still be "North"
	And The bot will be located on 5 positions from the left and 0 positions from the top

Scenario: A bot must be blocked to move forward when facing the east arena edge
	Given I have a bot with "East" orientation
	And That bot is deployed on a 10 x 10 arena
	And That bot is located on 9 positions from the left and 5 positions from the top
	When I make the bot move forward
	Then The orientation will still be "East"
	And The bot will be located on 9 positions from the left and 5 positions from the top

Scenario: A bot must be blocked to move forward when facing the south arena edge
	Given I have a bot with "South" orientation
	And That bot is deployed on a 10 x 10 arena
	And That bot is located on 5 positions from the left and 9 positions from the top
	When I make the bot move forward
	Then The orientation will still be "South"
	And The bot will be located on 5 positions from the left and 9 positions from the top

Scenario: A bot must be blocked to move forward when facing the west arena edge
	Given I have a bot with "West" orientation
	And That bot is deployed on a 10 x 10 arena
	And That bot is located on 0 positions from the left and 5 positions from the top
	When I make the bot move forward
	Then The orientation will still be "West"
	And The bot will be located on 0 positions from the left and 5 positions from the top



Scenario: A bot must be able to turn left when facing north
	Given I have a bot with "North" orientation
	And That bot is deployed on a 1 x 1 arena
	When I make the bot turn left 
	Then The new orientation will be "West"

Scenario: A bot must be able to turn left when facing east
	Given I have a bot with "East" orientation
	And That bot is deployed on a 1 x 1 arena
	When I make the bot turn left 
	Then The new orientation will be "North"

Scenario: A bot must be able to turn left when facing south
	Given I have a bot with "South" orientation
	And That bot is deployed on a 1 x 1 arena
	When I make the bot turn left 
	Then The new orientation will be "East"

Scenario: A bot must be able to turn left when facing west
	Given I have a bot with "West" orientation
	And That bot is deployed on a 1 x 1 arena
	When I make the bot turn left 
	Then The new orientation will be "South"



Scenario: A bot must be able to turn right when facing north
	Given I have a bot with "North" orientation
	And That bot is deployed on a 1 x 1 arena
	When I make the bot turn right 
	Then The new orientation will be "East"

Scenario: A bot must be able to turn right when facing east
	Given I have a bot with "East" orientation
	And That bot is deployed on a 1 x 1 arena
	When I make the bot turn right 
	Then The new orientation will be "South"

Scenario: A bot must be able to turn right when facing south
	Given I have a bot with "South" orientation
	And That bot is deployed on a 1 x 1 arena
	When I make the bot turn right 
	Then The new orientation will be "West"

Scenario: A bot must be able to turn right when facing west
	Given I have a bot with "West" orientation
	And That bot is deployed on a 1 x 1 arena
	When I make the bot turn right 
	Then The new orientation will be "North"



Scenario: A bot must be able to turn around when facing north
	Given I have a bot with "North" orientation
	And That bot is deployed on a 1 x 1 arena
	When I make the bot turn around 
	Then The new orientation will be "South"
	
Scenario: A bot must be able to turn around when facing east
	Given I have a bot with "East" orientation
	And That bot is deployed on a 1 x 1 arena
	When I make the bot turn around 
	Then The new orientation will be "West"

Scenario: A bot must be able to turn around when facing south
	Given I have a bot with "South" orientation
	And That bot is deployed on a 1 x 1 arena
	When I make the bot turn around 
	Then The new orientation will be "North"

Scenario: A bot must be able to turn around when facing west
	Given I have a bot with "West" orientation
	And That bot is deployed on a 1 x 1 arena
	When I make the bot turn around 
	Then The new orientation will be "East"