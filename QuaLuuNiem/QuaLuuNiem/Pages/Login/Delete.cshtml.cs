using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuaLuuNiem.Data;
using QuaLuuNiem.Model;

namespace QuaLuuNiem.Pages.Login
{
    public class DeleteModel : PageModel
    {
        private readonly QuaLuuNiem.Data.QuaLuuNiemContext _context;

        public DeleteModel(QuaLuuNiem.Data.QuaLuuNiemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ThongTinKhachHang ThongTinKhachHangs { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thongtinkhachhang = await _context.ThongTinKhachHangs.FirstOrDefaultAsync(m => m.ID == id);

            if (thongtinkhachhang == null)
            {
                return NotFound();
            }
            else
            {
                ThongTinKhachHangs = thongtinkhachhang;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thongtinkhachhang = await _context.ThongTinKhachHangs.FindAsync(id);
            if (thongtinkhachhang != null)
            {
                ThongTinKhachHangs = thongtinkhachhang;
                _context.ThongTinKhachHangs.Remove(ThongTinKhachHangs);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./QLUser");
        }
    }
}
