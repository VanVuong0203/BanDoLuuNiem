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
                ViewData["ErrorMessage"] = "Không t?m th?y ngý?i dùng.";
                return Page();
            }

            if (user.MatKhau != CurrentPassword)
            {
                ViewData["ErrorMessage"] = "M?t kh?u hi?n t?i không ðúng.";
                return Page();
            }

            if (NewPassword != ConfirmNewPassword)
            {
                ViewData["ErrorMessage"] = "M?t kh?u m?i và m?t kh?u xác nh?n không kh?p.";
                return Page();
            }

            user.MatKhau = NewPassword;
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Ð?i m?t kh?u thành công!";
            return RedirectToPage("/SanPhams/Index");
        }
    }
}
