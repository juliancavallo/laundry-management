using System.ComponentModel;

namespace LaundryManagement.Domain.DTOs
{
    public class UserViewDTO
    {
        public int Id { get; set; }
        [DisplayName("Full name")]
        public string FullName { get; set; }
        [DisplayName("User name")]
        public string UserName { get; set; }
        [DisplayName("Email")]
        public string Email { get; set; }
    }
}
