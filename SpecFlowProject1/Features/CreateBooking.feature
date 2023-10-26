Feature: CreateBooking

A short summary of the feature

@tag1
Scenario: Create a booking with a start and an end date
	Given the start date is <startDate>
	And the end date is <endDate>
	When the booking is created
	Then the result <bookingCreated> should be returned


Examples:
	| startDate  | endDate    | bookingCreated |
	| 2024-10-03 | 2024-10-05 | true           | #Testcase number: 1
	| 2024-10-03 | 2024-10-21 | false          | #Testcase number: 2
	| 2024-10-03 | 2024-10-10 | false          | #Testcase number: 3
	| 2024-10-03 | 2024-10-20 | false          | #Testcase number: 4
	| 2024-10-03 | 2024-10-02 | false          | #Testcase number: 5
	| 2024-10-21 | 2024-10-25 | true           | #Testcase number: 6
	| 2024-10-23 | 2024-10-21 | false          | #Testcase number: 7
	| 2024-10-10 | 2024-10-03 | false          | #Testcase number: 8
	| 2024-10-10 | 2024-10-21 | false          | #Testcase number: 9
	| 2024-10-10 | 2024-10-20 | false          | #Testcase number: 10
	| 2024-10-20 | 2024-10-18 | false          | #Testcase number: 11
	| 2024-10-20 | 2024-10-21 | false          | #Testcase number: 12