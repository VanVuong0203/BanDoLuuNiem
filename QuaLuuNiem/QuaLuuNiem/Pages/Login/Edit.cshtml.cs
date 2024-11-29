using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuaLuuNiem.Data;
using QuaLuuNiem.Model;

namespace QuaLuuNiem.Pages.Login
{
    public class EditModel : PageModel
    {
        private readonly QuaLuuNiem.Data.QuaLuuNiemContext _context;

        public EditModel(QuaLuuNiem.Data.QuaLuuNiemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ThongTinKhachHang ThongTinKhachHang { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thongtinkhachhang = await _context.ThongTinKhachHangs.FirstOrDefaultAsync(m => m.ID == id);
            if (thongtinkhachhang == null)
            {
                return NotFound();
            }
            ThongTinKhachHang = thongtinkhachhang;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var thongTinKhachHangToUpdate = await _context.ThongTinKhachHangs.FindAsync(id);

            if (thongTinKhachHangToUpdate == null)
            {
                return NotFound();
            }

            // Cập nhật các giá trị của thực thể
            thongTinKhachHangToUpdate.MaKH = ThongTinKhachHang.MaKH;
            thongTinKhachHangToUpdate.HoTen = ThongTinKhachHang.HoTen;
            thongTinKhachHangToUpdate.DiaChi = ThongTinKhachHang.DiaChi;
            thongTinKhachHangToUpdate.SDT = ThongTinKhachHang.SDT;
            thongTinKhachHangToUpdate.TenDangNhap = ThongTinKhachHang.TenDangNhap;
            thongTinKhachHangToUpdate.MatKhau = ThongTinKhachHang.MatKhau;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ThongTinKhachHangExists(ThongTinKhachHang.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("/Admin/QuanLyThongTinKhachHang");
        }


        private bool ThongTinKhachHangExists(int id)
        {
            return _context.ThongTinKhachHangs.Any(e => e.ID == id);
        }
    }
}
