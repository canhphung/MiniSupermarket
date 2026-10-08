using System.Text.Json.Serialization;

namespace MiniSupermarket.API.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        [JsonIgnore]
        public string PasswordHash { get; set; } = string.Empty;

        // Admin / Cashier / Warehouse
        public string Role { get; set; } = "Cashier";

        public bool IsActive { get; set; } = true;
    }
}