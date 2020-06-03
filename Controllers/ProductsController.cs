using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using InstrumentsShopAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace InstrumentsShopAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private DatabaseContext _context;

        public ProductsController(DatabaseContext context) 
        {
            _context = context;
        }

        [HttpGet] [Authorize]
        public IEnumerable<Product> Get()
        {
            var products = _context.Products.ToList();
            return products;
        }

        [HttpGet("{id}")] [Authorize]
        public string Get(int id) 
        {
            return _context.Products.Find(id).Name;
        }

        [HttpPost] [Authorize]
        public void Post([FromBody] string value) 
        {
            var product = new Product();
            product.Name = value;
            product.Price = 1000;

            _context.Products.Add(product);
            _context.SaveChanges();
        }

        [HttpDelete("{id}")] [Authorize]
        public void Delete(int id) 
        {
            var product = _context.Products.Find(id);

            _context.Products.Remove(product);
            _context.SaveChanges();
        }
    }
}