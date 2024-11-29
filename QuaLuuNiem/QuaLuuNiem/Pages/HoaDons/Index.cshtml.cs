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
    public class IndexModel : PageModel
    {
        private readonly QuaLuuNiemContext _context;

        public IndexModel(QuaLuuNiemContext context)
        {
            _context = context;
        }

        public IList<HoaDon> HoaDon { get; set; }
        public ThongTinKhachHang KhachHang { get; set; }
        public IList<CTHD> ChiTietHoaDon { get; set; }
        public IList<SanPham> SanPham { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                HoaDon = await _context.HoaDons.ToListAsync();
            }
            else
            {
                HoaDon = await _context.HoaDons.Where(h => h.MaHD == id).ToListAsync();

                if (HoaDon == null || !HoaDon.Any())
                {
                    return Page();
                }

                var hoaDon = HoaDon.FirstOrDefault();
                if (hoaDon != null)
                {
                    KhachHang = await _context.ThongTinKhachHangs.FirstOrDefaultAsync(h => h.MaKH == hoaDon.MaKH);
                    ChiTietHoaDon = await _context.CTHDs.Where(c => c.MaHD == hoaDon.MaHD).ToListAsync();
                    var maSPs = ChiTietHoaDon.Select(c => c.MaSP).ToList();
                    SanPham = await _context.SanPham
                        .Where(sp => maSPs.Contains(sp.MaSP))
                        .ToListAsync();
                }
            }

            return Page();
        }
    }
}
