using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuaLuuNiem.Model
{
    public class ThongTinGioHang
    {
        public int ID { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        
        public string MaKH {  get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        
        public string MaSP {  get; set; }
        public string TenSP {  get; set; }
        public int SoLuong {  get; set; }
        public int Gia {  get; set; }

        public string HinhAnh {  get; set; }
        public ICollection<ThongTinKhachHang> ThongTinKhachHangs { get; set; }
        public ICollection<SanPham> SanPhams { get; set; }
    }
}
