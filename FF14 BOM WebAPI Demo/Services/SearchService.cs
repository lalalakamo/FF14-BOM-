using FF14BOM.Dtos;
using FF14BOM.Models;
using Microsoft.EntityFrameworkCore;

namespace FF14BOM.Services
{
    public class SearchService
    {
        private readonly WebContext _webContext;
        

        public SearchService(WebContext webContext)
        {
            _webContext = webContext;
        }

        public List<ProductGetMainDto> GetProductMain()
        {
            var result = _webContext.Product
                .GroupBy(p => p.Pro_Level)
                .Select(g => new ProductGetMainDto
                {
                    Pro_Level = g.Key,
                    Products = g.Select(p => new ProductByLevelDto
                    {
                        Pro_Name = p.Pro_Name,
                        Pro_Id = p.Pro_Id
                    }).ToList()
                }).ToList();
            return result;
        }

        public List<BOMGetListDto> GetResult(string para)
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
