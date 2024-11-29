using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuaLuuNiem.Data;
using QuaLuuNiem.Model;

namespace QuaLuuNiem.Pages.GioHangs
{
    public class IndexModel : PageModel
    {
        private readonly QuaLuuNiemContext _context;

        public IndexModel(QuaLuuNiemContext context)
        {
            _context = context;
        }

        public IList<ThongTinGioHang> ThongTinGioHang { get; set; }
        public IList<HoaDon> HoaDon { get; set; }
        public IList<CTHD> CTHD { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = HttpContext.Session.GetString("UserMaKH");

            if (userId == null)
            {
                // Nếu không có người dùng đăng nhập, chuyển hướng đến trang đăng nhập
                return RedirectToPage("/Login/Dangnhap");
            }

            // Lấy thông tin giỏ hàng của người dùng đã đăng nhập
            ThongTinGioHang = await _context.GioHangs.Where(g => g.MaKH == userId.ToString()).ToListAsync();
            if (ThongTinGioHang == null)
            {
                ThongTinGioHang = new List<ThongTinGioHang>(); // Khởi tạo danh sách trống nếu không có dữ liệu
            }
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var thongtingiohang = await _context.GioHangs.FindAsync(id);

            if (thongtingiohang != null)
            {
                _context.GioHangs.Remove(thongtingiohang);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }

        [HttpPost]
        public async Task<IActionResult> OnPostMuaHangAsync(int productId, int quantity)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("./Index");
            }

            var userId = HttpContext.Session.GetString("UserMaKH");

            if (userId == null)
            {
                return RedirectToPage("/Login/Dangnhap");
            }

            var product = await _context.SanPham.FindAsync(productId);
            if (product == null)
            {
                return NotFound();
            }

            var SoLuongHD = await _context.HoaDons.CountAsync();

            // Tạo một hóa đơn mới
            var hoaDon = new HoaDon
            {
                MaHD = "HD" + (SoLuongHD + 1),
                MaKH = userId,
                NgayBan = DateTime.Now,
                TongTien = product.Gia * quantity
            };

            _context.HoaDons.Add(hoaDon);
            await _context.SaveChangesAsync();

            // Tạo một chi tiết hóa đơn
            var chiTietHoaDon = new CTHD
            {
                MaHD = "HD" + (SoLuongHD + 1),
                MaSP = product.MaSP,
                SoLuong = quantity,
                Gia = product.Gia,
                TenSP = product.TenSP
            };

            _context.CTHDs.Add(chiTietHoaDon);
            await _context.SaveChangesAsync();

            // Redirect to the invoice details page
            return RedirectToPage("/HoaDons/Index", new { id = hoaDon.MaHD });
        }


    }
}