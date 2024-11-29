using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuaLuuNiem.Model
{
    public class HoaDon
    {
        public int ID { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        [Display(Name = "Mã khách hàng")]
        public string MaKH {  get; set; }

        [Display(Name = "Mã hóa đơn")]
        public string MaHD {  get; set; }

        [Display(Name = "Ngày bán")]
        public DateTime NgayBan {  get; set; }

        [Display(Name = "Tổng tiền")]
        public int TongTien {  get; set; }

        public ICollection<ThongTinKhachHang> ThongTinKhachHang { get; set; }
        public ICollection<CTHD> CTHDs { get; set; }

    }
}
