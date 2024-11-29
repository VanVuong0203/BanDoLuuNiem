using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuaLuuNiem.Model
{
    public class CTHD
    {
        public int ID { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        [Display(Name = "Mã hóa đơn")]
        public string MaHD {  get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        [Display(Name = "Mã sản phẩm")]
        public string MaSP {  get; set; }

        [Display(Name = "Tên sản phẩm")]
        public string TenSP {  get; set; }

        [Display(Name = "Số lượng")]
        public int SoLuong {  get; set; }

        [Display(Name = "Giá")]
        public int Gia {  get; set; }
        public ICollection<HoaDon> HoaDons { get; set; }
    }
}
