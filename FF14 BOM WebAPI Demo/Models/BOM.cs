using System.ComponentModel.DataAnnotations.Schema;

namespace FF14BOM.Models
{
    public class BOM
    {
        public string Pro_Id { get; set; }  //產品編號

        [ForeignKey("Item")]
        public string Mtr_id { get; set; }  //材料編號

        public int Use_QTY { get; set; }    //使用數量

        public virtual Product Product { get; set; }
        public virtual Item Item { get; set; }
    }
}
