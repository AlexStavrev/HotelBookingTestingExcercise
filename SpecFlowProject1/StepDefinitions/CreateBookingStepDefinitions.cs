
using HotelBooking.Core;
using HotelBooking.Infrastructure;
using HotelBooking.Infrastructure.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace HotelBooking.Specs.StepDefinitions
{
    [Binding]
    public class CreateBookingStepDefinitions : IDisposable
    {
        private readonly SqliteConnection _connection;
        private readonly BookingManager _bookingManager;
        private DateTime _startDate, _endDate;
        private bool _bookingCreated;

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
            dbInitializer.Initialize(dbContext, false);

            // Create repositories and BookingManager
            var bookingRepos = new BookingRepository(dbContext);
            var roomRepos = new RoomRepository(dbContext);
            var customerRepos = new CustomerRepository(dbContext);
            _bookingManager = new BookingManager(bookingRepos, roomRepos);

            var customerId = customerRepos.Add(new Customer() { Name = "Test Customer", Email = "test@email.com" });

            var roomIdA = roomRepos.Add(new Room() { Description = "A" });
            var roomIdB = roomRepos.Add(new Room() { Description = "B" });

            bookingRepos.Add(new Booking()
            {
                StartDate = new DateTime(2024, 10, 10),
                EndDate = new DateTime(2024, 10, 20),
                IsActive = true,
                CustomerId = customerId,
                RoomId = roomIdA
            });

            bookingRepos.Add(new Booking()
            {
                StartDate = new DateTime(2024, 10, 10),
                EndDate = new DateTime(2024, 10, 20),
                IsActive = true,
                CustomerId = customerId,
                RoomId = roomIdB
            });
        }
        
        [Given(@"the start date is (.*)")]
        public void GivenTheStartDateIsStartDate(string startDate)
        {
            _startDate = DateTime.ParseExact(startDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            
        }

        [Given(@"the end date is (.*)")]
        public void GivenTheEndDateIsEndDate(string endDate)
        {
            _endDate = DateTime.ParseExact(endDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
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

            try
            {
                var returnedId = _bookingManager.CreateBooking(newBooking);

                _bookingCreated = returnedId == -1 ? false : true;
            }
            catch(ArgumentException)
            {
                _bookingCreated = false;
            }
            
        }

        [Then(@"the result (.*) should be returned")]
        public void ThenTheResultBookingIdShouldBeReturned(bool bookingCreated)
        {
            Assert.Equal(bookingCreated, _bookingCreated);
        }

        public void Dispose()
        {
            _connection.Close();
        }
    }
}
