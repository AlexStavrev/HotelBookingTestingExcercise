using HotelBooking.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Mvc.Controllers;
public class RoomsController : Controller
{
    private readonly IRepository<Room> _repository;

    public RoomsController(IRepository<Room> repos)
    {
        _repository = repos;
    }

    // GET: Rooms
    public IActionResult Index()
    {
        return View(_repository.GetAll().ToList());
    }

    // GET: Rooms/Details/5
    public IActionResult Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Room room = _repository.Get(id.Value);
        if (room == null)
        {
            return NotFound();
        }

        return View(room);
    }

    // GET: Rooms/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Rooms/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("Id,Description")] Room room)
    {
        if (ModelState.IsValid)
        {
            _repository.Add(room);
            return RedirectToAction(nameof(Index));
        }
        return View(room);
    }

    // GET: Rooms/Edit/5
    public IActionResult Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Room room = _repository.Get(id.Value);
        if (room == null)
        {
            return NotFound();
        }
        return View(room);
    }

    // POST: Rooms/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("Id,Description")] Room room)
    {
        if (id != room.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _repository.Edit(room);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_repository.Get(room.Id) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(room);
    }

    // GET: Rooms/Delete/5
    public IActionResult Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Room room = _repository.Get(id.Value);
        if (room == null)
        {
            return NotFound();
        }

        return View(room);
    }

    // POST: Rooms/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        if (id > 0)
            _repository.Remove(id);
        return RedirectToAction(nameof(Index));
    }

}
