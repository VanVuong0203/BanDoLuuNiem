using System.ComponentModel.DataAnnotations;

namespace QuaLuuNiem.Model
{
    public class ThongKeDoanhThu
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Display(Name = "Số Lượng")]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public decimal Amount { get; set; }

        [Display(Name = "Tổng Doanh Thu")]
        [Range(1, 100000000)]
        [DisplayFormat(DataFormatString = "{0:#,##0 VND}", ApplyFormatInEditMode = true)]
        public decimal TotalRevenue { get; set; }
    }
}
