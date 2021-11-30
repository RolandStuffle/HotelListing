using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelListing.Models
{
    public class UserTO : LoginUserTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [DataType(DataType.PhoneNumber)] public string PhoneNumber { get; set; }

        public ICollection<string> Roles { get; set; }
    }

    public class LoginUserTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "Your password is limited to {2} to {1} character"), MinLength(6)]
        public string Password { get; set; }
    }
}