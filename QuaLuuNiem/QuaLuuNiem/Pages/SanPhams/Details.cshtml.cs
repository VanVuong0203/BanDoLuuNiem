using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuaLuuNiem.Data;
using QuaLuuNiem.Model;

namespace QuaLuuNiem.Pages.SanPhams
{
    public class DetailsModel : PageModel
    {
        private readonly QuaLuuNiem.Data.QuaLuuNiemContext _context;

        public DetailsModel(QuaLuuNiem.Data.QuaLuuNiemContext context)
        {
            _context = context;
        }

        public SanPham SanPham { get; set; } = default!;
        public HoaDon HoaDon { get; set; } = default!;
        public CTHD CTHD { get; set; } = default!;
        public string userName { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            userName = HttpContext.Session.GetString("UserName");
            if (id == null)
            {
                return NotFound();
            }

            var sanpham = await _context.SanPham.FirstOrDefaultAsync(m => m.ID == id);
            if (sanpham == null)
            {
                return NotFound();
            }
            else
            {
                SanPham = sanpham;
            }
            return Page();
        }

        [BindProperty]
        public int ProductId { get; set; }


        
        [HttpPost]
        public async Task<IActionResult> OnPostAddToCartAsync(string MSP, int productId, int quantity)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("./Index");
            }

            var userId = HttpContext.Session.GetString("UserMaKH");
            userName = HttpContext.Session.GetString("UserName");

            if (userId == null)
            {
                return RedirectToPage("/Login/Dangnhap");
            }

            var cartItem = await _context.GioHangs.SingleOrDefaultAsync(ci => ci.MaSP == MSP && ci.MaKH == userId);

            if (cartItem == null)
            {
                var product = await _context.SanPham.FindAsync(productId);
                if (product == null)
                {
                    return NotFound();
                }

                var newCartItem = new ThongTinGioHang
                {
                    MaSP = product.MaSP,
                    TenSP = product.TenSP,
                    Gia = product.Gia,
                    SoLuong = quantity,
                    HinhAnh = product.HinhAnh,
                    MaKH = userId
                };

                _context.GioHangs.Add(newCartItem);
            }
            else
            {
                cartItem.SoLuong += quantity;
            }

            await _context.SaveChangesAsync();

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
            userName = HttpContext.Session.GetString("UserName");

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

            var hoaDon = new HoaDon
            {
                MaHD = "HD" + (SoLuongHD + 1),
                MaKH = userId,
                NgayBan = DateTime.Now,
                TongTien = product.Gia * quantity
            };

            _context.HoaDons.Add(hoaDon);
            await _context.SaveChangesAsync();

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

            return RedirectToPage("/HoaDons/Index", new { id = hoaDon.MaHD });
        }




        /*
        public async Task<IActionResult> OnPostAsync()
        {
            // Lấy thông tin sản phẩm từ cơ sở dữ liệu
            var product = await _context.SanPham.FindAsync(ProductId);
            var userId = HttpContext.Session.GetString("UserMaKH");

            if (product != null)
            {
                // Tạo một đối tượng ThongTinGioHang mới
                var cartItem = new ThongTinGioHang
                {
                    MaKH = userId,
                    MaSP = product.MaSP,
                    TenSP = product.TenSP,
                    SoLuong = 1, // Số lượng ban đầu là 1, bạn có thể thay đổi nếu cần
                    Gia = product.Gia,
                    
                };

                // Thêm đối tượng ThongTinGioHang vào cơ sở dữ liệu
                _context.GioHangs.Add(cartItem);

                    await _context.SaveChangesAsync();

                // Đặt thông báo thành công
                TempData["SuccessMessage"] = "Sản phẩm đã được thêm vào giỏ hàng thành công.";

                // Sau khi thêm vào giỏ hàng, chuyển hướng đến trang giỏ hàng
                return RedirectToPage("/Cart/Index");
            }

            // Đặt thông báo lỗi
            TempData["ErrorMessage"] = "Không thể thêm sản phẩm vào giỏ hàng.";

            // Trở lại trang chi tiết sản phẩm
            return RedirectToPage("/SanPhams/Details", new { id = ProductId });
        }*/

    }
}
