using HotelBooking.Core;

namespace HotelBooking.Mvc.Models;

public class BookingViewModel : IBookingViewModel
{
    private readonly IRepository<Booking> _bookingRepository;
    private readonly IBookingManager _bookingManager;
    private int _yearToDisplay;

    public BookingViewModel(IRepository<Booking> repository, IBookingManager manager)
    {
        _bookingRepository = repository;
        _bookingManager = manager;
    }

    public IEnumerable<Booking> Bookings
    {
        get
        {
            return _bookingRepository.GetAll();
        }
    }

    public int YearToDisplay
    {
        get
        {
            int minBookingYear = MinBookingDate.Year;
            int maxBookingYear = MaxBookingDate.Year;
            if (_yearToDisplay < minBookingYear)
                return minBookingYear;
            else if (_yearToDisplay > maxBookingYear)
                return maxBookingYear;
            else
                return _yearToDisplay;
        }
        set { _yearToDisplay = value; }
    }

    public string GetMonthName(int month)
    {
        return System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month);
    }

    public bool DateIsOccupied(int year, int month, int day)
    {
        bool occupied = false;
        if (day <= DateTime.DaysInMonth(year, month))
        {
            DateTime dt = new DateTime(year, month, day);
            DateTime occupiedDate = FullyOccupiedDates.FirstOrDefault(d => d == dt);
            if (occupiedDate > DateTime.MinValue)
                occupied = true;
        }
        return occupied;
    }

    public List<DateTime> FullyOccupiedDates
    {
        get
        {
            return _bookingManager.GetFullyOccupiedDates(MinBookingDate, MaxBookingDate);
        }
    }

    private DateTime MinBookingDate
    {
        get
        {
            var bookingStartDates = Bookings.Select(b => b.StartDate);
            return bookingStartDates.Any() ? bookingStartDates.Min() : DateTime.MinValue;
        }
    }

    private DateTime MaxBookingDate
    {
        get
        {
            var bookingEndDates = Bookings.Select(b => b.EndDate);
            return bookingEndDates.Any() ? bookingEndDates.Max() : DateTime.MaxValue;
        }
    }
}
