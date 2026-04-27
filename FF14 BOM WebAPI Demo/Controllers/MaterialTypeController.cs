using FF14BOM.Dtos;
using FF14BOM.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FF14BOM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialTypeController : ControllerBase
    {
        private readonly WebContext _webContext;

        public MaterialTypeController(WebContext webContext)
        {
            _webContext = webContext;
        }

        // GET: api/<MaterialTypeController>
        [HttpGet]
        public IActionResult Get()
        {
            var result = _webContext.MaterialType.Select(a => new MaterialTypeGetDto { 
                Mtr_type = a.Mtr_type,
                Mtr_Type_name = a.Mtr_Type_name
            }).ToList();
            return Ok(result);

        }

        // GET api/<MaterialTypeController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _webContext.MaterialType.Select(a => new MaterialTypeGetDto
            {
                Mtr_type = a.Mtr_type,
                Mtr_Type_name = a.Mtr_Type_name
            }).FirstOrDefault();
            return Ok(result);
        }

        // POST api/<MaterialTypeController>
        [HttpPost("{mtr_type}")]
        public IActionResult Post(string mtr_type,[FromBody] string mtr_name)
        {
            if (_webContext.MaterialType.Any(a => a.Mtr_type == mtr_type))
                return BadRequest($"已經有{mtr_type}的類別資料");
            _webContext.MaterialType.Add(new MaterialType { Mtr_type = mtr_type, Mtr_Type_name = mtr_name });
            _webContext.SaveChanges();
            return Ok($"新增{mtr_type}的類別資料成功");
        }

        // DELETE api/<MaterialTypeController>/5
        [HttpDelete("{mtr_type}")]
        public IActionResult Delete(string mtr_type)
        {
            if(_webContext.MaterialType.FirstOrDefault(a => a.Mtr_type == mtr_type) == null)
                return NotFound($"沒有{mtr_type}的類別資料");
            _webContext.MaterialType.Remove(new MaterialType { Mtr_type = mtr_type });
            _webContext.SaveChanges();
            return Ok($"刪除{mtr_type}的類別資料成功");
        }
    }
}
