using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsPortal.Data;
using NewsPortal.Entity;

namespace NewsPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {

        private readonly DataContext _context;

        public TestController(DataContext context)
        {
            _context = context;
        }

        [HttpPost("add_name")]
        public async Task<IActionResult> AddName([FromBody] Test testModel)
        {
            // You can either create a separate TestEntity or use an existing table temporarily
            Console.WriteLine($"Received name: {testModel.Name}");

            _context.Tests.Add(testModel);
            await _context.SaveChangesAsync();

            return Ok(testModel);
        }
    }
}
