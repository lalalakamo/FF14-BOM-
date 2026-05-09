using FF14BOM.Dtos;
using FF14BOM.Models;

namespace FF14BOM.Services
{
    public class ProductService
    {
        private readonly WebContext _webContext;

        public ProductService(WebContext webContext)
        {
            _webContext = webContext;
        }

        private static ProductGetDto ProductGetDto(Product p)
        { 
            var result = new ProductGetDto
            {
                Pro_Id = p.Pro_Id,
                Pro_Name = p.Pro_Name,
                Pro_Level = p.Pro_Level,
                Pro_Type = p.Pro_Type,
                Pro_part = p.Pro_part
            };
            return result;
        }

        public List<ProductGetDto> GetProducts(string? L, string? P)
        {
            var query = _webContext.Product.AsQueryable();

            if (!string.IsNullOrEmpty(L)) query = query.Where(q => q.Pro_Level == L);
            if (!string.IsNullOrEmpty(P)) query = query.Where(q => q.Pro_part == P);

            return query.Select(p => ProductGetDto(p)).ToList();
        }

        public ProductGetDto GetProduct(string id)
        { 
            var query = _webContext.Product.Where(p => p.Pro_Id == id).AsQueryable();

            return query.Select(p => ProductGetDto(p)).FirstOrDefault();
        }
        
    }
}
