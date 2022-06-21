using System.ComponentModel.DataAnnotations;

namespace HostelListing.Models
{
    public class CreateCountryDto
    {
        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Country Name is too long!")]
        public string Name { get; set; } = "";

        [Required]
        [StringLength(maximumLength: 2, MinimumLength = 1, ErrorMessage = "Country Short Name should be 2 characters long!")]
        public string ShortName { get; set; } = "";
    }
}
