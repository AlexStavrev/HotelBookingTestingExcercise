
using HotelBooking.Core;
using HotelBooking.Infrastructure;
using HotelBooking.Infrastructure.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Specs.StepDefinitions
{
    [Binding]
    public class CreateBookingStepDefinitions : IDisposable
    {
        private readonly SqliteConnection _connection;
        private readonly BookingManager _bookingManager;
        private DateTime _startDate, _endDate;
        private int? _returnedId;

        public CreateBookingStepDefinitions() 
        {
            _connection = new SqliteConnection("DataSource=:memory:");

            // In-memory database only exists while the _connection is open
            _connection.Open();

            // Initialize test database
            var options = new DbContextOptionsBuilder<HotelBookingContext>()
                            .UseSqlite(_connection).Options;
            var dbContext = new HotelBookingContext(options);
            IDbInitializer dbInitializer = new DbInitializer();
            dbInitializer.Initialize(dbContext);

            // Create repositories and BookingManager
            var bookingRepos = new BookingRepository(dbContext);
            var roomRepos = new RoomRepository(dbContext);
            _bookingManager = new BookingManager(bookingRepos, roomRepos);

            roomRepos.Add(new Room() { Description = "A" });
        }
        
        [Given(@"the start date is (.*)")]
        public void GivenTheStartDateIsStartDate(string startDate)
        {
            _startDate = DateTime.Parse(startDate);
            
        }

        [Given(@"the end date is (.*)")]
        public void GivenTheEndDateIsEndDate(string endDate)
        {
            _endDate = DateTime.Parse(endDate);
        }

        [When(@"the booking is created")]
        public void WhenTheBookingIsCreated()
        {
            var newBooking = new Booking()
            {
                StartDate = _startDate,
                EndDate = _endDate,
                CustomerId = 1,
                RoomId = 1
            };

            _returnedId = _bookingManager.CreateBooking(newBooking);
        }

        [Then(@"the result (.*) should be returned")]
        public void ThenTheResultBookingIdShouldBeReturned(int bookingId)
        {
            Assert.Equal(bookingId, _returnedId);
        }

        public void Dispose()
        {
            _connection.Close();
        }
    }
}
