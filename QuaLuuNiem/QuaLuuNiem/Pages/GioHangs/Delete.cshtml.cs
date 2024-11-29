using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuaLuuNiem.Data;
using QuaLuuNiem.Model;

namespace QuaLuuNiem.Pages.GioHangs
{
    public class DeleteModel : PageModel
    {
        private readonly QuaLuuNiem.Data.QuaLuuNiemContext _context;

        public DeleteModel(QuaLuuNiem.Data.QuaLuuNiemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ThongTinGioHang ThongTinGioHang { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thongtingiohang = await _context.GioHangs.FirstOrDefaultAsync(m => m.ID == id);

            if (thongtingiohang == null)
            {
                return NotFound();
            }
            else
            {
                ThongTinGioHang = thongtingiohang;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thongtingiohang = await _context.GioHangs.FindAsync(id);
            if (thongtingiohang != null)
            {
                ThongTinGioHang = thongtingiohang;
                _context.GioHangs.Remove(ThongTinGioHang);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
