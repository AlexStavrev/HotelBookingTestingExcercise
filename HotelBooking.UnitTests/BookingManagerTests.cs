using System;
using System.Linq;
using HotelBooking.Core;
using HotelBooking.Infrastructure.Repositories;
using HotelBooking.UnitTests.Fakes;
using Xunit;

namespace HotelBooking.UnitTests;
public class BookingManagerTests
{
    private readonly IBookingManager _bookingManager;
    private readonly IRepository<Booking> _bookingRepository;

    public BookingManagerTests(){
        DateTime start = DateTime.Today.AddDays(10);
        DateTime end = DateTime.Today.AddDays(20);
        _bookingRepository = new FakeBookingRepository(start, end);
        IRepository<Room> roomRepository = new FakeRoomRepository();
        _bookingManager = new BookingManager(_bookingRepository, roomRepository);
    }

    [Fact]
    public void FindAvailableRoom_StartDateNotInTheFuture_ThrowsArgumentException()
    {
        // Arrange
        DateTime date = DateTime.Today;

        // Act
        Action act = () => _bookingManager.FindAvailableRoom(date, date);

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void FindAvailableRoom_RoomAvailable_RoomIdNotMinusOne()
    {
        // Arrange
        DateTime date = DateTime.Today.AddDays(1);
        // Act
        int roomId = _bookingManager.FindAvailableRoom(date, date);
        // Assert
        Assert.NotEqual(-1, roomId);
    }

    [Fact]
    public void FindAvailableRoom_RoomAvialable_ReturnsAvailableRoom()
    {
        // Arrange
        DateTime date = DateTime.Today.AddDays(1);

        // Act
        int roomId = _bookingManager.FindAvailableRoom(date, date);

        var bookingForReturnedId = _bookingRepository.GetAll().Where(
            booking =>
                booking.RoomId == roomId
            &&  booking.StartDate <= date
            &&  booking.EndDate >= date
            &&  booking.IsActive
            );

        // Assert
    }
}
