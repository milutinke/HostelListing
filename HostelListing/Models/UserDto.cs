using System.ComponentModel.DataAnnotations;

namespace HostelListing.Models
{
    public class UserDto : UserLoginDto
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; } = "";

        public ICollection<string>? Roles { get; set; }
    }
}
