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
    [HttpGet(Name = "GetCustomers")]
    public IEnumerable<Customer> Get()
    {
        return _repository.GetAll();
    }

    [HttpPost]
    public IActionResult Post([FromBody] Customer customer) 
    { 
        if(customer == null)
        {
            return BadRequest();
        }

        _repository.Add(customer);

        return CreatedAtRoute("GetCustomers", null);
    }

}
