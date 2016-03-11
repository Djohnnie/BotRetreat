Feature: BotScript
	In order to make sure a bot cannot alter its parameters
	I want to be told the result of changing them



Scenario: X position member should not be allowed to change
	Given I have a bot with all health 100
	And A bot script "_xPosition = 999;"
	When The bot script runs
	Then The X position member will not have changed
	
Scenario: X position property should not be allowed to change
	Given I have a bot with all health 100
	And A bot script "XPosition = 999;"
	When The bot script runs
	Then The X position property will not have changed



Scenario: Y position member should not be allowed to change
	Given I have a bot with all health 100
	And A bot script "_yPosition = 999;"
	When The bot script runs
	Then The Y position member will not have changed
	
Scenario: Y position property should not be allowed to change
	Given I have a bot with all health 100
	And A bot script "YPosition = 999;"
	When The bot script runs
	Then The Y position property will not have changed