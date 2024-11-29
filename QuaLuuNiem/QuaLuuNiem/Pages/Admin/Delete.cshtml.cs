using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuaLuuNiem.Data;
using QuaLuuNiem.Model;

namespace QuaLuuNiem.Pages.Admin
{
    public class DeleteModel : PageModel
    {
        private readonly QuaLuuNiemContext _context;

        public DeleteModel(QuaLuuNiemContext context)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ThongTinKhachHang = await _context.ThongTinKhachHangs.FindAsync(id);

            if (ThongTinKhachHang != null)
            {
                _context.ThongTinKhachHangs.Remove(ThongTinKhachHang);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./QuanLyThongTinKhachHang");
        }
    }
}
