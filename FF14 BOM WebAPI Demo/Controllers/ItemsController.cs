using FF14BOM.Dtos;
using FF14BOM.Models;
using FF14BOM.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.Identity.Client.Extensibility;
using Microsoft.JSInterop.Infrastructure;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FF14BOM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly WebContext _webContext;
        private readonly ItemService _itemService;

        public ItemsController(WebContext webContext,ItemService itemService)
        {
            _webContext = webContext;
            _itemService = itemService;
        }

        // GET: api/<ItemsController>
        [HttpGet]
        public IActionResult Get(string? T, string? L)
        {
            var result = _itemService.GetItems(T, L);
            if (!result.Any())
                return NotFound("找不到資料");
            return Ok(result);
        }

        // GET api/<ItemsController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var result = _itemService.GetItem(id);
            if (result == null)
                return NotFound($"找不到id={id}的資料");
            return Ok(result);
        }

        // POST api/<ItemsController>
        [HttpPost]
        public IActionResult Post([FromBody] List<ItemAddDto> itemDtosList)
        {
            if (itemDtosList == null) return BadRequest();
            return Ok(_itemService.PostItem(itemDtosList));
        }
        // DELETE api/<ItemsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            return Ok(_itemService.DeleteItem(id));
        }

        [HttpGet("main")]
        public IActionResult GetMain()
        {
            var result = _webContext.Item
                .GroupBy(i => i.Mtr_Level)
                .Select(g => new ItemGetMain
                {
                    Mtr_Level = g.Key,
                    ItemLists = g.Select(i => new ItemList
                    {
                        Mtr_id = i.Mtr_id,
                        Mtr_Name = i.Mtr_Name
                    }).ToList()
                })
                .Where(i => i.Mtr_Level != null && i.Mtr_Level != "")
                .ToList();

            return Ok(result);
        }
    }
}
