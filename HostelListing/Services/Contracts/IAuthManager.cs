using HostelListing.Models;

namespace HostelListing.Services.Contracts
{
    public interface IAuthManager
    {
        Task<bool> ValidateUser(UserLoginDto userDto);
        Task<string> CreateToken();
    }
}
