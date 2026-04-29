using FF14BOM.Dtos;
using FF14BOM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FF14BOM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly WebContext _webContext;

        public SearchController(WebContext webContext)
        {
            _webContext = webContext;
        }

        // GET: api/<SearchController>
        [HttpGet]
        public string Get()
        {
            return "輸入產品Id，並以+隔開 EX:7111+7112 ";
        }

        // GET api/<SearchController>/5
        [HttpGet("{para}")]
        public IEnumerable<BOMGetListDto> Get(string para)
        {
            string[] products = para.Split("+");

            var searchMTR = _webContext.BOM
                .Include(b => b.Item)
                .Where(b => products.Contains(b.Pro_Id))
                .ToList();

            var result = searchMTR
                .GroupBy(s => s.Mtr_id)
                .Select(g => new BOMGetListDto
                {
                    Mtr_Name = g.First().Item.Mtr_Name,
                    Use_QTY = g.Sum(s => s.Use_QTY)
                })
                .ToList();

            return result;
        }
    }
}
