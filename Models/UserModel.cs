using System.ComponentModel.DataAnnotations;

namespace Mappy.Models
{
    public class UserModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string? Username { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

        public DateTime LastReportedAccidentDate { get; set; }
    }
}