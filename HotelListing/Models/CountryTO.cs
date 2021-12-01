using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelListing.Models
{
    public class CountryTO : CreateCountryTO
    {
        public int Id { get; set; }

        public virtual IList<HotelTO> Hotels { get; set; }
    }

    public class UpdateCountryTO : CreateCountryTO
    {

    }

    public class CreateCountryTO
    {
        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Country name is too long")]
        public string Name { get; set; }

        [Required]
        [StringLength(maximumLength: 2, ErrorMessage = "Short Country name is too long")]
        public string ShortName { get; set; }
    }
}