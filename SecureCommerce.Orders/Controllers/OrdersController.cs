
using Microsoft.AspNetCore.Mvc;

namespace SecureCommerce.Orders.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private static readonly object[] Orders =
    {
        new { Id = 1, Product = "Mechanical Keyboard", Qty = 2 },
        new { Id = 2, Product = "USB Cable", Qty = 5 }
    };

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        return Ok(Orders);
    }
}
