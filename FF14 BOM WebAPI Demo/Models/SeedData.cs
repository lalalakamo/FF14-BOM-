using Microsoft.EntityFrameworkCore;
using FF14BOM.Models;

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

                    //#region 巧匠
                    //,new Product
                    //{
                    //    Pro_Level = "74",
                    //    Pro_Type = "1",
                    //    Pro_part = "1",
                    //    Pro_Id = "7411",
                    //    Pro_Name = "仙子棉巧匠工作帽"
                    //}
                    //, new Product
                    //{
                    //    Pro_Level = "74",
                    //    Pro_Type = "1",
                    //    Pro_part = "2",
                    //    Pro_Id = "7412",
                    //    Pro_Name = "仙子棉巧匠圍裙"
                    //}
                    //, new Product
                    //{
                    //    Pro_Level = "74",
                    //    Pro_Type = "1",
                    //    Pro_part = "3",
                    //    Pro_Id = "7413",
                    //    Pro_Name = "仙子棉巧匠袖套"
                    //}
                    //, new Product
                    //{
                    //    Pro_Level = "74",
                    //    Pro_Type = "1",
                    //    Pro_part = "4",
                    //    Pro_Id = "7414",
                    //    Pro_Name = "仙子棉巧匠馬褲"
                    //}
                    //#endregion

                    #endregion

                    #region 77等 纏尾蛟革

                    //#region 巧匠
                    //, new Product
                    //{
                    //    Pro_Level = "77",
                    //    Pro_Type = "1",
                    //    Pro_part = "2",
                    //    Pro_Id = "7712",
                    //    Pro_Name = "纏尾蛟革巧匠外套"
                    //}
                    //, new Product
                    //{
                    //    Pro_Level = "77",
                    //    Pro_Type = "1",
                    //    Pro_part = "3",
                    //    Pro_Id = "7713",
                    //    Pro_Name = "纏尾蛟革巧匠手套"
                    //}
                    //, new Product
                    //{
                    //    Pro_Level = "77",
                    //    Pro_Type = "1",
                    //    Pro_part = "5",
                    //    Pro_Id = "7115",
                    //    Pro_Name = "纏尾蛟革巧匠工作靴"
                    //}
                    //#endregion

                    //#region 大地
                    //, new Product
                    //{
                    //    Pro_Level = "77",
                    //    Pro_Type = "2",
                    //    Pro_part = "2",
                    //    Pro_Id = "7722",
                    //    Pro_Name = "纏尾蛟革大地長袍"
                    //}
                    //, new Product
                    //{
                    //    Pro_Level = "77",
                    //    Pro_Type = "2",
                    //    Pro_part = "3",
                    //    Pro_Id = "7723",
                    //    Pro_Name = "纏尾蛟革大地手套"
                    //}
                    //, new Product
                    //{
                    //    Pro_Level = "77",
                    //    Pro_Type = "2",
                    //    Pro_part = "5",
                    //    Pro_Id = "7725",
                    //    Pro_Name = "纏尾蛟革大地工作靴"
                    //}
                    //#endregion

                    #endregion

                    #region 80等 矮人棉

                    //#region 巧匠
                    //, new Product
                    //{
                    //    Pro_Level = "80",
                    //    Pro_Type = "1",
                    //    Pro_part = "1",
                    //    Pro_Id = "8011",
                    //    Pro_Name = "矮人棉貝雷帽"
                    //}
                    //, new Product
                    //{
                    //    Pro_Level = "80",
                    //    Pro_Type = "1",
                    //    Pro_part = "2",
                    //    Pro_Id = "8012",
                    //    Pro_Name = "矮人棉外套"
                    //}
                    //, new Product
                    //{
                    //    Pro_Level = "80",
                    //    Pro_Type = "1",
                    //    Pro_part = "4",
                    //    Pro_Id = "8014",
                    //    Pro_Name = "矮人棉軟甲褲"
                    //}
                    //#endregion

                    //#region 大地
                    //, new Product
                    //{
                    //    Pro_Level = "80",
                    //    Pro_Type = "2",
                    //    Pro_part = "1",
                    //    Pro_Id = "8021",
                    //    Pro_Name = "矮人棉頭帶"
                    //}
                    //, new Product
                    //{
                    //    Pro_Level = "80",
                    //    Pro_Type = "2",
                    //    Pro_part = "4",
                    //    Pro_Id = "8024",
                    //    Pro_Name = "矮人棉馬褲"
                    //}
                    //#endregion

                    #endregion
                    );
                    #endregion

                    #region Item檔 初始資料
                    context.Item.AddRange(
                    new Item
                    { 
                        Mtr_id = "C0001",
                        Mtr_type = "C",
                        Mtr_Name = "白麻布"
                    }
                    ,new Item
                    { 
                        Mtr_id = "C0002",
                        Mtr_type = "C",
                        Mtr_Name = "玉綢"
                    }
                    ,new Item
                    {
                        Mtr_id = "W0001",
                        Mtr_type = "W",
                        Mtr_Name = "白麻線"
                    }
                    ,new Item
                    {
                        Mtr_id = "N0001",
                        Mtr_type = "N",
                        Mtr_Name = "石金塊"
                    }
                    ,new Item
                    { 
                        Mtr_id = "L0001",
                        Mtr_type = "L",
                        Mtr_Name = "斯劍虎革"
                    }
                    ,new Item
                    { 
                        Mtr_id = "O0001",
                        Mtr_type = "O",
                        Mtr_Name = "橡膠"
                    }

                        );
                    #endregion

                    #region BOM檔 初始資料
                    context.BOM.AddRange(
                    //白麻巧匠包頭巾
                    new BOM
                    {
                        Pro_Id = "7111",
                        Mtr_id = "C0001",
                        Use_QTY = 2
                    }
                    , new BOM
                    { 
                        Pro_Id = "7111",
                        Mtr_id = "W0001",
                        Use_QTY = 1
                    }
                    , new BOM
                    {
                        Pro_Id = "7111",
                        Mtr_id = "N0001",
                        Use_QTY = 1
                    }
                    ///白麻巧匠工作服
                     , new BOM
                    {
                        Pro_Id = "7112",
                        Mtr_id = "C0001",
                        Use_QTY = 4
                     }
                    , new BOM
                    {
                        Pro_Id = "7112",
                        Mtr_id = "L0001",
                        Use_QTY = 1
                    }
                    , new BOM
                    { 
                        Pro_Id = "7112",
                        Mtr_id = "C0002",
                        Use_QTY = 1
                    }
                    , new BOM
                    {
                        Pro_Id = "7112",
                        Mtr_id = "W0001",
                        Use_QTY = 1
                    }

                        );
                    #endregion


                    context.SaveChanges();
                }
            }
        }
    }
}