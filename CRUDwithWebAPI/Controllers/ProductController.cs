using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CRUDwithWebAPI.Data;
using CRUDwithWebAPI.Models;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace CRUDwithWebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly MyappDbContext _context;

        public ProductController(MyappDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var products = _context.Products.ToList();
                if (products.Count == 0)
                {

                    return NotFound("Products not avialable");
                }
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet("{id}")]

        public IActionResult Get(int id)
        {
            try
            {

                 var product = _context.Products.Find(id);

                if (product == null)
                {
                    return NotFound($"Product details not found with id {id}");
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]

        public IActionResult Post(Product model)
        {
            try
            {
                _context.Add(model);
                _context.SaveChanges();
                return Ok("Product Created.");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Put(Product model)
        {

            if (model == null || model.Id == 0)
            {
                if (model == null)
                {
                    return BadRequest("model data is invalid");

                }
                else if (model.Id == 0)
                {
                    return BadRequest($"Product Id {model.Id} is invalid");
                }
            }
            try
            {

                var product = _context.Products.Find(model.Id);
                    if(product == null)
                    {
                        return BadRequest($"Product data not found with {model.Id}");

                    }

                    product.ProductName = model.ProductName;
                    product.Price = model.Price;
                    product.Quantity    = model.Quantity;
                    _context.SaveChanges();
                    return Ok("Product details updated");
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")] 
        public IActionResult Delete(int id)
        {
            try
            {
                var product = _context.Products.Find(id);
                if(product == null)
                {
                    return BadRequest("Product not found");
                }

                _context.Products.Remove(product);
                _context.SaveChanges();
                return Ok("Product details deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
    }
}
