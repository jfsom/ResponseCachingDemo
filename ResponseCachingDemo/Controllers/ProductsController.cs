using Microsoft.AspNetCore.Mvc;
using ResponseCachingDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace ResponseCachingDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        //// GET: api/Products
        //// Caching All Products
        //// The response will be cached for 60 seconds.
        //// All requests to this endpoint will share the same cached response.
        //[ResponseCache(Duration = 60)]
        //[HttpGet]
        //public async Task<ActionResult<List<Product>>> GetProducts()
        //{
        //    // Retrieve all products from the database
        //    var products = await _context.Products.ToListAsync();

        //    // To show you the performance differences we delay the response for 3 seconds
        //    await Task.Delay(TimeSpan.FromSeconds(3));

        //    return Ok(products);
        //}

        // GET: api/Products
        // Caching All Products
        [ResponseCache(CacheProfileName = "Default60")]
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            // Retrieve all products from the database
            var products = await _context.Products.ToListAsync();
            return Ok(products);
        }


        // GET: api/Products/5
        // Caching Single Product by ID
        // The response is cached for 120 seconds
        // Different cache entries for different id values.
        [ResponseCache(Duration = 120, VaryByQueryKeys = new[] { "id" })]
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            // Retrieve a single product by ID
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            // To show you the performance differences we delay the response for 3 seconds
            await Task.Delay(TimeSpan.FromSeconds(3));

            return Ok(product);
        }

        // GET: api/Products/vary-query?sort=price
        // GET: api/Products/vary-query?sort=name
        // Caches different responses for 120 Seconds based on the value of the "sort" query parameter
        [ResponseCache(Duration = 120, VaryByQueryKeys = new[] { "sort" })]
        [HttpGet("vary-query")]
        public async Task<ActionResult<List<Product>>> GetProductsVaryByQuery([FromQuery] string sort)
        {
            var products = await _context.Products.ToListAsync();

            // Sort products based on the query parameter
            if (!string.IsNullOrEmpty(sort))
            {
                if (sort.Equals("price", StringComparison.OrdinalIgnoreCase))
                {
                    products = products.OrderBy(p => p.Price).ToList();
                }
                else if (sort.Equals("name", StringComparison.OrdinalIgnoreCase))
                {
                    products = products.OrderBy(p => p.Name).ToList();
                }
            }

            await Task.Delay(TimeSpan.FromSeconds(3));
            return Ok(products);
        }

        // GET: api/Products/anylocation
        // Caching with Location = Any
        // The response can be cached by both client and proxy servers.
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
        [HttpGet("anylocation")]
        public async Task<ActionResult<List<Product>>> GetProductsAnyLocation()
        {
            var products = await _context.Products.ToListAsync();
            await Task.Delay(TimeSpan.FromSeconds(3));
            return Ok(products);
        }

        // GET: api/Products/clientlocation
        // Caching with Location = Client
        // The response is cached only on the client.
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]
        [HttpGet("clientlocation")]
        public async Task<ActionResult<List<Product>>> GetProductsClientLocation()
        {
            var products = await _context.Products.ToListAsync();
            await Task.Delay(TimeSpan.FromSeconds(3));
            return Ok(products);
        }

        // GET: api/Products/nolocation
        // Caching with Location = None
        // The response will not cached by any cache.
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.None)]
        [HttpGet("nolocation")]
        public async Task<ActionResult<List<Product>>> GetProductsNoLocation()
        {
            var products = await _context.Products.ToListAsync();
            await Task.Delay(TimeSpan.FromSeconds(3));
            return Ok(products);
        }

        // GET: api/Products/nostore
        // Caching with NoStore = true
        // The response will not be cached in any cache.
        [ResponseCache(NoStore = true)]
        [HttpGet("nostore")]
        public async Task<ActionResult<List<Product>>> GetProductsNoStore()
        {
            var products = await _context.Products.ToListAsync();
            await Task.Delay(TimeSpan.FromSeconds(3));
            return Ok(products);
        }

        // GET: api/Products/varybyheader
        // Caching with VaryByHeader = "User-Agent"
        // The response cache will vary based on the User-Agent header.
        [ResponseCache(Duration = 60, VaryByHeader = "User-Agent")]
        [HttpGet("varybyheader")]
        public async Task<ActionResult<List<Product>>> GetProductsVaryByHeader()
        {
            var products = await _context.Products.ToListAsync();
            await Task.Delay(TimeSpan.FromSeconds(3));
            return Ok(products);
        }
    }
}