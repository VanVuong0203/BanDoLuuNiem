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
            // Xóa d? li?u c? trong ThongKeDoanhThus (n?u có)
            await ClearThongKeDoanhThuAsync();

            // Th?c hi?n th?ng kê t? d? li?u trong HoaDon
            var groupedData = await _context.HoaDons
                .GroupBy(hd => hd.NgayBan.Date) // L?y ngày t? trý?ng Date trong HoaDon
                .ToListAsync();

            foreach (var group in groupedData)
            {
                var existingRecord = await _context.ThongKeDoanhThus
                    .FirstOrDefaultAsync(t => t.Date == group.Key);

                if (existingRecord == null)
                {
                    // N?u chýa có b?n ghi, thêm m?i
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
                    // N?u ð? có b?n ghi, c?p nh?t l?i TotalRevenue và Amount
                    existingRecord.TotalRevenue = group.Sum(hd => hd.TongTien);
                    existingRecord.Amount = group.Count();
                }
            }

            await _context.SaveChangesAsync();

            // Reload l?i d? li?u ð? hi?n th? trên giao di?n
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
