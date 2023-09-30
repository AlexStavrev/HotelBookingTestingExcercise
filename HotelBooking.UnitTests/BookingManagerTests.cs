using System;
using System.Collections.Generic;
using System.Linq;
using HotelBooking.Core;
using Moq;
using Xunit;

namespace HotelBooking.UnitTests;
public class BookingManagerTests
{
    private readonly IBookingManager _bookingManager;
    private readonly Mock<IRepository<Booking>> _fakeBookingRepository;
    private int _idCounter;

    public BookingManagerTests() {

        DateTime start = DateTime.Today.AddDays(10);
        DateTime end = DateTime.Today.AddDays(20);

        var bookings = new List<Booking>
        {
            new Booking { Id = 1, StartDate = DateTime.Today.AddDays(1), EndDate = DateTime.Today.AddDays(1), IsActive = true, CustomerId = 1, RoomId = 1 },
            new Booking { Id = 2, StartDate = start, EndDate = end, IsActive = true, CustomerId = 1, RoomId = 1 },
            new Booking { Id = 3, StartDate = start, EndDate = end, IsActive = true, CustomerId = 2, RoomId = 2 },
            new Booking { Id = 4, StartDate = start, EndDate = end, IsActive = false, CustomerId = 1, RoomId = 3 },
            new Booking { Id = 5, StartDate = DateTime.Today.AddDays(-5), EndDate = DateTime.Today.AddDays(-1), IsActive = false, CustomerId = 1, RoomId = 1 },
            new Booking { Id = 6, StartDate = start, EndDate = end, IsActive = true, CustomerId = 2, RoomId = 3 },

        };
        _idCounter = 5;

        var rooms = new List<Room>
        {
            new Room { Id=1, Description="A" },
            new Room { Id=2, Description="B" },
            new Room { Id=3, Description="C" },
        };

        _fakeBookingRepository = new Mock<IRepository<Booking>>();
        _fakeBookingRepository.Setup(x => x.GetAll()).Returns(bookings);
        _fakeBookingRepository.Setup(x => x.Get(It.IsAny<int>())).Returns((int bookingId) =>
        {
            return bookings.Where(booking => booking.Id == bookingId).First();
        });
        _fakeBookingRepository.Setup(x => x.Add(It.IsAny<Booking>())).Callback((Booking booking) =>
            bookings.Add(booking)
        );
        _fakeBookingRepository.Setup(x => x.Edit(It.IsAny<Booking>())).Returns(true);
        _fakeBookingRepository.Setup(x => x.Remove(It.IsAny<int>())).Returns(true);
        var fakeRoomRepository = new Mock<IRepository<Room>>();
        fakeRoomRepository.Setup(x => x.GetAll()).Returns(rooms);

        _bookingManager = new BookingManager(_fakeBookingRepository.Object, fakeRoomRepository.Object);
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

        var bookingForReturnedRoomId = _fakeBookingRepository.Object.GetAll().Where(
            booking =>
                booking.RoomId == roomId
            && booking.StartDate <= date
            && booking.EndDate >= date
            && booking.IsActive
            );

        // Assert
        Assert.Empty(bookingForReturnedRoomId);
    }

    public static IEnumerable<object[]> GetDates()
    {
        yield return new object[] { DateTime.Today.AddDays(9), DateTime.Today.AddDays(11) };
        yield return new object[] { DateTime.Today.AddDays(12), DateTime.Today.AddDays(14) };
        yield return new object[] { DateTime.Today.AddDays(19), DateTime.Today.AddDays(21) };
        yield return new object[] { DateTime.Today.AddDays(5), DateTime.Today.AddDays(25) };
    }

    [Theory]
    [MemberData(nameof(GetDates))]
    public void ReserveRoom_ShouldReturnFalse_WhenPartOfThePeriodIsFullyReserved(DateTime startDate, DateTime endDate)
    {
        //Arrange
        var newBooking = new Booking()
        {
            StartDate = startDate,
            EndDate = endDate,
            CustomerId = 1,
        };
        //Act
        int? bookingId = _bookingManager.CreateBooking(newBooking);

        //Assert
        Assert.Equal(-1, bookingId);
    }

