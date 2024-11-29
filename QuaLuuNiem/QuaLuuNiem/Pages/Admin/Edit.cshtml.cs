using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuaLuuNiem.Data;
using QuaLuuNiem.Model;

namespace QuaLuuNiem.Pages.Admin
{
    public class EditModel : PageModel
    {
        private readonly QuaLuuNiemContext _context;

        public EditModel(QuaLuuNiemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ThongTinKhachHang ThongTinKhachHang { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ThongTinKhachHang = await _context.ThongTinKhachHangs.FindAsync(id);

            if (ThongTinKhachHang == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var khachHangToUpdate = await _context.ThongTinKhachHangs.FindAsync(ThongTinKhachHang.ID);

            if (khachHangToUpdate == null)
            {
                return NotFound();
            }

            // Update the properties that are allowed to be changed
            khachHangToUpdate.MaKH = ThongTinKhachHang.MaKH;
            khachHangToUpdate.HoTen = ThongTinKhachHang.HoTen;
            khachHangToUpdate.DiaChi = ThongTinKhachHang.DiaChi;
            khachHangToUpdate.SDT = ThongTinKhachHang.SDT;
            khachHangToUpdate.TenDangNhap = ThongTinKhachHang.TenDangNhap;
            khachHangToUpdate.MatKhau = ThongTinKhachHang.MatKhau;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException)
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

            return RedirectToPage("./QuanLyThongTinKhachHang");
        }

        private bool ThongTinKhachHangExists(int id)
        {
            return _context.ThongTinKhachHangs.Any(e => e.ID == id);
        }
    }
}
