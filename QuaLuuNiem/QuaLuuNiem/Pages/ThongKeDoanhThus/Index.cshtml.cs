using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuaLuuNiem.Data;
using QuaLuuNiem.Model;

namespace QuaLuuNiem.Pages.ThongKeDoanhThus
{
    public class IndexModel : PageModel
    {
        private readonly QuaLuuNiemContext _context;

        public IndexModel(QuaLuuNiemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public IList<ThongKeDoanhThu> ThongKeDoanhThu { get; set; }

        public async Task OnGetAsync()
        {
            ThongKeDoanhThu = await _context.ThongKeDoanhThus
                .OrderBy(t => t.Date)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // X�a d? li?u c? trong ThongKeDoanhThus (n?u c�)
            await ClearThongKeDoanhThuAsync();

            // Th?c hi?n th?ng k� t? d? li?u trong HoaDon
            var groupedData = await _context.HoaDons
                .GroupBy(hd => hd.NgayBan.Date) // L?y ng�y t? tr�?ng Date trong HoaDon
                .ToListAsync();

            foreach (var group in groupedData)
            {
                var existingRecord = await _context.ThongKeDoanhThus
                    .FirstOrDefaultAsync(t => t.Date == group.Key);

                if (existingRecord == null)
                {
                    // N?u ch�a c� b?n ghi, th�m m?i
                    var thongKeDoanhThu = new ThongKeDoanhThu
                    {
                        Date = group.Key,
                        TotalRevenue = group.Sum(hd => hd.TongTien),
                        Amount = group.Count()
                    };

                    _context.ThongKeDoanhThus.Add(thongKeDoanhThu);
                }
                else
                {
                    // N?u �? c� b?n ghi, c?p nh?t l?i TotalRevenue v� Amount
                    existingRecord.TotalRevenue = group.Sum(hd => hd.TongTien);
                    existingRecord.Amount = group.Count();
                }
            }

            await _context.SaveChangesAsync();

            // Reload l?i d? li?u �? hi?n th? tr�n giao di?n
            ThongKeDoanhThu = await _context.ThongKeDoanhThus
                .OrderBy(t => t.Date)
                .ToListAsync();

            return RedirectToPage();
        }

        private async Task ClearThongKeDoanhThuAsync()
        {
            var existingRecords = await _context.ThongKeDoanhThus.ToListAsync();
            _context.ThongKeDoanhThus.RemoveRange(existingRecords);
            await _context.SaveChangesAsync();
        }
    }
}
