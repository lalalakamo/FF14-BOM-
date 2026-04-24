using System.Text.Json.Serialization;

namespace FF14BOM.Dtos
{
    public class BOMGetDto
    {
        public string Pro_Name { get; set; }
        public string Pro_Id { get; set; }
        public string Pro_Level { get; set; }
        public string Pro_part { get; set; }
        public List<MtrDetailDto> Materials { get; set; }

    }

    public class BOMAddDto
    {
        public string Pro_Name { get; set; }
        public string Pro_Id { get; set; }
        public List<MtrDetailIdDto> MtrDetailId { get; set; }
    }
}