    [Fact]
    public void ReserveRoomWithStartAndEndDate_DateIsNotInThePast_ReturnsTrue()
    {
        //Arrange
        DateTime dateStart = DateTime.Today.AddDays(1);
        DateTime dateEnd = DateTime.Today.AddDays(3);

        //Act

        var newBooking = new Booking() 
        {
        StartDate = dateStart,
        EndDate = dateEnd,
        CustomerId = 1,
        };

        var bookingId = _bookingManager.CreateBooking(newBooking);

        //Assert
        Assert.NotEqual(-1, bookingId);
    }

    [Fact]
    public void ReserveRoom_HasTheSameOwner_ReturnsTrue()
    {
        //Arrange
        DateTime dateStart = DateTime.Today.AddDays(1);
        DateTime dateEnd = DateTime.Today.AddDays(3);
        int customerId = 1;
        //Act

        var newBooking = new Booking()
        {
            StartDate = dateStart,
            EndDate = dateEnd,
            CustomerId = customerId,
        };

        var bookingId = _bookingManager.CreateBooking(newBooking);

        var bookingCustomerId = _fakeBookingRepository.Object.Get(bookingId.Value).CustomerId;

        //Assert
        Assert.Equal(customerId, bookingCustomerId);
    }

    [Fact]
    public void CancelCreatedReservation_ReservationIsNotActive_ShouldCancelReservationWhenItIsNotActive()
    {
        // Arrage
        int validBookingId = 4;

        // Act
        var result = _bookingManager.CancelCreatedReservation(validBookingId);

        // Assert
        Assert.True(result);
    }
    
    [Fact]
    public void CannotCancelCreatedReservation_ReservationIsActive_ShouldNotCancelReservationWhenItIsActive()
    {
        // Arrage
        int activeBookingId = 2;

        // Act
        var result = _bookingManager.CancelCreatedReservation(activeBookingId);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void CancelCreatedReservation_CannotCancelAlreadyExpiredReservation()
    {
        // Arrage
        int alreadyExpiredReservation = 5;

        // Act
        var result = _bookingManager.CancelCreatedReservation(alreadyExpiredReservation);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void DeleteCompletedReservation_ReservationIsComplete_ShouldDeleteReservationWhenItIsComplete()
    {
        // Arrage
        int validReservationId = 5;

        // Act
        var result = _bookingManager.RemoveCompletedBooking(validReservationId);

        // Assert
        Assert.True(result);
    }    
    
    [Fact]
    public void CannotDeleteIncompleteReservation_ReservationIsNotComplete_ShouldNotDeleteReservationWhenItIsNotComplete()
    {
        // Arrage
        int incompleteBookingId = 2;

        // Act
        var result = _bookingManager.RemoveCompletedBooking(incompleteBookingId);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void RemoveCompletedReservation_CannotDeleteReservationThatHasNotStarted()
    {
        // Arrage
        int futureReservation = 1;

        // Act
        var result = _bookingManager.RemoveCompletedBooking(futureReservation);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void EditReservation_ReservationIsNotActive_ShouldEditReservationWhenItIsNotActive()
    {
        // Arrage
        var newBooking = new Booking { Id = 2, StartDate = DateTime.Today.AddDays(10), EndDate = DateTime.Today.AddDays(20), IsActive = false, CustomerId = 2, RoomId = 3 };

        // Act
        bool result = _bookingManager.ChangeReservation(newBooking);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void EditReservation_ReservationIsActive_ShouldNotEditReservationWhenItIsActive()
    {
        // Arrage
        var newBooking = new Booking { Id = 4, StartDate = DateTime.Today.AddDays(10), EndDate = DateTime.Today.AddDays(20), IsActive = true, CustomerId = 1, RoomId = 3 };

        // Act
        bool result = _bookingManager.ChangeReservation(newBooking);

        // Assert
        Assert.False(result);
    }
}
