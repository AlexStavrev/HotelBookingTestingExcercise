using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using HotelBooking.Models;

namespace HotelBooking.Data.Repositories
{
    public class BookingRepository : IRepository<Booking>
    {
        private readonly HotelBookingContext db;

        public BookingRepository(HotelBookingContext context)
        {
            db = context;
        }

        public void Add(Booking entity)
        {
            var newBooking = db.Booking.Add(entity);
            db.SaveChanges();
        }

        public void Edit(Booking entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
        }

        public Booking Get(int id)
        {
            return db.Booking.Include(b => b.Customer).Include(b => b.Room).FirstOrDefault(b => b.Id == id);
        }

        public IEnumerable<Booking> GetAll()
        {
            return db.Booking.Include(b => b.Customer).Include(b => b.Room).ToList();
        }

        public void Remove(int id)
        {
            var booking = db.Booking.FirstOrDefault(b => b.Id == id);
            db.Booking.Remove(booking);
            db.SaveChanges();
        }

    }
}
