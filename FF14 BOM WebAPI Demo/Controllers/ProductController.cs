using FF14BOM.Dtos;
using FF14BOM.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FF14BOM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly WebContext _webContext;

        public ProductController(WebContext webContext)
        {
            _webContext = webContext;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public IActionResult Get(string? L,string? P)
        {
            if(string.IsNullOrEmpty(L) && string.IsNullOrEmpty(P))
                return NotFound("裝備編號組成:等級2碼+職業類別(巧匠1大地2)+部位(頭1身2手3褲4腳5)");

            var result = _webContext.Product.Select(p => new ProductDto
            { 
                Pro_Id = p.Pro_Id,
                Pro_Name = p.Pro_Name,
                Pro_Level = p.Pro_Level,
                Pro_Type = p.Pro_Type,
                Pro_part = p.Pro_part
            }).AsQueryable();

            if(!string.IsNullOrEmpty(L)) result = result.Where(r => r.Pro_Level == L);
            if(!string.IsNullOrEmpty(P)) result = result.Where(r => r.Pro_part == P);
            
            result.ToList();

            if (result == null)
                return NotFound();
            else
                return Ok(result);
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var result = _webContext.Product.Select(p => new ProductDto { 
                Pro_Id = p.Pro_Id,
                Pro_Name = p.Pro_Name,
                Pro_Level = p.Pro_Level,
                Pro_Type = p.Pro_Type,
                Pro_part = p.Pro_part
            }).FirstOrDefault(p => p.Pro_Id == id);
            return Ok(result);
        }

        // POST api/<ProductController>
        [HttpPost]
        public IActionResult Post([FromBody] List<ProductDto> ProductDtoList)
        {
            if (ProductDtoList == null) return BadRequest();
            string logmsg = "";

            foreach (var dto in ProductDtoList)
            { 
                //檢查是否有重複主鍵
                var exist = _webContext.Product.FirstOrDefault(p => p.Pro_Id == dto.Pro_Id);
                if (exist != null)
                { 
                    exist.Pro_Name = dto.Pro_Name;
                    exist.Pro_Level = dto.Pro_Level;
                    exist.Pro_Type = dto.Pro_Type;
                    exist.Pro_part = dto.Pro_part;
                    logmsg += $"已經有{dto.Pro_Id}的裝備資料，已更新為最新資料\n";
                }
                else
                {
                    _webContext.Product.Add(new Product { Pro_Id = dto.Pro_Id, Pro_Name = dto.Pro_Name, Pro_Level = dto.Pro_Level, Pro_Type = dto.Pro_Type, Pro_part = dto.Pro_part });
                    logmsg += $"新增{dto.Pro_Id}的裝備資料成功\n";
                }
            }
            _webContext.SaveChanges();
            return Ok(logmsg);
        }
    }
}
