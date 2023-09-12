using HotelBooking.Core;
using Microsoft.AspNetCore.Mvc;


namespace HotelBooking.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomersController : Controller
{
    private readonly IRepository<Customer> _repository;

    public CustomersController(IRepository<Customer> repos)
    {
        _repository = repos;
    }

    // GET: customers
    [HttpGet]
    public IEnumerable<Customer> Get()
    {
        return _repository.GetAll();
    }

}
