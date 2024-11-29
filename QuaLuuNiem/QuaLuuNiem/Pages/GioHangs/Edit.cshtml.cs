using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuaLuuNiem.Data;
using QuaLuuNiem.Model;

namespace QuaLuuNiem.Pages.GioHangs
{
    public class EditModel : PageModel
    {
        private readonly QuaLuuNiem.Data.QuaLuuNiemContext _context;

        public EditModel(QuaLuuNiem.Data.QuaLuuNiemContext context)
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

            var thongtingiohang =  await _context.GioHangs.FirstOrDefaultAsync(m => m.ID == id);
            if (thongtingiohang == null)
            {
                return NotFound();
            }
            ThongTinGioHang = thongtingiohang;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ThongTinGioHang).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ThongTinGioHangExists(ThongTinGioHang.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ThongTinGioHangExists(int id)
        {
            return _context.GioHangs.Any(e => e.ID == id);
        }
    }
}
