using System;
using System.Collections.Generic;

namespace server.Data.Entities
{
    public partial class Request
    {
        public Request()
        {
            EmployeeRequests = new HashSet<EmployeeRequest>();
        }

        public int Id { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string JobType { get; set; }
        public DateOnly? StartJobDate { get; set; }
        public int? EstimatedDuration { get; set; }
        public decimal? Price { get; set; }
        public decimal? StayAmount { get; set; }
        public decimal? TravelAmount { get; set; }
        public decimal? FoodAmount { get; set; }
        public int LocationId { get; set; }
        public int FarmerId { get; set; }
        public int CultivationId { get; set; }

        public virtual Cultivation Cultivation { get; set; }
        public virtual Farmer Farmer { get; set; }
        public virtual Location Location { get; set; }
        public virtual ICollection<EmployeeRequest> EmployeeRequests { get; set; }
    }
}
