using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuaLuuNiem.Data;
using QuaLuuNiem.Model;

namespace QuaLuuNiem.Pages.Login
{
    public class DoiMatKhauModel : PageModel
    {
        private readonly QuaLuuNiemContext _context;

        public DoiMatKhauModel(QuaLuuNiemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string CurrentPassword { get; set; }
        [BindProperty]
        public string NewPassword { get; set; }
        [BindProperty]
        public string ConfirmNewPassword { get; set; }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return RedirectToPage("/Login/Dangnhap");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return RedirectToPage("/Login/Dangnhap");
            }

            var userMaKH = HttpContext.Session.GetString("UserMaKH");
            var user = _context.ThongTinKhachHangs.FirstOrDefault(u => u.MaKH == userMaKH);

            if (user == null)
            {
                ViewData["ErrorMessage"] = "Kh�ng t?m th?y ng�?i d�ng.";
                return Page();
            }

            if (user.MatKhau != CurrentPassword)
            {
                ViewData["ErrorMessage"] = "M?t kh?u hi?n t?i kh�ng ��ng.";
                return Page();
            }

            if (NewPassword != ConfirmNewPassword)
            {
                ViewData["ErrorMessage"] = "M?t kh?u m?i v� m?t kh?u x�c nh?n kh�ng kh?p.";
                return Page();
            }

            user.MatKhau = NewPassword;
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "�?i m?t kh?u th�nh c�ng!";
            return RedirectToPage("/SanPhams/Index");
        }
    }
}
