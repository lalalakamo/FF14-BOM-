using FF14BOM.Dtos;
using FF14BOM.Models;
using FF14BOM.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FF14BOM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly WebContext _webContext;
        private readonly ProductService _productService;

        public ProductController(WebContext webContext,ProductService productService)
        {
            _webContext = webContext;
            _productService = productService;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public IActionResult Get(string? L,string? P)
        {
            return Ok(_productService.GetProducts(L,P));
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var result = _productService.GetProduct(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        // POST api/<ProductController>
        [HttpPost]
        public IActionResult Post([FromBody] List<ProductAddDto> ProductDtoList)
        {
            if (ProductDtoList == null) return BadRequest();
            return Ok(_productService.PostProduct(ProductDtoList));
        }

        //Delete api/<ProductController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            return Ok(_productService.DeleteProduct(id));
        }
    }
}
