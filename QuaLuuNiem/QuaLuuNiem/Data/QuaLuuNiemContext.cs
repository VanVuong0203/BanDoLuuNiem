using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuaLuuNiem.Model;

namespace QuaLuuNiem.Data
{
    public class QuaLuuNiemContext : DbContext
    {
        public QuaLuuNiemContext (DbContextOptions<QuaLuuNiemContext> options)
            : base(options)
        {
        }

        public DbSet<SanPham> SanPham { get; set; }
        public DbSet<ThongTinGioHang> GioHangs { get; set; }
        public DbSet<CTHD> CTHDs { get; set; }
        public DbSet<HoaDon> HoaDons { get; set; }
        public DbSet<ThongTinKhachHang> ThongTinKhachHangs { get; set; }
        public DbSet<ThongKeDoanhThu> ThongKeDoanhThus { get; set; } = default;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SanPham>().ToTable("SanPham");
            modelBuilder.Entity<ThongTinGioHang>().ToTable("ThongTinGioHang");
            modelBuilder.Entity<CTHD>().ToTable("CTHD");
            modelBuilder.Entity<HoaDon>().ToTable("HoaDon");
            modelBuilder.Entity<ThongTinKhachHang>().ToTable("ThongTinKhachHang");
            modelBuilder.Entity<ThongKeDoanhThu>().ToTable("ThongKeDoanhThu");

        }
    }
}
