namespace NetCoreStudy3_DTO.Models
{
    public class Product
    {
        public string Pro_Level { get; set; }    //裝備等級 採16進制 100等 → A0

        public string Pro_Type { get; set; }    //裝備職業別 1巧匠2大地

        public string Pro_part { get; set; }   //裝備部位  頭1身體2身體3手4褲5鞋6

        public string Pro_Name { get; set; }   //裝備名稱

        public string Pro_Id { get; set; }     //產品編號 用來找BOM用

        //依裝備等級+裝備部位搜尋 
        //ex 白麻巧匠包頭巾→711 斯劍虎革巧匠手套→714




    }
}
