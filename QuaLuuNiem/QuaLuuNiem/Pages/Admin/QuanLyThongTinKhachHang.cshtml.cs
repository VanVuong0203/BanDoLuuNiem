using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuaLuuNiem.Data;
using QuaLuuNiem.Model;

namespace QuaLuuNiem.Pages.Admin
{
    public class QuanLyThongTinKhachHangModel : PageModel
    {
        private readonly QuaLuuNiemContext _context;

        public QuanLyThongTinKhachHangModel(QuaLuuNiemContext context)
        {
            _context = context;
        }

        public IList<ThongTinKhachHang> ThongTinKhachHang { get; set; }

        public void OnGet()
        {
            ThongTinKhachHang = _context.ThongTinKhachHangs.ToList();
        }
    }
}
