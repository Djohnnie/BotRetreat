Feature: HistoryMapper
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers



Scenario: Map history entity to history datatransfer object
	Given I have a history entity
	And The Id property is set to 'ACDC0BF0-E8B8-4955-913A-1B40A789C5E7'
	And The Name property is set to 'HistoryItem'
	And The Description property is set to 'HistoryDescription'
	And The Type property is set to 'Message'
	And The DateTime property is set to '25-05-1985'
	When I map the entity to a datatransfer object
	Then the result should be a valid history datatransfer object
	And The Id property should be 'ACDC0BF0-E8B8-4955-913A-1B40A789C5E7'
	And The Name property should be 'HistoryItem'
	And The Description property should be 'HistoryDescription'
	And The Type property should be 'Message'
	And The DateTime property should be '25-05-1985'