using System;
using System.Collections.Generic;

namespace server.Data.Entities
{
    public partial class EmployeeRequest
    {
        public EmployeeRequest()
        {
            EmployeeRatings = new HashSet<EmployeeRating>();
            Farmerratings = new HashSet<Farmerrating>();
        }

        public int Id { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool? MessageSent { get; set; }
        public string PaymentStatus { get; set; }
        public string PaymentMethod { get; set; }
        public int EmployeeId { get; set; }
        public int RequestId { get; set; }
        public int PackageId { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Package Package { get; set; }
        public virtual Request Request { get; set; }
        public virtual ICollection<EmployeeRating> EmployeeRatings { get; set; }
        public virtual ICollection<Farmerrating> Farmerratings { get; set; }
    }
}
