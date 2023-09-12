﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelBooking.Core;

public class BookingManager : IBookingManager
{
    private readonly IRepository<Booking> _bookingRepository;
    private readonly IRepository<Room> _roomRepository;

    // Constructor injection
    public BookingManager(IRepository<Booking> bookingRepository, IRepository<Room> roomRepository)
    {
        this._bookingRepository = bookingRepository;
        this._roomRepository = roomRepository;
    }

    public bool CreateBooking(Booking booking)
    {
        int roomId = FindAvailableRoom(booking.StartDate, booking.EndDate);

        if (roomId >= 0)
        {
            booking.RoomId = roomId;
            booking.IsActive = true;
            _bookingRepository.Add(booking);
            return true;
        }
        else
        {
            return false;
        }
    }

    public int FindAvailableRoom(DateTime startDate, DateTime endDate)
    {
        if (startDate <= DateTime.Today || startDate > endDate)
            throw new ArgumentException("The start date cannot be in the past or later than the end date.");

        var activeBookings = _bookingRepository.GetAll().Where(b => b.IsActive);
        foreach (var room in _roomRepository.GetAll())
        {
            var activeBookingsForCurrentRoom = activeBookings.Where(b => b.RoomId == room.Id);
            if (activeBookingsForCurrentRoom.All(b => startDate < b.StartDate &&
                endDate < b.StartDate || startDate > b.EndDate && endDate > b.EndDate))
            {
                return room.Id;
            }
        }
        return -1;
    }

    public List<DateTime> GetFullyOccupiedDates(DateTime startDate, DateTime endDate)
    {
        if (startDate > endDate)
            throw new ArgumentException("The start date cannot be later than the end date.");

        List<DateTime> fullyOccupiedDates = new List<DateTime>();
        int noOfRooms = _roomRepository.GetAll().Count();
        var bookings = _bookingRepository.GetAll();

        if (bookings.Any())
        {
            for (DateTime d = startDate; d <= endDate; d = d.AddDays(1))
            {
                var noOfBookings = from b in bookings
                                   where b.IsActive && d >= b.StartDate && d <= b.EndDate
                                   select b;
                if (noOfBookings.Count() >= noOfRooms)
                    fullyOccupiedDates.Add(d);
            }
        }
        return fullyOccupiedDates;
    }

    public List<DateTime> GetPartiallyOccupiedDates(DateTime startDate, DateTime endDate)
    {
        if (startDate > endDate)
            throw new ArgumentException("The start date cannot be later than the end date.");

        var partiallyOccupiedDates = new List<DateTime>();
        var bookings = _bookingRepository.GetAll();

        if (bookings.Any())
        {
            for (var d = startDate; d <= endDate; d = d.AddDays(1))
            {
                var noOfBookings = from b in bookings
                                   where b.IsActive && d >= b.StartDate && d <= b.EndDate
                                   select b;
                if (noOfBookings.Any())
                    partiallyOccupiedDates.Add(d);
            }
        }
        return partiallyOccupiedDates;
    }
}
