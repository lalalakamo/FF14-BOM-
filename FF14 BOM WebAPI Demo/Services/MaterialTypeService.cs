using FF14BOM.Dtos;
using FF14BOM.Models;
using Microsoft.Identity.Client;

namespace FF14BOM.Services
{
    public class MaterialTypeService
    {
        private readonly WebContext _webcontext;

        public MaterialTypeService(WebContext webcontext)
        {
            _webcontext = webcontext;
        }

        public static MaterialTypeGetDto GetMaterialTypeDto(MaterialType m)
        {
            var result = new MaterialTypeGetDto
            {
                Mtr_type = m.Mtr_type,
                Mtr_Type_name = m.Mtr_Type_name
            };
            return result;
        }

        public List<MaterialTypeGetDto> GetMatetialTypes()
        {
            return _webcontext.MaterialType.Select(m => GetMaterialTypeDto(m)).ToList();
        }

        public MaterialTypeGetDto GetMatetialType(string id)
        {
            var query = _webcontext.MaterialType.FirstOrDefault(m => m.Mtr_type == id);
            if (query == null)
                return null;
            return GetMaterialTypeDto(query);
        }

        public string PostMaterialType(List<MaterialTypeGetDto> MTRDtoList)
        {
            string logmsg = "";

            foreach (var dto in MTRDtoList)
            {
                var exist = _webcontext.MaterialType.FirstOrDefault(a => a.Mtr_type == dto.Mtr_type);
                if (exist != null)
                {
                    exist.Mtr_Type_name = dto.Mtr_Type_name;
                    logmsg += $"更新{dto.Mtr_type}的類別名稱為{dto.Mtr_Type_name}\n";
                }
                else
                {
                    _webcontext.Add(new MaterialType { Mtr_type = dto.Mtr_type, Mtr_Type_name = dto.Mtr_Type_name });
                    logmsg += $"新增{dto.Mtr_type}的類別資料成功\n";
                }
            }
            _webcontext.SaveChanges();
            return logmsg;
        }

        public string DeleteMaterialType(string mtr_type)
        {
            var delete = _webcontext.MaterialType.FirstOrDefault(m => m.Mtr_type == mtr_type);
            if (delete == null)
                return $"找不到{mtr_type}材料類別";
            else
            {
                _webcontext.MaterialType.Remove(delete);
                _webcontext.SaveChanges();
                return $"材料類別{mtr_type}已刪除";
            }    
        }

    }
}
