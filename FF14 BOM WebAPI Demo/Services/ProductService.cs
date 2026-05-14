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
            if (query == null)
                return null;
            return query.Select(p => ProductGetDto(p)).FirstOrDefault();
        }

        public string PostProduct(List<ProductAddDto> ProductDtoList)
        {
            string logmsg = "";

            foreach (var dto in ProductDtoList)
            {
                if (!string.IsNullOrEmpty(dto.Pro_Id))
                {
                    //檢查是否有重複主鍵
                    if (UpdateProduct(dto))
                        logmsg += $"已經有{dto.Pro_Id}的裝備資料，已更新為最新資料\n";
                    else
                    {
                        CreateProduct(dto);
                        logmsg += $"新增{dto.Pro_Id}的裝備資料成功\n";
                    }
                    var exist = _webContext.Product.FirstOrDefault(p => p.Pro_Id == dto.Pro_Id);
                }
                else
                {
                    //檢查是否有重複主鍵
                    if (UpdateProduct(dto))
                        logmsg += $"已經有{dto.Pro_Id}的裝備資料，已更新為最新資料\n";
                    else
                    {
                        CreateProduct(dto);
                        logmsg += $"新增{dto.Pro_Level + dto.Pro_Type + dto.Pro_part}的裝備資料成功\n";
                    }
                }
            }
            _webContext.SaveChanges();
            return logmsg;
        }

        public string DeleteProduct(string id)
        {
            var delete = _webContext.Product.FirstOrDefault(p => p.Pro_Id == id);
            if (delete == null)
                return $"{id}產品資料不存在";
            _webContext.Product.Remove(delete);
            _webContext.SaveChanges();
            return $"{id}產品資料已刪除";
        }

        private void CreateProduct(ProductAddDto dto)
        {
            _webContext.Product.Add(new Product
            {
                Pro_Id = dto.Pro_Level + dto.Pro_Type + dto.Pro_part,
                Pro_Name = dto.Pro_Name,
                Pro_Level = dto.Pro_Level,
                Pro_Type = dto.Pro_Type,
                Pro_part = dto.Pro_part
            });

            //自動新增符合該裝備等級的材料bom
            var items = _webContext.Item.Where(i => i.Mtr_Level == dto.Pro_Level).ToList();
            foreach (var item in items)
            {
                _webContext.BOM.Add(new BOM
                {
                    Pro_Id = dto.Pro_Level + dto.Pro_Type + dto.Pro_part,
                    Mtr_id = item.Mtr_id,
                    Use_QTY = 0
                });
            }

        }

        private bool UpdateProduct(ProductAddDto dto)
        {
            string Pro_id;

            if(string.IsNullOrEmpty(dto.Pro_Id))
                Pro_id = dto.Pro_Level + dto.Pro_Type + dto.Pro_part;
            else
                Pro_id = dto.Pro_Id;

            var exist = _webContext.Product.FirstOrDefault(p => p.Pro_Id == Pro_id);
            if (exist != null)
            {
                exist.Pro_Name = dto.Pro_Name;
                exist.Pro_Level = dto.Pro_Level;
                exist.Pro_Type = dto.Pro_Type;
                exist.Pro_part = dto.Pro_part;

                _webContext.SaveChanges();
                return true;
            }
            return false;
        }

    }
}
