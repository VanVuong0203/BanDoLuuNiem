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

namespace QuaLuuNiem.Pages.HoaDons
{
    public class EditModel : PageModel
    {
        private readonly QuaLuuNiem.Data.QuaLuuNiemContext _context;

        public EditModel(QuaLuuNiem.Data.QuaLuuNiemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public HoaDon HoaDon { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoadon =  await _context.HoaDons.FirstOrDefaultAsync(m => m.ID == id);
            if (hoadon == null)
            {
                return NotFound();
            }
            HoaDon = hoadon;
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

            _context.Attach(HoaDon).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HoaDonExists(HoaDon.ID))
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

        private bool HoaDonExists(int id)
        {
            return _context.HoaDons.Any(e => e.ID == id);
        }
    }
}
