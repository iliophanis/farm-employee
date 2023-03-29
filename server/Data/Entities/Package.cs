using System;
using System.Collections.Generic;

namespace server.Data.Entities
{
    public partial class Package
    {
        public Package()
        {
            EmployeeRequests = new HashSet<EmployeeRequest>();
        }

        public int Id { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public int MaxRequests { get; set; }

        public virtual ICollection<EmployeeRequest> EmployeeRequests { get; set; }
    }
}
