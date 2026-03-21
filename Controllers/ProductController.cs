using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProductInventoryAPI.Data;
using ProductInventoryAPI.Models;
namespace ProductInventoryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProductController(AppDbContext context)
        {
            _context = context;
        }
    
        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _context.Products.OrderBy(p=> p.Id).ToList();
            return Ok(products);
        }

        [HttpGet("{id}")]

        public IActionResult GetProductById(int id)
        {
            try
            {
                
                if (id<=0)
                    return BadRequest("ID must be positive value");
                var product = _context.Products.Find(id);
                if (product == null)
                    return NotFound($"Product with ID {id} not found.");
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occured:" + ex.Message);
            }
        }
        [HttpPost]
        public IActionResult AddProduct([FromBody] Product newProduct)
        {
            if(newProduct == null)
                return BadRequest("Product data is required.");

            if(string.IsNullOrEmpty(newProduct.Name))
                return BadRequest("Product name is required.");

            if(newProduct.Price <=0)
                return BadRequest("Price must be greater than zero.");

            if(newProduct.StockQuantity < 0)
                return BadRequest("Stock quantity cannot be negative.");

            _context.Products.Add(newProduct);
            _context.SaveChanges();
            return Ok(newProduct);
            }



        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] Product updateProduct)
        {
            var product = _context.Products.Find(id);
            if(product == null)
                return NotFound("Product not found.");
                
            product.Name=updateProduct.Name;
            product.Category=updateProduct.Category;
            product.Price=updateProduct.Price;
            product.StockQuantity=updateProduct.StockQuantity;

            _context.SaveChanges();
            return Ok(product);

        }
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);
            if(product==null)
                return NotFound("Product not found.");
            _context.Products.Remove(product);
            _context.SaveChanges();
            return Ok("Product deleted successfully.");
        }
            
        [HttpGet("category/{category}")]
        public IActionResult GetByCategory(string category)
        {
            var product = _context.Products.Where(p => p.Category == category).ToList();
            if(product.Count == 0)
                return NotFound($"No products found in category");
            return Ok(product);
        }

        [HttpGet("sorted")]
        public IActionResult GetSortedProducts(string order = "asc")
        {
            var product = order.ToLower() == "desc"
                ? _context.Products.OrderByDescending(p => p.Price).ToList()
                : _context.Products.OrderBy(p => p.Price).ToList();
            return Ok(product);
        }

    }
    
}