using System.ComponentModel.DataAnnotations;

namespace Mappy.Models
{
    public class Coordinates
    {
        [Required]
        public double Latitude { get; set; }
        
        [Required]
        public double Longitude { get; set; }
    }
}