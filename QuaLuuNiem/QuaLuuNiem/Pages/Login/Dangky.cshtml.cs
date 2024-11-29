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

namespace QuaLuuNiem.Pages.Login
{
    public class DangkyModel : PageModel
    {
        private readonly QuaLuuNiem.Data.QuaLuuNiemContext _context;

        public DangkyModel(QuaLuuNiem.Data.QuaLuuNiemContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ThongTinKhachHang ThongTinKhachHang { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var SoLuong = await _context.ThongTinKhachHangs.CountAsync();

            ThongTinKhachHang.MaKH = "KH" + (SoLuong + 1).ToString();

            _context.ThongTinKhachHangs.Add(ThongTinKhachHang);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Login/Dangnhap");
        }
    }
}
