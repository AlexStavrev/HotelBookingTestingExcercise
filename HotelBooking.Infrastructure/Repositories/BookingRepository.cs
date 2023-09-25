using System.Collections.Generic;
using System.Linq;
using HotelBooking.Core;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Infrastructure.Repositories;

public class BookingRepository : IRepository<Booking>
{
    private readonly HotelBookingContext _db;

    public BookingRepository(HotelBookingContext context)
    {
        _db = context;
    }

    public int Add(Booking entity)
    {
        _db.Booking.Add(entity);
        _db.SaveChanges();
        return entity.Id;
    }

    public bool Edit(Booking entity)
    {
        _db.Entry(entity).State = EntityState.Modified;
        _db.SaveChanges();
        return true;
    }

    public Booking Get(int id)
    {
        return _db.Booking.Include(b => b.Customer).Include(b => b.Room).FirstOrDefault(b => b.Id == id);
    }

    public IEnumerable<Booking> GetAll()
    {
        return _db.Booking.Include(b => b.Customer).Include(b => b.Room).ToList();
    }

    public bool Remove(int id)
    {
        var booking = _db.Booking.FirstOrDefault(b => b.Id == id);
        _db.Booking.Remove(booking);
        _db.SaveChanges();
        return true;
    }
}
