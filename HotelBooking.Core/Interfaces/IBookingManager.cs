using System;
using System.Collections.Generic;

namespace HotelBooking.Core;

public interface IBookingManager
{
    int CreateBooking(Booking booking);
    int FindAvailableRoom(DateTime startDate, DateTime endDate);
    List<DateTime> GetFullyOccupiedDates(DateTime startDate, DateTime endDate);
    List<DateTime> GetPartiallyOccupiedDates(DateTime startDate, DateTime endDate);
    bool CancelCreatedReservation(int bookingId);
    bool RemoveCompletedBooking(int bookingId);
    bool ChangeReservation(Booking newBooking);
}
