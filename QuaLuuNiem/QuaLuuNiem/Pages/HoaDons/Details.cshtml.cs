using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuaLuuNiem.Data;
using QuaLuuNiem.Model;

namespace QuaLuuNiem.Pages.HoaDons
{
    public class DetailsModel : PageModel
    {
        private readonly QuaLuuNiemContext _context;

        public DetailsModel(QuaLuuNiemContext context)
        {
            _context = context;
        }

        public HoaDon HoaDon { get; set; }
        public ThongTinKhachHang KhachHang { get; set; }
        public IList<CTHD> ChiTietHoaDon { get; set; }
        public IList<SanPham> SanPham { get; set; }

        public async Task<IActionResult> OnGetDetailsAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            HoaDon = await _context.HoaDons.FirstOrDefaultAsync(m => m.MaHD == id);

            if (HoaDon == null)
            {
                return NotFound();
            }

            KhachHang = await _context.ThongTinKhachHangs.FirstOrDefaultAsync(h => h.MaKH == HoaDon.MaKH);
            ChiTietHoaDon = await _context.CTHDs.Where(c => c.MaHD == HoaDon.MaHD).ToListAsync();
            var maSPs = ChiTietHoaDon.Select(c => c.MaSP).ToList();
            SanPham = await _context.SanPham.Where(sp => maSPs.Contains(sp.MaSP)).ToListAsync();

            return Page();
        }
    }
}
