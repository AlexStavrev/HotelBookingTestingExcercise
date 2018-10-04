using System;
using HotelBooking.BusinessLogic;
using HotelBooking.Data;
using HotelBooking.Data.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace HotelBooking.IntegrationTests
{
    public class BookingManagerTests : IDisposable
    {
        // This test class uses a separate Sqlite in-memory database. While the
        // .NET Core built-in in-memory database is not a relational database,
        // Sqllite in-memory database is. This means that an exception is thrown,
        // if a database constraint is violated, and this is a desirable behavior
        // when testing.

        SqliteConnection connection;
        BookingManager bookingManager;

        public BookingManagerTests(){
            connection = new SqliteConnection("DataSource=:memory:");

            // In-memory database only exists while the connection is open
            connection.Open();

            // Initialize test database
            var options = new DbContextOptionsBuilder<HotelBookingContext>()
                            .UseSqlite(connection).Options;
            var dbContext = new HotelBookingContext(options);
            DbInitializer.Initialize(dbContext);

            // Create repositories and BookingManager
            var bookingRepos = new BookingRepository(dbContext);
            var roomRepos = new RoomRepository(dbContext);
            bookingManager = new BookingManager(bookingRepos, roomRepos);
        }

        public void Dispose()
        {
            // This will delete the in-memory database
            connection.Close();
        }

        [Fact]
        public void Test1()
        {
            // Act
            var roomId = bookingManager.FindAvailableRoom(DateTime.Today.AddDays(8), DateTime.Today.AddDays(8));
            // Assert
            Assert.Equal(-1, roomId);
        }
    }
}
