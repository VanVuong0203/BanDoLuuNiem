using QuaLuuNiem.Model;

namespace QuaLuuNiem.Data
{
    public class DbInitializer
    {
        public static void Initialize(QuaLuuNiemContext context)
        {
            context.Database.EnsureCreated();
            
            // Look for any students.
            if (context.SanPham.Any())
            {
                return;   // DB has been seeded
            }

            var sanphams = new SanPham[]
            {
                new SanPham{ID=1, MaSP="GB1",TenSP="Gấu bông Doreamon",SoLuong=100, Gia=150000, HinhAnh = "GBDoraemon.jpg",MoTa="CHI TIẾT SẢN PHẨM:\r\n- Tên sản phẩm: Gấu bông Thú bông - Doremon Doreamon vui vẻ\r\n- Chất liệu: vải mịn và bông lông gòn xốp phồng.\r\n- Công dụng: Dùng để trang trí, ôm ngủ, ... hay làm quà tặng bạn bè, người thân, em bé...",Loai="GauBong"},
            new SanPham { ID = 2, MaSP = "GB2", TenSP = "Gấu Bông Mèo Hoàng Thượng Cosplay 3 Màu - Mèo Bông Cao Cấp Gấu Bông City", SoLuong = 50, Gia = 180000, HinhAnh = "GBMeoHoangThuong.jpg", MoTa = "Tên sản phẩm: Gấu Bông Mèo Hoàng Thượng Cosplay\r\n✔️ Màu sắc: nhiều màu\r\n✔️ Kích Thước: 22 cm - 25 cm - 40 cm - 50 cm\r\n✔️ Cân Nặng: \r\n✔️ Xuất xứ: Việt Nam\r\n✔️ Chất liệu vải lông cao cấp\r\n✔️ Ruột nhồi 100% bông gòn trắng\r\n✔️ Ảnh Thật Độc Quyền Shop Tự Chụp \r\n✔️ An toàn cho sức khỏe người dùng, vải mềm mịn tạo cảm giác thoải mái khi ôm.\r\n✔️ Là món quà ý nghĩa dành tặng người thương\r\n✔️ Shop luôn khẳng định 100% sản phẩm đến tay người tiêu dùng giống hệt hình ảnh và video mô tả sản phẩm.", Loai = "GauBong" },
            };

            foreach (SanPham s in sanphams)
            {
                context.SanPham.Add(s);
            }
            //context.SaveChanges();
            
        }
    }
}
