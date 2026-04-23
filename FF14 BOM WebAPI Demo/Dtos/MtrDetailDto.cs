namespace FF14BOM.Dtos
{
    public class MtrDetailDto
    {
        public string Mtr_Name { get; set; } // 來自 Item.Mtr_Name
        public int Use_QTY { get; set; }        // 來自 BOM.Use_QTY
    }

    public class MtrDetailIdDto
    {
        public string Mtr_Id { get; set; } // 來自 Item.Mtr_Name
        public int Use_QTY { get; set; }        // 來自 BOM.Use_QTY
    }
}
