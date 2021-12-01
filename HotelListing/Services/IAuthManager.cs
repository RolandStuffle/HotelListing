using System.Threading.Tasks;
using HotelListing.Models;

namespace HotelListing.Services
{
    public interface IAuthManager
    {
        Task<bool> ValidateUser(LoginUserTO userTo);

        Task<string> CreateToken();
    }
}
