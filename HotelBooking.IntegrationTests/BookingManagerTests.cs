using System;
using HotelBooking.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace HotelBooking.IntegrationTests
{
    public class BookingManagerTests : IDisposable
    {
        SqliteConnection connection;

        public BookingManagerTests(){
            connection = new SqliteConnection("DataSource=:memory:");

            // In-memory database only exists while the connection is open
            connection.Open();

            // Initialize test database
            var options = new DbContextOptionsBuilder<HotelBookingContext>()
                            .UseSqlite(connection).Options;
            var dbContext = new HotelBookingContext(options);
            DbInitializer.Initialize(dbContext);
        }

        public void Dispose()
        {
            // This will delete the in-memory database
            connection.Close();
        }

        [Fact]
        public void Test1()
        {
            Assert.Equal(1, 1);
        }
    }
}
