using HotelBooking.Core;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class BookingsController : Controller
{
    private readonly IRepository<Booking> _bookingRepository;
    private readonly IRepository<Customer> _customerRepository;
    private readonly IRepository<Room> _roomRepository;
    private readonly IBookingManager _bookingManager;

    public BookingsController(IRepository<Booking> bookingRepos, IRepository<Room> roomRepos,
        IRepository<Customer> customerRepos, IBookingManager manager)
    {
        _bookingRepository = bookingRepos;
        _roomRepository = roomRepos;
        _customerRepository = customerRepos;
        _bookingManager = manager;
    }

    // GET: bookings
    [HttpGet(Name = "GetBookings")]
    public IEnumerable<Booking> Get()
    {
        return _bookingRepository.GetAll();
    }

    // GET bookings/5
    [HttpGet("{id}", Name = "GetBooking")]
    public IActionResult Get(int id)
    {
        var item = _bookingRepository.Get(id);
        if (item == null)
        {
            return NotFound();
        }
        return new ObjectResult(item);
    }

    // POST bookings
    [HttpPost]
    public IActionResult Post([FromBody]Booking booking)
    {
        if (booking == null)
        {
            return BadRequest();
        }

        int? createdId = _bookingManager.CreateBooking(booking);

        if (createdId != -1)
        {
            return CreatedAtRoute("GetBookings", null);
        }
        else
        {
            return Conflict("The booking could not be created. All rooms are occupied. Please try another period.");
        }

    }

    // PUT bookings/5
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody]Booking booking)
    {
        if (booking == null || booking.Id != id)
        {
            return BadRequest();
        }

        var modifiedBooking = _bookingRepository.Get(id);

        if (modifiedBooking == null)
        {
            return NotFound();
        }

        // This implementation will only modify the booking's state and customer.
        // It is not safe to directly modify StartDate, EndDate and Room, because
        // it could conflict with other active bookings.
        modifiedBooking.IsActive = booking.IsActive;
        modifiedBooking.CustomerId = booking.CustomerId;

        _bookingRepository.Edit(modifiedBooking);
        return NoContent();
    }

    // DELETE bookings/5
    [HttpDelete("reservation/{id}")]
    public IActionResult DeleteCreatedReservation(int id)
    {
        if (_bookingRepository.Get(id) == null)
        {
            return NotFound();
        }

        if (_bookingManager.CancelCreatedReservation(id))
        {
            return NoContent();
        }
        return BadRequest();
    }
    
    [HttpDelete("{id}")]
    public IActionResult DeleteCompletedBooking(int id)
    {
        if (_bookingRepository.Get(id) == null)
        {
            return NotFound();
        }

        if (_bookingManager.RemoveCompletedBooking(id))
        {
            
            return NoContent();
        }
        return BadRequest();
    }
}
