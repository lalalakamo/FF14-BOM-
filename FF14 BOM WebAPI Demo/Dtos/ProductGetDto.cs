namespace FF14BOM.Dtos
{
    public class ProductGetDto
    {
        public string Pro_Level { get; set; }    //裝備等級 採16進制 100等 → A0

        public string Pro_Type { get; set; }    //裝備職業別 1巧匠2大地

        public string Pro_part { get; set; }   //裝備部位  頭1身體2身體3手4褲5鞋6

        public string Pro_Name { get; set; }   //裝備名稱

        public string Pro_Id { get; set; }     //產品編號 用來找BOM用
    }
    public class ProductAddDto
    {
        public string Pro_Level { get; set; }    //裝備等級 採16進制 100等 → A0

        public string Pro_Type { get; set; }    //裝備職業別 1巧匠2大地

        public string Pro_part { get; set; }   //裝備部位  頭1身體2身體3手4褲5鞋6

        public string Pro_Name { get; set; }   //裝備名稱

        public string? Pro_Id { get; set; }     //產品編號 用來找BOM用
    }
}
