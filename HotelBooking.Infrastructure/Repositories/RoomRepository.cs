using System;
using System.Collections.Generic;
using System.Linq;
using HotelBooking.Core;

namespace HotelBooking.Infrastructure.Repositories;
public class RoomRepository : IRepository<Room>
{
    private readonly HotelBookingContext _db;

    public RoomRepository(HotelBookingContext context)
    {
        _db = context;
    }

    public void Add(Room entity)
    {
        _db.Room.Add(entity);
        _db.SaveChanges();
    }

    public void Edit(Room entity)
    {
        throw new NotImplementedException();
    }

    public Room Get(int id)
    {
        // The FirstOrDefault method below returns null
        // if there is no room with the specified Id.
        return _db.Room.FirstOrDefault(r => r.Id == id);
    }

    public IEnumerable<Room> GetAll()
    {
        return _db.Room.ToList();
    }

    public void Remove(int id)
    {
        // The Single method below throws an InvalidOperationException
        // if there is not exactly one room with the specified Id.
        var room = _db.Room.Single(r => r.Id == id);
        _db.Room.Remove(room);
        _db.SaveChanges();
    }
}
