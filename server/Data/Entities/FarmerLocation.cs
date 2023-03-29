using System;
using System.Collections.Generic;

namespace server.Data.Entities
{
    public partial class FarmerLocation
    {
        public int Id { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public byte[] Data { get; set; }
        public int FarmerId { get; set; }
        public int LocationId { get; set; }

        public virtual Farmer Farmer { get; set; }
        public virtual Location Location { get; set; }
    }
}
