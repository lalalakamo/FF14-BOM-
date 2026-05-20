using FF14BOM.Dtos;
using FF14BOM.Models;
using FF14BOM.Services;
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
        private readonly SearchService _searchService;

        public SearchController(WebContext webContext, SearchService searchService)
        {
            _webContext = webContext;
            _searchService = searchService;
        }

        // GET: api/<SearchController>
        [HttpGet]
        public IActionResult GetMain()
        {
            //return "輸入產品Id，並以+隔開 EX:7111+7112 ";
            return Ok(_searchService.GetProductMain());
        }

        // GET api/<SearchController>/5
        [HttpGet("{para}")]
        public IActionResult Get(string para)
        {
            return Ok(_searchService.GetResult(para));
        }
    }
}
