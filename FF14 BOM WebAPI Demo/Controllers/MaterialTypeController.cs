using FF14BOM.Dtos;
using FF14BOM.Models;
using FF14BOM.Services;
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
        private readonly MaterialTypeService _materialtypeService;

        public MaterialTypeController(WebContext webContext,MaterialTypeService materialTypeService)
        {
            _webContext = webContext;
            _materialtypeService = materialTypeService;
        }

        // GET: api/<MaterialTypeController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_materialtypeService.GetMatetialTypes());
        }

        // GET api/<MaterialTypeController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var result = _materialtypeService.GetMatetialType(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        // POST api/<MaterialTypeController>
        [HttpPost]
        public IActionResult Post([FromBody] List<MaterialTypeGetDto> MTRDtoList)
        {
            if (MTRDtoList == null) return BadRequest("請提供類別資料");
            return Ok(_materialtypeService.PostMaterialType(MTRDtoList));
        }

        // DELETE api/<MaterialTypeController>/5
        [HttpDelete("{mtr_type}")]
        public IActionResult Delete(string mtr_type)
        {
            return Ok(_materialtypeService.DeleteMaterialType(mtr_type));
            //var delete = _webContext.MaterialType.FirstOrDefault(a => a.Mtr_type == mtr_type);
            //if (delete == null)
            //    return NotFound($"沒有{mtr_type}的類別資料");
            //_webContext.MaterialType.Remove(delete);
            //_webContext.SaveChanges();
            //return Ok($"刪除{mtr_type}的類別資料成功");
        }
    }
}
