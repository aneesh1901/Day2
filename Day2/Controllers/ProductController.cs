using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Day2.Data;

namespace Day2.Controllers
{
    // 1) This tells ASP.NET Core this is an API controller
    [ApiController]

    // 2) This defines the base route: "api/products"
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _db;
        public ProductsController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _db.Products.ToListAsync();
            return Ok(products);
        }



        // GET: api/products/1
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _db.Products.FindAsync(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        // POST: api/products
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Product product)
        {
            _db.Products.Add(product);
            await _db.SaveChangesAsync();

            return Ok(product);
        }

        // PUT: api/products/2
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Product updated)
        {
            var existing = await _db.Products.FindAsync(id);

            if (existing == null)
                return NotFound();

            // Update only the fields you allow to change
            existing.Name = updated.Name;
            existing.Price = updated.Price;

            await _db.SaveChangesAsync();

            return Ok(existing);
        }

        // DELETE: api/products/2
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _db.Products.FindAsync(id);

            if (existing == null)
                return NotFound();

            _db.Products.Remove(existing);
            await _db.SaveChangesAsync();

            return NoContent();
        }





    }
}
