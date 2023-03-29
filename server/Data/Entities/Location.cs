using System;
using System.Collections.Generic;

namespace server.Data.Entities
{
    public partial class Location
    {
        public Location()
        {
            FarmerLocations = new HashSet<FarmerLocation>();
            Requests = new HashSet<Request>();
        }

        public int Id { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public decimal Longtitude { get; set; }
        public decimal Latitude { get; set; }
        public string Prefecture { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string Street { get; set; }

        public virtual ICollection<FarmerLocation> FarmerLocations { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
    }
}
