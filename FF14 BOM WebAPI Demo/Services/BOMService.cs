using FF14BOM.Dtos;
using FF14BOM.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace FF14BOM.Services
{
    public class BOMService
    {
        private readonly WebContext _webContext;

        public BOMService(WebContext webContext)
        {
            _webContext = webContext;
        }

        public IQueryable<BOMGetDto> BOMGetDto(IQueryable<Product> p)
        {
            var result = p.Select(p => new BOMGetDto
            {
                Pro_Id = p.Pro_Id,
                Pro_Name = p.Pro_Name,
                Materials = p.BOMs.Select(b => new MtrDetailDto
                {
                    Mtr_id = b.Mtr_id,
                    Mtr_Name = b.Item.Mtr_Name,
                    Use_QTY = b.Use_QTY
                }).ToList()
            });
            return result;
        }
        public List<BOMGetDto> GetBOMs(string? L, string? P)
        {
            var query = _webContext.Product.AsQueryable();

            if (!string.IsNullOrEmpty(L)) query = query.Where(q => q.Pro_Level == L);
            if (!string.IsNullOrEmpty(P)) query = query.Where(q => q.Pro_part == P);

            return BOMGetDto(query).ToList();
        }

        public BOMGetDto? GetBOM(string id)
        {
            var query = _webContext.Product.Where(p => p.Pro_Id == id).AsQueryable();
            return BOMGetDto(query).FirstOrDefault();

        }
    }
}
