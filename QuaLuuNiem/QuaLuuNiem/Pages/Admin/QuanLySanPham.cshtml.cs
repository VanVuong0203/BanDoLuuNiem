using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuaLuuNiem.Data;
using QuaLuuNiem.Model;

namespace QuaLuuNiem.Pages.Admin
{
    public class QuanLySanPhamModel : PageModel
    {
        private readonly QuaLuuNiemContext _context;

        public QuanLySanPhamModel(QuaLuuNiemContext context)
        {
            _context = context;
        }

        public IList<SanPham> SanPham { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
            {
                return RedirectToPage("/Login/Dangnhap");
            }

            SanPham = await _context.SanPham.ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var sanPham = await _context.SanPham.FindAsync(id);

            if (sanPham != null)
            {
                _context.SanPham.Remove(sanPham);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}
