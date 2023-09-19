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

    public BookingManagerTests() {

        DateTime start = DateTime.Today.AddDays(10);
        DateTime end = DateTime.Today.AddDays(20);

        var bookings = new List<Booking>
        { 
            new Booking { Id = 1, StartDate = DateTime.Today.AddDays(1), EndDate = DateTime.Today.AddDays(1), IsActive = true, CustomerId = 1, RoomId = 1 },
            new Booking { Id = 2, StartDate = start, EndDate = end, IsActive = true, CustomerId = 1, RoomId = 1 },
            new Booking { Id = 3, StartDate = start, EndDate = end, IsActive = true, CustomerId = 2, RoomId = 2 },
            new Booking { Id = 4, StartDate = start, EndDate = end, IsActive = false, CustomerId = 1, RoomId = 3 },
        };
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
        _fakeBookingRepository.Setup(x => x.Edit(It.IsAny<Booking>())).Callback((Booking newBooking) =>
            {
                int index = bookings.FindIndex(s => s.Id == newBooking.Id);
                if (index != -1)
                {
                    bookings[index] = newBooking;
                }
            }
        );

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
            &&  booking.StartDate <= date
            &&  booking.EndDate >= date
            &&  booking.IsActive
            );

        // Assert
        Assert.Empty(bookingForReturnedRoomId);
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

        var bookingCustomerId = _fakeBookingRepository.Object.Get(bookingId).CustomerId;

        //Assert
        Assert.Equal(customerId, bookingCustomerId);
    }

    [Fact]
    public void CancelCreatedReservation_ReservationIsNotActive_ShouldCancelReservationWhenItIsNotActive()
    {
        // Arrage
        // Act
        _bookingManager.CancelCreatedReservation(4);

        // Assert
        _fakeBookingRepository.Verify(bookingRepo => bookingRepo.Remove(It.IsAny<int>()), Times.Exactly(1));
    }
    
    [Fact]
    public void CannotCancelCreatedReservation_ReservationIsActive_ShouldNotCancelReservationWhenItIsActive()
    {
        // Arrage
        // Act
        _bookingManager.CancelCreatedReservation(2);
        Action act = () => _fakeBookingRepository.Verify(bookingRepo => bookingRepo.Remove(It.IsAny<int>()), Times.Exactly(1));

        // Assert
        Assert.Throws<MockException>(act);
    }

    [Fact]
    public void EditReservation_ReservationIsNotActive_ShouldEditReservationWhenItIsNotActive()
    {
        // Arrage
        var newBooking = new Booking { Id = 2, StartDate = DateTime.Today.AddDays(10), EndDate = DateTime.Today.AddDays(20), IsActive = false, CustomerId = 2, RoomId = 3 };

        // Act
        _bookingManager.ChangeReservation(newBooking);

        // Assert
        _fakeBookingRepository.Verify(bookingRepo => bookingRepo.Edit(It.IsAny<Booking>()), Times.Exactly(1));
    }

    [Fact]
    public void EditReservation_ReservationIsActive_ShouldNotEditReservationWhenItIsActive()
    {
        // Arrage
        var newBooking = new Booking { Id = 4, StartDate = DateTime.Today.AddDays(10), EndDate = DateTime.Today.AddDays(20), IsActive = true, CustomerId = 1, RoomId = 3 };

        // Act
        _bookingManager.ChangeReservation(newBooking);
        Action act = () => _fakeBookingRepository.Verify(bookingRepo => bookingRepo.Edit(It.IsAny<Booking>()), Times.Exactly(1));

        // Assert
        Assert.Throws<MockException>(act);
    }
}
