using Microsoft.EntityFrameworkCore;
using NetCoreStudy3_DTO.Models;

namespace WebAPI.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new WebContext(serviceProvider.GetRequiredService<DbContextOptions<WebContext>>()))
            {
                if (!context.Item.Any())
                {
                    #region Product檔 初始資料
                    context.Product.AddRange(

                    #region 71等 白麻、斯劍虎革

                    #region 巧匠
                    new Product
                    {
                        Pro_Level = "71",
                        Pro_Type = "1",
                        Pro_part = "1",
                        Pro_Id = "7111",
                        Pro_Name = "白麻巧匠頭巾"
                    }
                    , new Product
                    {
                        Pro_Level = "71",
                        Pro_Type = "1",
                        Pro_part = "2",
                        Pro_Id = "7112",
                        Pro_Name = "白麻巧匠工作服"
                    }
                    , new Product
                    {
                        Pro_Level = "71",
                        Pro_Type = "1",
                        Pro_part = "3",
                        Pro_Id = "7113",
                        Pro_Name = "斯劍虎革巧匠手套"
                    }
                    , new Product
                    {
                        Pro_Level = "71",
                        Pro_Type = "1",
                        Pro_part = "4",
                        Pro_Id = "7114",
                        Pro_Name = "白麻巧匠打底褲"
                    }
                    , new Product
                    {
                        Pro_Level = "71",
                        Pro_Type = "1",
                        Pro_part = "5",
                        Pro_Id = "7115",
                        Pro_Name = "斯劍虎革巧匠工作靴"
                    }
                    #endregion

                    #region 大地
                    ,new Product
                    {
                        Pro_Level = "71",
                        Pro_Type = "2",
                        Pro_part = "1",
                        Pro_Id = "7121",
                        Pro_Name = "白麻大地頭巾"
                    }
                    , new Product
                    {
                        Pro_Level = "71",
                        Pro_Type = "2",
                        Pro_part = "2",
                        Pro_Id = "7122",
                        Pro_Name = "白麻大地大衣"
                    }
                    , new Product
                    {
                        Pro_Level = "71",
                        Pro_Type = "2",
                        Pro_part = "3",
                        Pro_Id = "7123",
                        Pro_Name = "斯劍虎革大地手套"
                    }
                    , new Product
                    {
                        Pro_Level = "71",
                        Pro_Type = "2",
                        Pro_part = "4",
                        Pro_Id = "7124",
                        Pro_Name = "白麻大地打底褲"
                    }
                    , new Product
                    {
                        Pro_Level = "71",
                        Pro_Type = "2",
                        Pro_part = "5",
                        Pro_Id = "7125",
                        Pro_Name = "斯劍虎革大地工作靴"
                    }
                    #endregion

                    #endregion

                    #region 74等 仙子棉

                    #region 巧匠
                    ,new Product
                    {
                        Pro_Level = "74",
                        Pro_Type = "1",
                        Pro_part = "1",
                        Pro_Id = "7411",
                        Pro_Name = "仙子棉巧匠工作帽"
                    }
                    , new Product
                    {
                        Pro_Level = "74",
                        Pro_Type = "1",
                        Pro_part = "2",
                        Pro_Id = "7412",
                        Pro_Name = "仙子棉巧匠圍裙"
                    }
                    , new Product
                    {
                        Pro_Level = "74",
                        Pro_Type = "1",
                        Pro_part = "3",
                        Pro_Id = "7413",
                        Pro_Name = "仙子棉巧匠袖套"
                    }
                    , new Product
                    {
                        Pro_Level = "74",
                        Pro_Type = "1",
                        Pro_part = "4",
                        Pro_Id = "7414",
                        Pro_Name = "仙子棉巧匠馬褲"
                    }
                    #endregion

                    #endregion

                    #region 77等 纏尾蛟革

                    #region 巧匠
                    , new Product
                    {
                        Pro_Level = "77",
                        Pro_Type = "1",
                        Pro_part = "2",
                        Pro_Id = "7712",
                        Pro_Name = "纏尾蛟革巧匠外套"
                    }
                    , new Product
                    {
                        Pro_Level = "77",
                        Pro_Type = "1",
                        Pro_part = "3",
                        Pro_Id = "7713",
                        Pro_Name = "纏尾蛟革巧匠手套"
                    }
                    , new Product
                    {
                        Pro_Level = "77",
                        Pro_Type = "1",
                        Pro_part = "5",
                        Pro_Id = "7115",
                        Pro_Name = "纏尾蛟革巧匠工作靴"
                    }
                    #endregion

                    #region 大地
                    , new Product
                    {
                        Pro_Level = "77",
                        Pro_Type = "2",
                        Pro_part = "2",
                        Pro_Id = "7722",
                        Pro_Name = "纏尾蛟革大地長袍"
                    }
                    , new Product
                    {
                        Pro_Level = "77",
                        Pro_Type = "2",
                        Pro_part = "3",
                        Pro_Id = "7723",
                        Pro_Name = "纏尾蛟革大地手套"
                    }
                    , new Product
                    {
                        Pro_Level = "77",
                        Pro_Type = "2",
                        Pro_part = "5",
                        Pro_Id = "7725",
                        Pro_Name = "纏尾蛟革大地工作靴"
                    }
                    #endregion

#

                    #region 80等 矮人棉

                    #region 巧匠
                    new Product
                    {
                        Pro_Level = "80",
                        Pro_Type = "1",
                        Pro_part = "1",
                        Pro_Id = "8011",
                        Pro_Name = "矮人棉貝雷帽"
                    }
                    , new Product
                    {
                        Pro_Level = "80",
                        Pro_Type = "1",
                        Pro_part = "2",
                        Pro_Id = "8012",
                        Pro_Name = "矮人棉外套"
                    }
                    , new Product
                    {
                        Pro_Level = "80",
                        Pro_Type = "1",
                        Pro_part = "4",
                        Pro_Id = "8014",
                        Pro_Name = "矮人棉軟甲褲"
                    }
                    #endregion

                    #region 大地
                    , new Product
                    {
                        Pro_Level = "71",
                        Pro_Type = "2",
                        Pro_part = "1",
                        Pro_Id = "7121",
                        Pro_Name = "白麻大地頭巾"
                    }
                    , new Product
                    {
                        Pro_Level = "71",
                        Pro_Type = "2",
                        Pro_part = "2",
                        Pro_Id = "7122",
                        Pro_Name = "白麻大地大衣"
                    }
                    , new Product
                    {
                        Pro_Level = "71",
                        Pro_Type = "2",
                        Pro_part = "3",
                        Pro_Id = "7123",
                        Pro_Name = "斯劍虎革大地手套"
                    }
                    , new Product
                    {
                        Pro_Level = "71",
                        Pro_Type = "2",
                        Pro_part = "4",
                        Pro_Id = "7124",
                        Pro_Name = "白麻大地打底褲"
                    }
                    , new Product
                    {
                        Pro_Level = "71",
                        Pro_Type = "2",
                        Pro_part = "5",
                        Pro_Id = "7125",
                        Pro_Name = "斯劍虎革大地工作靴"
                    }
                    #endregion

                    #endregion

                    #endregion
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}