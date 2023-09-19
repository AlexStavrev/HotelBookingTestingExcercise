using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelBooking.Core;
using HotelBooking.Mvc.Models;

namespace HotelBooking.Mvc.Controllers;
public class BookingsController : Controller
{
    private readonly IRepository<Booking> _bookingRepository;
    private readonly IRepository<Customer> _customerRepository;
    private readonly IRepository<Room> _roomRepository;
    private readonly IBookingManager _bookingManager;
    private readonly IBookingViewModel _bookingViewModel;

    public BookingsController(IRepository<Booking> bookingRepos, IRepository<Room> roomRepos,
        IRepository<Customer> customerRepos, IBookingManager manager, IBookingViewModel viewModel)
    {
        _bookingRepository = bookingRepos;
        _roomRepository = roomRepos;
        _customerRepository = customerRepos;
        _bookingManager = manager;
        _bookingViewModel = viewModel;
    }

    // GET: Bookings
    public IActionResult Index(int? id)
    {
        _bookingViewModel.YearToDisplay = (id == null) ? DateTime.Today.Year : id.Value;
        return View(_bookingViewModel);
    }

    // GET: Bookings/Details/5
    public IActionResult Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Booking booking = _bookingRepository.Get(id.Value);
        if (booking == null)
        {
            return NotFound();
        }

        return View(booking);
    }

    // GET: Bookings/Create
    public IActionResult Create()
    {
        ViewData["CustomerId"] = new SelectList(_customerRepository.GetAll(), "Id", "Name");
        return View();
    }

    // POST: Bookings/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("StartDate,EndDate,CustomerId")] Booking booking)
    {
        if (ModelState.IsValid)
        {
            int createdId = _bookingManager.CreateBooking(booking);

            if (createdId != -1)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        ViewData["CustomerId"] = new SelectList(_customerRepository.GetAll(), "Id", "Name", booking.CustomerId);
        ViewBag.Status = "The booking could not be created. There were no available room.";
        return View(booking);
    }

    // GET: Bookings/Edit/5
    public IActionResult Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Booking booking = _bookingRepository.Get(id.Value);
        if (booking == null)
        {
            return NotFound();
        }
        ViewData["CustomerId"] = new SelectList(_customerRepository.GetAll(), "Id", "Name", booking.CustomerId);
        ViewData["RoomId"] = new SelectList(_roomRepository.GetAll(), "Id", "Description", booking.RoomId);
        return View(booking);
    }

    // POST: Bookings/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("StartDate,EndDate,IsActive,CustomerId,RoomId")] Booking booking)
    {
        if (id != booking.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _bookingRepository.Edit(booking);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_bookingRepository.Get(booking.Id) == null)
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
        ViewData["CustomerId"] = new SelectList(_customerRepository.GetAll(), "Id", "Name", booking.CustomerId);
        ViewData["RoomId"] = new SelectList(_roomRepository.GetAll(), "Id", "Description", booking.RoomId);
        return View(booking);
    }

    // GET: Bookings/Delete/5
    public IActionResult Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Booking booking = _bookingRepository.Get(id.Value);
        if (booking == null)
        {
            return NotFound();
        }

        return View(booking);
    }

    // POST: Bookings/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        _bookingRepository.Remove(id);
        return RedirectToAction(nameof(Index));
    }
}
