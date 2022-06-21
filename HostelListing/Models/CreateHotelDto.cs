
using System.ComponentModel.DataAnnotations;

namespace HostelListing.Models
{
    public class CreateHotelDto
    {
        [Required]
        [StringLength(maximumLength: 150, ErrorMessage = "Hotel Name is too long, it can be 150 characters max!")]
        public string Name { get; set; } = "";

        [Required]
        [StringLength(maximumLength: 250, ErrorMessage = "Hotel Address is too long, it can be 250 characters max!")]
        public string Address { get; set; } = "";

        [Required]
        [Range(1, 5)]
        public double Rating { get; set; }

        [Required]
        public int CountryId { get; set; }
    }
}
