using System;
using TechTalk.SpecFlow;

namespace HotelBooking.Specs.StepDefinitions
{
    [Binding]
    public class CreateBookingStepDefinitions
    {
        [Given(@"the start date is <startDate>")]
        public void GivenTheStartDateIsStartDate()
        {
            throw new PendingStepException();
        }

        [Given(@"the end date is <endDate>")]
        public void GivenTheEndDateIsEndDate()
        {
            throw new PendingStepException();
        }

        [When(@"the booking is created")]
        public void WhenTheBookingIsCreated()
        {
            throw new PendingStepException();
        }

        [Then(@"the result <bookingId> should be returned")]
        public void ThenTheResultBookingIdShouldBeReturned()
        {
            throw new PendingStepException();
        }
    }
}
