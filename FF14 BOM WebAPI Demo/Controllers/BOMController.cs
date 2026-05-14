using Azure.Core;
using FF14BOM.Dtos;
using FF14BOM.Models;
using FF14BOM.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FF14BOM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BOMController : ControllerBase
    {
        private readonly WebContext _webContext;
        private readonly BOMService _bomService;

        public BOMController(WebContext webContext,BOMService bomService)
        {
            _webContext = webContext;
            _bomService = bomService;
        }


        //案條件搜尋
        [HttpGet]
        public IActionResult Get(string? L ,string? P)
        {
            return Ok(_bomService.GetBOMs(L, P));
        }

        //案編號搜尋
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var result = _bomService.GetBOM(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public IActionResult Post([FromBody] List<BOMGetDto> BOMDtoList)
        {
            if (BOMDtoList == null) return BadRequest();

            return Ok(_bomService.PostBOM(BOMDtoList));
        }
        // DELETE api/<ValuesController>/5
        [HttpDelete("{proId}")]
        public IActionResult Delete(string proId)
        {
            return Ok(_bomService.DeleteBOM(proId));
        }

        [HttpPut("{proIdfrom}/{proIdto}")]
        public IActionResult Copy(string proIdfrom,string proIdto)
        {
            return Ok(_bomService.CopyBOM(proIdfrom, proIdto));
        }
    }
}
