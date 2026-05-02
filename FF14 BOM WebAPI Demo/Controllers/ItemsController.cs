using FF14BOM.Dtos;
using FF14BOM.Models;
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

        public ItemsController(WebContext webContext)
        {
            _webContext = webContext;
        }

        // GET: api/<ItemsController>
        [HttpGet]
        public IActionResult Get(string? T)
        {
            var query = _webContext.Item.AsQueryable();
            if(!string.IsNullOrEmpty(T))
                query = query.Where(q => q.Mtr_type == T);

            var result = query.Select(i => new ItemGetDto 
            { 
                Mtr_id = i.Mtr_id,
                Mtr_type = i.Mtr_type,
                Mtr_Name = i.Mtr_Name,
                NPC_Sell = i.NPC_Sell
            }).ToList();

            return Ok(result);
        }

        // GET api/<ItemsController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var result = _webContext.Item.Select(i => new ItemGetDto { 
                Mtr_id = i.Mtr_id,
                Mtr_type = i.Mtr_type,
                Mtr_Name = i.Mtr_Name,
                NPC_Sell = i.NPC_Sell
            })
            .Where(i => i.Mtr_id == id).FirstOrDefault();
            if (result == null) return NotFound($"沒有{ id }的材料資料");
            return Ok(result);

        }

        // POST api/<ItemsController>
        [HttpPost]
        public IActionResult Post([FromBody] List<ItemAddDto> itemDtosList)
        {
            if (itemDtosList == null) return BadRequest();
            string logmsg="";
            foreach (var dto in itemDtosList)
            {
                if (string.IsNullOrEmpty(dto.Mtr_id))
                {
                    if(string.IsNullOrEmpty(dto.Mtr_type))
                        return BadRequest("若未輸入材料編號，材料類別不能為空，無法自動產生編號");

                    var search = _webContext.Item
                        .Where(i => i.Mtr_type == dto.Mtr_type)
                        .OrderByDescending(i => i.Mtr_id)
                        .Select(i => i.Mtr_id)
                        .FirstOrDefault();

                    int nextNum = 1;

                    if (search != null && search.Length > 1)
                    { 
                        if(int.TryParse(search.Substring(1),out int currentMax))
                            nextNum = currentMax +1;
                    }

                    string newId = $"{dto.Mtr_type}{nextNum.ToString("D4")}";

                    _webContext.Item.Add(new Item {
                        Mtr_id = newId,
                        Mtr_Name = dto.Mtr_Name,
                        Mtr_type = dto.Mtr_type,
                        NPC_Sell = dto.NPC_Sell
                    });
                    logmsg += $"新增{newId}的材料資料成功\n";

                }
                else
                {
                    var exist = _webContext.Item.FirstOrDefault(i => i.Mtr_id == dto.Mtr_id);
                    if (exist != null)
                    {
                        if (exist.NPC_Sell != dto.NPC_Sell || exist.Mtr_Name != dto.Mtr_Name || exist.Mtr_type != dto.Mtr_type)
                        {
                            exist.NPC_Sell = dto.NPC_Sell;
                            exist.Mtr_Name = dto.Mtr_Name;
                            exist.Mtr_type = dto.Mtr_type;
                            logmsg += $"更新{dto.Mtr_id}的材料資料成功\n";
                        }
                        else
                        {
                            logmsg += $"已經有{dto.Mtr_id}的材料資料，且內容相同，跳過\n";
                        }
                    }
                    else
                    {
                        _webContext.Item.Add(new Item
                        {
                            Mtr_id = dto.Mtr_id,
                            Mtr_Name = dto.Mtr_Name,
                            Mtr_type = dto.Mtr_type,
                            NPC_Sell = dto.NPC_Sell
                        });
                        logmsg += $"新增{dto.Mtr_id}的材料資料成功\n";
                    }
                }
            }
            _webContext.SaveChanges();
            return Ok(logmsg);
        }
        // DELETE api/<ItemsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var delete = _webContext.Item.FirstOrDefault(i => i.Mtr_id == id);
            if (delete == null) return NotFound($"沒有{id}的材料資料，無法刪除");
            _webContext.Item.Remove(delete);
            _webContext.SaveChanges();
            return Ok($"刪除{id}的材料資料成功");
        }
    }
}
