using AuthMicroservice.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AuthMicroservice.DAL.Entities
{
    public class UserRoles
    {
        [Required]
        [Column(TypeName = "nvarchar(20)")]
        public Role Role { get; set; }

        [Required]
        public long UserId { get; set; }
        public User User { get; set; }
    }
}
