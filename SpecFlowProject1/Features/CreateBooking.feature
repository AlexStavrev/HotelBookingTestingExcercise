Feature: CreateBooking

A short summary of the feature

@tag1
Scenario: Create a booking with a start and an end date
	Given the start date is <startDate>
	And the end date is <endDate>
	When the booking is created
	Then the result <bookingId> should be returned 
