using System.Text.Json.Serialization;

namespace FF14BOM.Dtos
{
    public class BOMGetDto 
    {
        public string Pro_Name { get; set; }   //裝備名稱
        public string Pro_Id { get; set; }     //產品編號 用來找BOM用
        public List<MtrDetailDto> Materials { get; set; }
    }

    public class BOMGetListDto //輸入查詢編號後，由+拆開來的產品所需料號總和List
    {
        public string Mtr_Name { get; set; }    //材料名稱
        public int Use_QTY { get; set; }        //使用量
    }

    public class BOMAddDto 
    {
        public string Pro_Name { get; set; }   //裝備名稱
        public string Pro_Id { get; set; }     //產品編號 用來找BOM用
        public List<MtrDetailIdDto> MtrDetailId { get; set; }
    }

    public class MtrDetailDto
    {
        public string Mtr_Name { get; set; } // 來自 Item.Mtr_Name
        public string Mtr_id { get; set; } // 來自 Item.Mtr_Id
        public int Use_QTY { get; set; }        // 來自 BOM.Use_QTY
    }

    public class MtrDetailIdDto
    {
        public string Mtr_Id { get; set; } // 來自 Item.Mtr_Id
        public int Use_QTY { get; set; }        // 來自 BOM.Use_QTY
    }
}
