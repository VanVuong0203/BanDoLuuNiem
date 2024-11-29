using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuaLuuNiem.Data;
using QuaLuuNiem.Model;

namespace QuaLuuNiem.Pages.Login
{
    public class DangnhapModel : PageModel
    {
        private readonly QuaLuuNiemContext _context;

        public DangnhapModel(QuaLuuNiemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ThongTinKhachHang Logins { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            // Kiểm tra null trước khi thực hiện truy vấn
            if (!string.IsNullOrEmpty(Logins.TenDangNhap) && !string.IsNullOrEmpty(Logins.MatKhau))
            {
                var user = _context.ThongTinKhachHangs
                    .SingleOrDefault(u => u.TenDangNhap == Logins.TenDangNhap && u.MatKhau == Logins.MatKhau);

                if (user != null)
                {
                    HttpContext.Session.SetString("UserMaKH", user.MaKH);
                    HttpContext.Session.SetString("UserName", user.HoTen);

                    // Kiểm tra quyền admin
                    if (user.TenDangNhap == "Admin0" && user.MatKhau == "Admin0")
                    {
                        HttpContext.Session.SetString("IsAdmin", "true");
                    }
                    else
                    {
                        HttpContext.Session.SetString("IsAdmin", "false");
                    }

                    // Nếu đăng nhập thành công
                    return RedirectToPage("/SanPhams/Index");
                }
                else
                {
                    // Nếu đăng nhập thất bại
                    ViewData["ErrorMessage"] = "Tên đăng nhập hoặc mật khẩu không đúng.";
                }
            }
            else
            {
                ViewData["ErrorMessage"] = "Tên đăng nhập và mật khẩu không được để trống.";
            }
            return Page();
        }
    }
}
