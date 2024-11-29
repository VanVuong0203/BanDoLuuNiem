using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuaLuuNiem.Data;
using QuaLuuNiem.Model;

namespace QuaLuuNiem.Pages.SanPhams
{
    public class DeleteModel : PageModel
    {
        private readonly QuaLuuNiem.Data.QuaLuuNiemContext _context;

        public DeleteModel(QuaLuuNiem.Data.QuaLuuNiemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public SanPham SanPham { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanpham = await _context.SanPham.FirstOrDefaultAsync(m => m.ID == id);

            if (sanpham == null)
            {
                return NotFound();
            }
            else
            {
                SanPham = sanpham;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanpham = await _context.SanPham.FindAsync(id);
            if (sanpham != null)
            {
                SanPham = sanpham;
                _context.SanPham.Remove(SanPham);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
