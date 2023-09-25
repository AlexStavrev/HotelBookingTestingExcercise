using System;
using System.Collections.Generic;

namespace HotelBooking.Core;

public interface IBookingManager
{
    int CreateBooking(Booking booking);
    int FindAvailableRoom(DateTime startDate, DateTime endDate);
    List<DateTime> GetFullyOccupiedDates(DateTime startDate, DateTime endDate);
    List<DateTime> GetPartiallyOccupiedDates(DateTime startDate, DateTime endDate);
    void CancelCreatedReservation(int bookingId);
    void RemoveCompletedReservation(int bookingId);
    void ChangeReservation(Booking newBooking);
}
