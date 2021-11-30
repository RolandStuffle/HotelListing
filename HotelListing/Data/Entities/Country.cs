using System.Collections.Generic;
using HotelListing.Data.Common;

namespace HotelListing.Data.Entities
{
    public class Country : BaseEntity
    {
        public string Name { get; set; }
        public string ShortName { get; set; }

        public virtual IList<Hotel> Hotels { get; set; }
    }
}
