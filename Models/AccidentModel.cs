using System.ComponentModel.DataAnnotations;

namespace Mappy.Models
{
    public class AccidentModel : Coordinates
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}