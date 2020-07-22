using Integration.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Integration.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        [Route("{input}")]
        public async Task<ActionResult<string>> GetAsync(string input)
        {
            if (string.IsNullOrEmpty(input))
                return BadRequest();

            var orderResponse = await _orderService.ProcessOrder(input);

            if (orderResponse == null)
                return NotFound();

            if (string.IsNullOrEmpty(orderResponse.ErrorMessage))
                return Ok(orderResponse.Output);
            else
                return BadRequest(orderResponse.ErrorMessage);


        }
    }
}