using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniSupermarket.API.Models
{
    // Ánh xạ model Customer với bảng "Customers" trong SQL Server
    [Table("Customers")]
    public class Customer
    {
        // Khóa chính của bảng Customers
        // Giá trị được SQL Server tự động tăng theo IDENTITY(1,1)
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }

        // Tên khách hàng
        // Bắt buộc nhập và tối đa 100 ký tự
        [Required(ErrorMessage = "Tên khách hàng không được để trống!")]
        [StringLength(100, ErrorMessage = "Tên khách hàng không vượt quá 100 ký tự")]
        public string CustomerName { get; set; } = string.Empty;

        // Số điện thoại dùng làm thông tin liên hệ chính của khách hàng
        // Lưu dưới dạng VARCHAR(15), bắt buộc nhập
        [Required(ErrorMessage = "Số điện thoại không được để trống!")]
        [Column(TypeName = "varchar(15)")]
        [StringLength(15, ErrorMessage = "Số điện thoại không vượt quá 15 ký tự")]
        public string PhoneNumber { get; set; } = string.Empty;

        // Địa chỉ khách hàng
        // Không bắt buộc, tối đa 200 ký tự
        [StringLength(200, ErrorMessage = "Địa chỉ không vượt quá 200 ký tự")]
        public string? Address { get; set; }

        // Điểm thưởng tích lũy của khách hàng
        // Khách hàng mới mặc định có 0 điểm
        public int RewardPoints { get; set; } = 0;

        // Hạng thành viên của khách hàng
        // Giá trị mặc định khi tạo mới là "Chuẩn"
        [StringLength(50, ErrorMessage = "Hạng thành viên không vượt quá 50 ký tự")]
        public string MembershipRank { get; set; } = "Chuẩn";
    }
}