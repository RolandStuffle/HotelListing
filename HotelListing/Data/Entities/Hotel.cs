using System.ComponentModel.DataAnnotations.Schema;
using HotelListing.Data.Common;

namespace HotelListing.Data.Entities
{
    public class Hotel : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public double Rating { get; set; }

        [ForeignKey(nameof(Country))] public int CountryId { get; set; }
        public Country Country { get; set; }
    }
}