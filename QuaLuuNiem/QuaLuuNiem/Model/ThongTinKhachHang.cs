using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuaLuuNiem.Model
{
    public class ThongTinKhachHang
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string MaKH { get; set; }

        [Required(ErrorMessage = "Họ tên là bắt buộc")]
        [Display(Name = "Họ và tên")]
        public string HoTen { get; set; }

        [Required(ErrorMessage = "Địa chỉ là bắt buộc")]
        [Display(Name = "Địa chỉ")]
        public string DiaChi { get; set; }

        [Required(ErrorMessage = "Số điện thoại là bắt buộc")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Số điện thoại phải chứa chính xác 10 chữ số")]
        [Display(Name = "Số điện thoại")]
        public string SDT { get; set; }

        [Required(ErrorMessage = "Tên đăng nhập là bắt buộc")]
        [MinLength(5, ErrorMessage = "Tên đăng nhập phải có ít nhất 6 ký tự")]
        [RegularExpression(@"^(?=.*[a-zA-Z])(?=.*\d)[a-zA-Z\d]{6,}$", ErrorMessage = "Tên đăng nhập phải chứa ít nhất 1 ký tự chữ và 1 ký tự số")]
        [Display(Name = "Tên đăng nhập")]
        public string TenDangNhap { get; set; }

        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        [MinLength(6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự")]
        [Display(Name = "Mật khẩu")]
        public string MatKhau { get; set; }

        public ICollection<HoaDon> HoaDons { get; set; }
    }
}
