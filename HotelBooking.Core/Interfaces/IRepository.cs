using System.Collections.Generic;

namespace HotelBooking.Core;

public interface IRepository<T>
{
    IEnumerable<T> GetAll();
    T Get(int id);
    int Add(T entity);
    void Edit(T entity);
    void Remove(int id);
}
