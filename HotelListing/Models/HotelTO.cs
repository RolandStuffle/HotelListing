using System.ComponentModel.DataAnnotations;

namespace HotelListing.Models
{
    public class HotelTO : CreateHotelTO
    {
        public int Id { get; set; }

        public CountryTO Country { get; set; }
    }

    public class UpdateHotelTO : CreateHotelTO
    {
    }

    public class CreateHotelTO
    {
        [Required]
        [StringLength(maximumLength: 100, ErrorMessage = "Hotel name is too long")]
        public string Name { get; set; }

        [Required]
        [StringLength(maximumLength: 250, ErrorMessage = "Address is too long")]
        public string Address { get; set; }

        [Required] [Range(1, 5)] public double Rating { get; set; }

        [Required] public int CountryId { get; set; }
    }
}