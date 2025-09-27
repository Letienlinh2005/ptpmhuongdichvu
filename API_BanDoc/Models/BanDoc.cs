namespace MyWebAPI.Models
{
    public class BanDoc
    {
        public string maBanDoc { get; set; } = string.Empty;

        public string hoTen { get; set; } = string.Empty;

        public string soThe { get; set; } 

        public string email { get; set; }

        public string dienThoai { get; set; }
        public DateOnly hanThe { get; set; }

        public string trangThaiThe { get; set; }
        public decimal duNo { get; set; }

    }
}
