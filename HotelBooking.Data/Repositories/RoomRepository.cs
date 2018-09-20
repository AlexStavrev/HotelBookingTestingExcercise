using System;
using System.Collections.Generic;
using System.Linq;
using HotelBooking.Models;

namespace HotelBooking.Data.Repositories
{
    public class RoomRepository : IRepository<Room>
    {
        private readonly HotelBookingContext db;

        public RoomRepository(HotelBookingContext context)
        {
            db = context;
        }

        public void Add(Room entity)
        {
            throw new NotImplementedException();
        }

        public void Edit(Room entity)
        {
            throw new NotImplementedException();
        }

        public Room Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Room> GetAll()
        {
            return db.Room.ToList();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
