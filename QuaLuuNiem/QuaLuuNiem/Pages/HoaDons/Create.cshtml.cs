using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using QuaLuuNiem.Data;
using QuaLuuNiem.Model;

namespace QuaLuuNiem.Pages.HoaDons
{
    public class CreateModel : PageModel
    {
        private readonly QuaLuuNiem.Data.QuaLuuNiemContext _context;

        public CreateModel(QuaLuuNiem.Data.QuaLuuNiemContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public HoaDon HoaDon { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.HoaDons.Add(HoaDon);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
