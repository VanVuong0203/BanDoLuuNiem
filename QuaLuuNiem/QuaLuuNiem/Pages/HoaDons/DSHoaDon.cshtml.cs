using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuaLuuNiem.Data;
using QuaLuuNiem.Model;

namespace QuaLuuNiem.Pages.HoaDons
{
    public class DSHoaDonModel : PageModel
    {
        private readonly QuaLuuNiemContext _context;

        public DSHoaDonModel(QuaLuuNiemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public IList<HoaDon> HoaDon { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            HoaDon = await _context.HoaDons.ToListAsync();
            var userId = HttpContext.Session.GetString("UserMaKH");

            if (userId == null)
            {
                // Nếu không có người dùng đăng nhập, chuyển hướng đến trang đăng nhập
                return RedirectToPage("/Login/Dangnhap");
            }
            

            HoaDon = await _context.HoaDons
                                  .Where(h => h.MaKH == userId)
                                  .ToListAsync();
            if (HoaDon == null)
            {
                HoaDon = new List<HoaDon>(); // Khởi tạo danh sách trống nếu không có dữ liệu
            }
            return Page();
        }
    }
}
