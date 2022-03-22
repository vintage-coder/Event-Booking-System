using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EBSystem.Authentication.API.Entities
{
    public class Login
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string? UserName { get; set; }

        public string? Password { get; set; }

        public string? RefreshToken { get; set; }

        
        public DateTime? RefreshTokenExpiryTime { get; set; }

    }
}
