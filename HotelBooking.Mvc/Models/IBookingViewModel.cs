﻿using HotelBooking.Core;

namespace HotelBooking.Mvc.Models;

public interface IBookingViewModel
{
    IEnumerable<Booking> Bookings { get; }
    List<DateTime> FullyOccupiedDates { get; }
    int YearToDisplay { get; set; }
    string GetMonthName(int month);
    bool DateIsOccupied(int year, int month, int day);
    bool DateHasAnyReservation(int year, int month, int day);
}
