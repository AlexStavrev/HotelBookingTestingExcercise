using System;
using System.Collections.Generic;
using System.Linq;
using HotelBooking.Core;

namespace HotelBooking.Infrastructure.Repositories;

public class CustomerRepository : IRepository<Customer>
{
    private readonly HotelBookingContext _db;

    public CustomerRepository(HotelBookingContext context)
    {
        _db = context;
    }

    public int Add(Customer entity)
    {
        throw new NotImplementedException();
    }

    public void Edit(Customer entity)
    {
        throw new NotImplementedException();
    }

    public Customer Get(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Customer> GetAll()
    {
        return _db.Customer.ToList();
    }

    public void Remove(int id)
    {
        throw new NotImplementedException();
    }
}
