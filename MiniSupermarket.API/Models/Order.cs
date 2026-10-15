
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniSupermarket.API.Models
{
    /// <summary>
    /// Đại diện cho một đơn hàng trong hệ thống siêu thị mini.
    /// Lưu trữ thông tin nhân viên bán hàng, khách hàng và thanh toán.
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Mã định danh duy nhất của đơn hàng.
        /// </summary>
        [Key]
        public int OrderId { get; set; }

        /// <summary>Điểm đã tặng khi checkout, dùng để hoàn tác đúng lúc hủy.</summary>
        public int RewardPointsAwarded { get; set; }

        /// <summary>
        /// Thời điểm tạo đơn hàng theo múi giờ UTC.
        /// </summary>
        public DateTime CreatedAtUtc { get; set; }

        /// <summary>
        /// Mã nhân viên thu ngân thực hiện giao dịch.
        /// </summary>
        public int CashierId { get; set; }

        /// <summary>
        /// Mã khách hàng mua hàng.
        /// Có thể null nếu khách hàng không đăng ký thành viên.
        /// </summary>
        public int? CustomerId { get; set; }

        /// <summary>
        /// Tổng số tiền cần thanh toán của đơn hàng.
        /// </summary>
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Số tiền mặt khách hàng đưa cho thu ngân.
        /// </summary>
        [Column(TypeName = "decimal(18,2)")]
        public decimal CashReceived { get; set; }

        /// <summary>
        /// Số tiền thừa cần trả lại cho khách hàng.
        /// </summary>
        [Column(TypeName = "decimal(18,2)")]
        public decimal ChangeAmount { get; set; }

        /// <summary>
        /// Danh sách các sản phẩm thuộc đơn hàng.
        /// Quan hệ một-nhiều với OrderItem.
        /// </summary>
        public List<OrderItem> Items { get; set; } = new();
    }

    /// <summary>
    /// Đại diện cho một dòng chi tiết trong đơn hàng.
    /// Lưu thông tin sản phẩm, số lượng và giá bán tại thời điểm mua.
    /// </summary>
    public class OrderItem
    {
        /// <summary>
        /// Mã định danh duy nhất của chi tiết đơn hàng.
        /// </summary>
        [Key]
        public int OrderItemId { get; set; }

        /// <summary>
        /// Mã đơn hàng chứa chi tiết này.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Đơn hàng mà chi tiết này thuộc về.
        /// Navigation property trong Entity Framework Core.
        /// </summary>
        public Order Order { get; set; } = null!;

        /// <summary>
        /// Mã sản phẩm được bán.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Tên sản phẩm tại thời điểm bán.
        /// Bắt buộc nhập và có độ dài tối đa 150 ký tự.
        /// </summary>
        [Required]
        [MaxLength(150)]
        public string ProductName { get; set; } = string.Empty;

        /// <summary>
        /// Số lượng sản phẩm được mua.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Đơn giá sản phẩm tại thời điểm bán.
        /// Được lưu cố định để đảm bảo lịch sử giao dịch
        /// không bị ảnh hưởng khi giá sản phẩm thay đổi.
        /// </summary>
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Thành tiền của sản phẩm.
        /// Được tính bằng số lượng nhân với đơn giá.
        /// </summary>
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }
    }
}
