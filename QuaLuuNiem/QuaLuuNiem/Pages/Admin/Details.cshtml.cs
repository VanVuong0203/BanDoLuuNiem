using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuaLuuNiem.Data;
using QuaLuuNiem.Model;

namespace QuaLuuNiem.Pages.Admin
{
    public class DetailsModel : PageModel
    {
        private readonly QuaLuuNiemContext _context;

        public DetailsModel(QuaLuuNiemContext context)
        {
            _context = context;
        }

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
    }
}
