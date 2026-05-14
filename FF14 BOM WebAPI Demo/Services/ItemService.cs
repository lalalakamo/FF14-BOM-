using FF14BOM.Dtos;
using FF14BOM.Models;

namespace FF14BOM.Services
{
    public class ItemService
    {
        private readonly WebContext _webContext;

        public ItemService(WebContext webContext)
        {
            _webContext = webContext;
        }

        public static ItemGetDto GetItemDto(Item i)
        {
            var result = new ItemGetDto
            {
                Mtr_id = i.Mtr_id,
                mtr_Level = i.Mtr_Level,
                Mtr_Name = i.Mtr_Name,
                Mtr_type = i.Mtr_type
            };
            return result;
        }

        public List<ItemGetDto> GetItems(string? T, string? L)
        {
            var query = _webContext.Item.AsQueryable();

            if(!string.IsNullOrEmpty(T)) query = query.Where(i => i.Mtr_type == T);
            if (!string.IsNullOrEmpty(L)) query = query.Where(i => i.Mtr_Level == L);

            return query.Select(i => GetItemDto(i)).ToList();
        }

        public ItemGetDto  GetItem(string id)
        {
            var query = _webContext.Item.Where(i => i.Mtr_id == id).AsQueryable();
            if (query == null)
                return null;
            return query.Select(i => GetItemDto(i)).FirstOrDefault();
        }

        public string PostItem(List<ItemAddDto> itemDtosList)
        {
            string logmsg = "";
            foreach (var dto in itemDtosList)
            {
                if (string.IsNullOrEmpty(dto.Mtr_id))
                {
                    if (string.IsNullOrEmpty(dto.Mtr_type))
                        return "若未輸入材料編號，材料類別不能為空，無法自動產生編號";

                        logmsg += $"新增{CreateItem(dto)}的材料資料成功\n";
                }
                else
                {
                    var exist = _webContext.Item.FirstOrDefault(i => i.Mtr_id == dto.Mtr_id);
                    if (exist == null)
                        logmsg += $"新增{CreateItem(dto)}的材料資料成功\n";
                    else
                    {
                        if (exist.NPC_Sell != dto.NPC_Sell || exist.Mtr_Name != dto.Mtr_Name || exist.Mtr_type != dto.Mtr_type || exist.Mtr_Level != dto.mtr_Level)
                        {
                            exist.Mtr_Level = dto.mtr_Level;
                            exist.Mtr_Name = dto.Mtr_Name;
                            exist.Mtr_type = dto.Mtr_type;

                            //_webContext.SaveChanges();
                            logmsg += $"更新{dto.Mtr_id}的材料資料成功\n";
                        }
                        else
                        {
                            logmsg += $"已經有{dto.Mtr_id}的材料資料，且內容相同，跳過\n";
                        }
                    }
                }
            }
            _webContext.SaveChanges();
            return logmsg;
        }

        public string DeleteItem(string id)
        {
            var delete = _webContext.Item.FirstOrDefault(i => i.Mtr_id == id);
            if (delete == null)
                return $"找不到id={id}的材料資料";
            _webContext.Item.Remove(delete);
            _webContext.SaveChanges();
            return $"刪除{id}的材料資料成功";
        }

        private string CreateItem(ItemAddDto dto)
        {
            string mtr_id;
            if (!string.IsNullOrEmpty(dto.Mtr_id))
                mtr_id = dto.Mtr_id;
            else
            {
                int newIDNum;
                var search = _webContext.Item.Where(i => i.Mtr_type == dto.Mtr_type)
                                              .OrderByDescending(i => i.Mtr_id)
                                              .Select(i => i.Mtr_id)
                                              .FirstOrDefault();
                newIDNum = int.Parse(search.Substring(1)) + 1;
                mtr_id = $"{dto.Mtr_type}{newIDNum.ToString("D4")}";
            }

            _webContext.Item.Add(new Item
            {
                Mtr_id = mtr_id,
                Mtr_Level = dto.mtr_Level,
                Mtr_Name = dto.Mtr_Name,
                Mtr_type = dto.Mtr_type
            });
            //_webContext.SaveChanges();
            return mtr_id;
        }
    }
}
