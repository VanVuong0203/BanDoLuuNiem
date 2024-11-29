using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using QuaLuuNiem.Data;
using QuaLuuNiem.Model;

namespace QuaLuuNiem.Pages.GioHangs
{
    public class DetailsModel : PageModel
    {
        private readonly QuaLuuNiem.Data.QuaLuuNiemContext _context;

        public DetailsModel(QuaLuuNiem.Data.QuaLuuNiemContext context)
        {
            _context = context;
        }

        public List<ThongTinGioHang> SelectedItems { get; set; } = new List<ThongTinGioHang>();
        public decimal TotalPrice { get; set; }
        public string userName { get; set; }
        public string userID { get; set; }
        public string userSDT { get; set; }
        public string userDC { get; set; }
        public SanPham SanPham { get; set; }

        public async Task OnGetAsync(string selectedIds)
        {
            if (string.IsNullOrEmpty(selectedIds))
            {
                // No selected IDs
                return;
            }
            userName = HttpContext.Session.GetString("UserName");
            userID = HttpContext.Session.GetString("UserMaKH");
            userSDT = HttpContext.Session.GetString("UserSDT");
            userDC = HttpContext.Session.GetString("UserDC");

            var ids = selectedIds.Split(',').Select(int.Parse).ToList();

            SelectedItems = await _context.GioHangs
                        .Where(item => ids.Contains(item.ID))
                        .ToListAsync();

            TotalPrice = SelectedItems.Sum(item => item.Gia * item.SoLuong);
            var SoLuongHD = await _context.HoaDons.CountAsync();

            var newCartItem = new HoaDon
            {
                MaKH = userID,
                MaHD = "HD" + (SoLuongHD + 1),
                TongTien = (int)TotalPrice,
                NgayBan = DateTime.Now,
            };

            _context.HoaDons.Add(newCartItem);
            await _context.SaveChangesAsync();

            foreach (var item in SelectedItems)
            {
                // Tạo chi tiết hóa đơn cho từng sản phẩm trong giỏ hàng
                var chiTietHoaDon = new CTHD
                {
                    MaHD = "HD" + (SoLuongHD + 1),
                    MaSP = item.MaSP,
                    SoLuong = item.SoLuong,
                    Gia = item.Gia
                };

                // Thêm vào danh sách chi tiết hóa đơn của hóa đơn mới
                _context.CTHDs.Add(chiTietHoaDon);
                await _context.SaveChangesAsync();
            }
            
        }
    }
}
