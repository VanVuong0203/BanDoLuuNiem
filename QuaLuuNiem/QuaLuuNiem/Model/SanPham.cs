using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuaLuuNiem.Model
{
    public class SanPham
    {
        
        public int ID {  get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string MaSP {  get; set; }

        [Display(Name = "Tên sản phẩm")]
        public string TenSP {  get; set; }

        [Display(Name = "Hình ảnh")]
        public string HinhAnh { get; set; }

        [Display(Name = "Số lượng")]
        public int SoLuong {  get; set; }

        [Display(Name = "Giá")]
        public int Gia {  get; set; }
        [Display(Name = "Loại")]
        public string Loai {  get; set; }
        [Display(Name = "Mô tả")]
        public string MoTa {  get; set; }
    }
}