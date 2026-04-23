using FF14BOM.Dtos;
using FF14BOM.Models;
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

        public BOMController(WebContext webContext)
        {
            _webContext = webContext;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<BOMGetDto> Get()
        {
            var results = _webContext.Product
                .Select(p => new BOMGetDto
                {
                    Pro_Name = p.Pro_Name,
                    Pro_Id = p.Pro_Id,
                    Materials = _webContext.BOM
                   .Where(b => b.Pro_Id == p.Pro_Id)
                   .Select(b => new MtrDetailDto
                   {
                       Use_QTY = b.Use_QTY,
                       Mtr_Name = _webContext.Item
                       .Where(i => i.Mtr_id == b.Mtr_id)
                       .Select(i => i.Mtr_Name)
                       .FirstOrDefault()
                   })
                   .ToList()

                });
                //.ToList();
            return results;
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public IEnumerable<BOMGetDto> GetById(string id)
        {
            var result = _webContext.Product
                .Where(p => p.Pro_Id == id)
                .Select(p => new BOMGetDto
                {
                    Pro_Name = p.Pro_Name,
                    Pro_Id = p.Pro_Id,
                    Materials = _webContext.BOM
                    .Where(b => b.Pro_Id == p.Pro_Id && b.Pro_Id == id)
                    .Select(b => new MtrDetailDto
                    {
                        Use_QTY = b.Use_QTY,
                        Mtr_Name = _webContext.Item
                        .Where(i => i.Mtr_id == b.Mtr_id)
                        .Select(i => i.Mtr_Name)
                        .FirstOrDefault()
                    }).ToList()
                });
            return result;
        }

        //案條件搜尋
        [HttpGet("C")]
        public IEnumerable<BOMGetDto> GetByCondition(string? L ,string? P)
        { 
            var result = _webContext.Product
                .Where(p => (L == null || p.Pro_Level == L) && (P == null || p.Pro_part == P))
                .Select(p => new BOMGetDto
                {
                    Pro_Name = p.Pro_Name,
                    Pro_Id = p.Pro_Id,
                    Materials = _webContext.BOM
                    .Where(b => b.Pro_Id == p.Pro_Id)
                    .Select(b => new MtrDetailDto
                    {
                        Use_QTY = b.Use_QTY,
                        Mtr_Name = _webContext.Item
                        .Where(i => i.Mtr_id == b.Mtr_id)
                        .Select(i => i.Mtr_Name)
                        .FirstOrDefault()
                    }).ToList()
                }).ToList();
            return result;
        }

        //案條件搜尋
        [HttpGet("LIST/{id}")]
        public IEnumerable<BOMAddDto> GetByList(string id)
        {
            var result = _webContext.Product
                .Where(p => p.Pro_Id == id)
                .Select(p => new BOMAddDto
                {
                    Pro_Name = p.Pro_Name,
                    Pro_Id = p.Pro_Id,
                    MtrDetailId = _webContext.BOM
                        .Where(b => b.Pro_Id == p.Pro_Id)
                        .Select(b => new MtrDetailIdDto
                        {
                            Mtr_Id = b.Mtr_id,
                            Use_QTY = b.Use_QTY
                        }).ToList(),
                }).ToList();

            return result;
        }


        // POST api/<ValuesController>
        [HttpPost]
        public IActionResult Post([FromBody] List<BOMAddDto> BOMDtoList)
        {
            string okmsg="",badmsg="";
            var itemstoadd = new List<BOM>();

            string Pro_Id = BOMDtoList[0].Pro_Id;
            foreach (var mtr in BOMDtoList[0].MtrDetailId)
            {
                string Mtr_Id = mtr.Mtr_Id;
                int Use_QTY = mtr.Use_QTY;

                //檢查是否有重複主鍵
                bool exist = _webContext.BOM.Any(b => b.Pro_Id == Pro_Id && b.Mtr_id == Mtr_Id);
                if (exist)  //有重複，跳過該筆資料並記錄錯誤訊息
                { 
                    badmsg += $"產品編號{Pro_Id}+材料編號{Mtr_Id}\n";
                    continue;
                }

                //無重複，新增BOM物件到清單
                itemstoadd.Add(new BOM
                {
                    Pro_Id = Pro_Id,
                    Mtr_id = Mtr_Id,
                    Use_QTY = Use_QTY
                });
                okmsg += $"產品編號{Pro_Id}+材料編號{Mtr_Id}\n";
            }
            if (itemstoadd.Any())
            {
                _webContext.BOM.AddRange(itemstoadd);
                _webContext.SaveChanges();
            }
            return Ok("成功寫入" + okmsg + "\n" + "已存在未寫入:" + badmsg);
        }

        //先執行GET/LIST後，直接修改BOM LIST後，再執行PUT，會直接覆蓋原有BOM資料，請注意！
        // PUT api/<ValuesController>/5
        //[HttpPut("PUT")]
        //public IActionResult Put([FromBody] List<BOMDto> BOMDtoList)
        //{
        //    var itemstoadd = new List<BOM>();

        //    foreach(var dto in BOMDtoList)
        //    {
        //        bool exist = _webContext.BOM.Any(b => b.Pro_Id == dto.Pro_Id && b.Mtr_id == dto.Mtr_id);
        //        if (exist)
        //            return BadRequest($"產品編號{dto.Pro_Id}+材料編號{dto.Mtr_id}已存在，請確認後再試一次！");



        //    }


        //}

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }

        
    }
}
