using System;
using HotelBooking.Core;
using HotelBooking.Infrastructure;
using HotelBooking.Infrastructure.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace HotelBooking.IntegrationTests;

public class BookingManagerTests : IDisposable
{
    // This test class uses a separate Sqlite in-memory database. While the
    // .NET Core built-in in-memory database is not a relational database,
    // Sqlite in-memory database is. This means that an exception is thrown,
    // if a database constraint is violated, and this is a desirable behavior
    // when testing.

    private readonly SqliteConnection _connection;
    private readonly BookingManager _bookingManager;

    public BookingManagerTests()
    {
        _connection = new SqliteConnection("DataSource=:memory:");

        // In-memory database only exists while the _connection is open
        _connection.Open();

        // Initialize test database
        var options = new DbContextOptionsBuilder<HotelBookingContext>()
                        .UseSqlite(_connection).Options;
        var dbContext = new HotelBookingContext(options);
        IDbInitializer dbInitializer = new DbInitializer();
        dbInitializer.Initialize(dbContext, true);

        // Create repositories and BookingManager
        var bookingRepos = new BookingRepository(dbContext);
        var roomRepos = new RoomRepository(dbContext);
        _bookingManager = new BookingManager(bookingRepos, roomRepos);
    }

    public void Dispose()
    {
        // This will delete the in-memory database
        _connection.Close();
    }

    [Fact]
    public void FindAvailableRoom_Should_ReturnMinusOne_When_ThereAreNoAvailableRooms()
    {
        // Act
        var roomId = _bookingManager.FindAvailableRoom(DateTime.Today.AddDays(8), DateTime.Today.AddDays(8));
        // Assert
        Assert.Equal(-1, roomId);
    }
}
