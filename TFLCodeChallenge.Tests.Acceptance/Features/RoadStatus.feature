Feature: RoadStatus

In order to check the Road Status by a Id


@RoadStatus
Scenario: I can check the road displayName
	Given a valid road id A2 is specified
	When the client is run
	Then the road 'displayName’ should be displayed as 'A2'

Scenario: I can check the road statusSeverity
	Given a valid road id A2 is specified
	When the client is run
	Then the road ‘statusSeverity’ should be displayed as ‘Good’

Scenario: I can check the road statusSeverityDescription
	Given a valid road id A2 is specified
	When the client is run
	Then the road ‘statusSeverityDescription’ should be displayed as ‘No Exceptional Delays’

Scenario: I can check the invalid road return an informative error
	Given an invalid road id A233 is specified
	When the client is run
	Then the application should return an informative error


