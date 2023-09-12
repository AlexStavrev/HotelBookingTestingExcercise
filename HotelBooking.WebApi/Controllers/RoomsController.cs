using HotelBooking.Core;
using Microsoft.AspNetCore.Mvc;


namespace HotelBooking.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class RoomsController : Controller
{
    private readonly IRepository<Room> _repository;

    public RoomsController(IRepository<Room> repos)
    {
        _repository = repos;
    }

    // GET: rooms
    [HttpGet(Name = "GetRooms")]
    public IEnumerable<Room> Get()
    {
        return _repository.GetAll();
    }

    // GET rooms/5
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var item = _repository.Get(id);
        if (item == null)
        {
            return NotFound();
        }
        return new ObjectResult(item);
    }

    // POST roooms
    [HttpPost]
    public IActionResult Post([FromBody] Room room)
    {
        if (room == null)
        {
            return BadRequest();
        }

        _repository.Add(room);
        return CreatedAtRoute("GetRooms", null);
    }


    // DELETE rooms/5
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (id > 0)
        {
            _repository.Remove(id);
            return NoContent();
        }
        else {
            return BadRequest();
        }
    }
}
