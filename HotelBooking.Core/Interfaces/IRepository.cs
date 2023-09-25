using System.Collections.Generic;

namespace HotelBooking.Core;

public interface IRepository<T>
{
    IEnumerable<T> GetAll();
    T Get(int id);
    int Add(T entity);
    bool Edit(T entity);
    bool Remove(int id);
}
