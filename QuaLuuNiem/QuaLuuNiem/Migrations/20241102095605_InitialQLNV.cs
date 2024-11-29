using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuaLuuNiem.Migrations
{
    /// <inheritdoc />
    public partial class InitialQLNV : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ThongTinGioHangID",
                table: "SanPham",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CTHD",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaHD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaSP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenSP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    Gia = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CTHD", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "HoaDon",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaKH = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaHD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayBan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TongTien = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDon", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ThongKeDoanhThu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalRevenue = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThongKeDoanhThu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ThongTinGioHang",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaKH = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaSP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenSP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    Gia = table.Column<int>(type: "int", nullable: false),
                    HinhAnh = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThongTinGioHang", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CTHDHoaDon",
                columns: table => new
                {
                    CTHDsID = table.Column<int>(type: "int", nullable: false),
                    HoaDonsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CTHDHoaDon", x => new { x.CTHDsID, x.HoaDonsID });
                    table.ForeignKey(
                        name: "FK_CTHDHoaDon_CTHD_CTHDsID",
                        column: x => x.CTHDsID,
                        principalTable: "CTHD",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CTHDHoaDon_HoaDon_HoaDonsID",
                        column: x => x.HoaDonsID,
                        principalTable: "HoaDon",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThongTinKhachHang",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaKH = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HoTen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SDT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenDangNhap = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThongTinGioHangID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThongTinKhachHang", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ThongTinKhachHang_ThongTinGioHang_ThongTinGioHangID",
                        column: x => x.ThongTinGioHangID,
                        principalTable: "ThongTinGioHang",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "HoaDonThongTinKhachHang",
                columns: table => new
                {
                    HoaDonsID = table.Column<int>(type: "int", nullable: false),
                    ThongTinKhachHangID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDonThongTinKhachHang", x => new { x.HoaDonsID, x.ThongTinKhachHangID });
                    table.ForeignKey(
                        name: "FK_HoaDonThongTinKhachHang_HoaDon_HoaDonsID",
                        column: x => x.HoaDonsID,
                        principalTable: "HoaDon",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HoaDonThongTinKhachHang_ThongTinKhachHang_ThongTinKhachHangID",
                        column: x => x.ThongTinKhachHangID,
                        principalTable: "ThongTinKhachHang",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_ThongTinGioHangID",
                table: "SanPham",
                column: "ThongTinGioHangID");

            migrationBuilder.CreateIndex(
                name: "IX_CTHDHoaDon_HoaDonsID",
                table: "CTHDHoaDon",
                column: "HoaDonsID");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDonThongTinKhachHang_ThongTinKhachHangID",
                table: "HoaDonThongTinKhachHang",
                column: "ThongTinKhachHangID");

            migrationBuilder.CreateIndex(
                name: "IX_ThongTinKhachHang_ThongTinGioHangID",
                table: "ThongTinKhachHang",
                column: "ThongTinGioHangID");

            migrationBuilder.AddForeignKey(
                name: "FK_SanPham_ThongTinGioHang_ThongTinGioHangID",
                table: "SanPham",
                column: "ThongTinGioHangID",
                principalTable: "ThongTinGioHang",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SanPham_ThongTinGioHang_ThongTinGioHangID",
                table: "SanPham");

            migrationBuilder.DropTable(
                name: "CTHDHoaDon");

            migrationBuilder.DropTable(
                name: "HoaDonThongTinKhachHang");

            migrationBuilder.DropTable(
                name: "ThongKeDoanhThu");

            migrationBuilder.DropTable(
                name: "CTHD");

            migrationBuilder.DropTable(
                name: "HoaDon");

            migrationBuilder.DropTable(
                name: "ThongTinKhachHang");

            migrationBuilder.DropTable(
                name: "ThongTinGioHang");

            migrationBuilder.DropIndex(
                name: "IX_SanPham_ThongTinGioHangID",
                table: "SanPham");

            migrationBuilder.DropColumn(
                name: "ThongTinGioHangID",
                table: "SanPham");
        }
    }
}
