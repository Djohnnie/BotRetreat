Feature: ArenaMapper
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers



Scenario: Map arena entity to arena datatransfer object
	Given I have a arena entity
	And The Id property is set to 'E414DA28-5B14-4AAE-8392-4D718673ADA3'
	And The Name property is set to 'ArenaName'
	And The Active property is set to 'true'
	And The Width property is set to '25'
	And The Height property is set to '50'
	And The Private property is set to 'true'
	And The DeploymentRestriction property is set to '10'
	When I map the entity to a datatransfer object
	Then the result should be a valid arena datatransfer object
	And The Id property should be 'E414DA28-5B14-4AAE-8392-4D718673ADA3'
	And The Name property should be 'ArenaName'
	And The Active property should be 'true'
	And The Width property should be '25'
	And The Height property should be '50'
	And The Private property should be 'true'
	And The DeploymentRestriction property should be '10'