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

        //案條件搜尋
        [HttpGet]
        public IActionResult Get(string? L ,string? P)
        {
            var query = _webContext.Product.AsQueryable();

            if (!string.IsNullOrEmpty(L)) query = query.Where(q => q.Pro_Level == L);
            if (!string.IsNullOrEmpty(P)) query = query.Where(q => q.Pro_part == P);

            var result = query.Select(p => new BOMGetDto
            { 
                Pro_Id = p.Pro_Id,
                Pro_Name = p.Pro_Name,
                Materials = p.BOMs.Select(b => new MtrDetailDto
                {
                    Mtr_id = b.Mtr_id,
                    Mtr_Name = b.Item.Mtr_Name,
                    Use_QTY = b.Use_QTY
                }).ToList()
            }).ToList();

            return Ok(result);
        }

        //案編號搜尋
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var query = _webContext.Product.Where(p => p.Pro_Id == id).AsQueryable();

            var result = query.Select(p => new BOMGetDto 
            {
                Pro_Id = p.Pro_Id,
                Pro_Name = p.Pro_Name,
                Materials = p.BOMs.Select(b => new MtrDetailDto 
                {
                    Mtr_id = b.Mtr_id,
                    Mtr_Name = b.Item.Mtr_Name,
                    Use_QTY = b.Use_QTY
                }).ToList()
            }).FirstOrDefault();

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        //案條件搜尋(BOM初始資料型態)
        [HttpGet("BOM")]
        public IActionResult GetBOM(string? L, string? P)
        {
            var query = _webContext.Product.AsQueryable();

            if (!string.IsNullOrEmpty(L)) query = query.Where(q => q.Pro_Level == L);
            if (!string.IsNullOrEmpty(P)) query = query.Where(q => q.Pro_part == P);

            var result = query.Select(p => new BOMAddDto
            {
                Pro_Id = p.Pro_Id,
                Pro_Name = p.Pro_Name,
                MtrDetailId = p.BOMs.Select(b => new MtrDetailIdDto
                {
                    Mtr_Id = b.Mtr_id,
                    Use_QTY = b.Use_QTY
                }).ToList()
            }).ToList();

            return Ok(result);
        }

        //案編號搜尋(BOM初始資料型態)
        [HttpGet("{id}/BOM")]
        public IActionResult GetByIdBOM(string id)
        {
            var query = _webContext.Product.Where(p => p.Pro_Id == id).AsQueryable();

            var result = query.Select(p => new BOMAddDto
            {
                Pro_Id = p.Pro_Id,
                Pro_Name = p.Pro_Name,
                MtrDetailId = p.BOMs.Select(b => new MtrDetailIdDto
                {
                    Mtr_Id = b.Mtr_id,
                    Use_QTY = b.Use_QTY
                }).ToList()
            }).FirstOrDefault();

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public IActionResult Post([FromBody] List<BOMGetDto> BOMDtoList)
        {
            if (BOMDtoList == null) return BadRequest();
            string logMsg = "";

            foreach (var dto in BOMDtoList)
            {
                foreach (var mtr in dto.Materials)
                {
                    //檢查是否有重複主鍵
                    var exist = _webContext.BOM.FirstOrDefault(b => b.Pro_Id == dto.Pro_Id && b.Mtr_id == mtr.Mtr_id);
                    if (exist != null)  //有重複，跳過該筆資料並記錄錯誤訊息
                    {
                        if (exist.Use_QTY != mtr.Use_QTY)
                        {
                            exist.Use_QTY = mtr.Use_QTY; //更新數量
                            logMsg += $"[更新]{dto.Pro_Id}材料{mtr.Mtr_id}->數量" + mtr.Use_QTY + "\n";
                        }
                    }
                    else
                    {
                        //無重複，新增BOM物件到清單
                        _webContext.BOM.Add(new BOM { Pro_Id = dto.Pro_Id, Mtr_id = mtr.Mtr_id, Use_QTY = mtr.Use_QTY });
                        logMsg += $"[新增]{dto.Pro_Id}材料{mtr.Mtr_id}\n";
                    }
                }
            }

            //foreach (var dto in BOMDtoList)
            //{
            //    foreach (var mtr in dto.MtrDetailId)
            //    {
            //        //檢查是否有重複主鍵
            //        var exist = _webContext.BOM.FirstOrDefault(b => b.Pro_Id == dto.Pro_Id && b.Mtr_id == mtr.Mtr_Id);
            //        if (exist != null)  //有重複，跳過該筆資料並記錄錯誤訊息
            //        {
            //            if (exist.Use_QTY != mtr.Use_QTY)
            //            {
            //                exist.Use_QTY = mtr.Use_QTY; //更新數量
            //                logMsg += $"[更新]{dto.Pro_Id}材料{mtr.Mtr_Id}->數量" + mtr.Use_QTY + "\n";
            //            }
            //        }
            //        else
            //        {
            //            //無重複，新增BOM物件到清單
            //            _webContext.BOM.Add(new BOM { Pro_Id = dto.Pro_Id, Mtr_id = mtr.Mtr_Id, Use_QTY = mtr.Use_QTY });
            //            logMsg += $"[新增]{dto.Pro_Id}材料{mtr.Mtr_Id}\n";
            //        }
            //    }
            //}

            _webContext.SaveChanges();
            return Ok("處理完成\n" + logMsg);
        }
        // DELETE api/<ValuesController>/5
        [HttpDelete("{proId}")]
        public IActionResult Delete(string proId, string? mtr_Id)
        {
            var query = _webContext.BOM.Where(b => b.Pro_Id == proId);

            if (!string.IsNullOrEmpty(mtr_Id))
                query = query.Where(b => b.Mtr_id == mtr_Id);
            
            var deletelist = query.ToList();

            if (!deletelist.Any())
                return NotFound("找不到對應的 BOM 資料，請檢查編號是否有誤。");

            _webContext.BOM.RemoveRange(deletelist);
            _webContext.SaveChanges();

            string msg = string.IsNullOrEmpty(mtr_Id)
                ? $"已清空產品 {proId} 的所有 BOM 資料"
                : $"已刪除產品 {proId} 中材料 {mtr_Id} 的紀錄";

            return Ok(msg);

        }

        
    }
}
