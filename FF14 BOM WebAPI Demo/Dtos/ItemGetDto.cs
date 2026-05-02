namespace FF14BOM.Dtos
{
    public class ItemGetDto
    {
         public string Mtr_Name { get; set; }    //材料名稱
         public string Mtr_type { get; set; }    //材料類別
         public string Mtr_id { get; set; }      //材料編號
         public bool NPC_Sell { get; set; }    //NPC販售
    }
    public class ItemAddDto
    {
        public string Mtr_Name { get; set; } = " ";    //材料名稱
        public string Mtr_type { get; set; }             //材料類別
        public string? Mtr_id { get; set; }              //材料編號
        public bool NPC_Sell { get; set; } = false;     //NPC販售
    }
}
