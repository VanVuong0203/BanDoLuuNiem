using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using QuaLuuNiem.Data;
using QuaLuuNiem.Model;

namespace QuaLuuNiem.Pages.SanPhams
{
    public class IndexModel : PageModel
    {
        private readonly QuaLuuNiemContext _context;
        private readonly IConfiguration _configuration;

        public IndexModel(QuaLuuNiemContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public string Ten { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public PaginatedList<SanPham> SanPhams { get; set; }
        public SelectList LoaiList { get; set; }
        public string SanPhamLoai { get; set; }

        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, string sanPhamLoai, int? pageIndex)
        {
            CurrentSort = sortOrder;
            Ten = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            CurrentFilter = searchString;
            SanPhamLoai = sanPhamLoai;


            IQueryable<string> loaiQuery = from s in _context.SanPham
                                           orderby s.Loai
                                           select s.Loai;

            LoaiList = new SelectList(await loaiQuery.Distinct().ToListAsync());

            IQueryable<SanPham> sanphamsIQ = from s in _context.SanPham
                                             select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                sanphamsIQ = sanphamsIQ.Where(s => s.TenSP.Contains(searchString)
                                       || s.Loai.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(sanPhamLoai))
            {
                sanphamsIQ = sanphamsIQ.Where(x => x.Loai == sanPhamLoai);
            }

            switch (sortOrder)
            {
                case "name_desc":
                    sanphamsIQ = sanphamsIQ.OrderByDescending(s => s.TenSP);
                    break;
                case "ID":
                    sanphamsIQ = sanphamsIQ.OrderBy(s => s.MaSP);
                    break;
                default:
                    sanphamsIQ = sanphamsIQ.OrderBy(s => s.Loai);
                    break;
            }

            var pageSize = _configuration.GetValue("PageSize", 20);
            SanPhams = await PaginatedList<SanPham>.CreateAsync(sanphamsIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
