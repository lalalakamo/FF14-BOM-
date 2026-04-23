using FF14BOM.Dtos;
using FF14BOM.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FF14BOM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly WebContext _webContext;

        public ItemsController(WebContext webContext)
        {
            _webContext = webContext;
        }

        // GET: api/<ItemsController>
        [HttpGet]
        public IEnumerable<ItemGetDto> Get()
        {
            var result = _webContext.Item
                .Select(i => new ItemGetDto
                {
                    Mtr_Name = i.Mtr_Name,
                    Mtr_id = i.Mtr_id
                }).ToList();
            return result;
        }

        // GET api/<ItemsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ItemsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ItemsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ItemsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
