using AuthMicroservice.Domain.Enums;

namespace AuthMicroservice.Domain.Models
{
    public class UserRoles
    {
        public Role Role { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
    }
}
