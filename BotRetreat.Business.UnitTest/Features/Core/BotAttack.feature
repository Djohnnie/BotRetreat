Feature: BotAttack
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers


Scenario: A bot must be able to harm its neighbours by self destructing
	Given I have a bot with "North" orientation
	And That bot is deployed on a 3 x 3 arena
	And That bot is located on 1 positions from the left and 1 positions from the top
	And A bot with physical health of 100 is deployed on 0 positions from the left and 0 positions from the top
	And A bot with physical health of 100 is deployed on 1 positions from the left and 0 positions from the top
	And A bot with physical health of 100 is deployed on 2 positions from the left and 0 positions from the top
	And A bot with physical health of 100 is deployed on 0 positions from the left and 1 positions from the top
	And A bot with physical health of 100 is deployed on 2 positions from the left and 1 positions from the top
	And A bot with physical health of 100 is deployed on 0 positions from the left and 2 positions from the top
	And A bot with physical health of 100 is deployed on 1 positions from the left and 2 positions from the top
	And A bot with physical health of 100 is deployed on 2 positions from the left and 2 positions from the top
	When I make the bot self destruct
	Then The self destructing bot will be dead
	And The bot located on 0 positions from the left and 0 positions from the top will have a physical health of 90
	And The bot located on 1 positions from the left and 0 positions from the top will have a physical health of 90
	And The bot located on 2 positions from the left and 0 positions from the top will have a physical health of 90
	And The bot located on 0 positions from the left and 1 positions from the top will have a physical health of 90
	And The bot located on 2 positions from the left and 1 positions from the top will have a physical health of 90
	And The bot located on 0 positions from the left and 2 positions from the top will have a physical health of 90
	And The bot located on 1 positions from the left and 2 positions from the top will have a physical health of 90
	And The bot located on 2 positions from the left and 2 positions from the top will have a physical health of 90