using System;
using System.Collections.Generic;

namespace server.Data.Entities
{
    public partial class Employee
    {
        public Employee()
        {
            EmployeeRequests = new HashSet<EmployeeRequest>();
        }

        public int Id { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public decimal AvgRate { get; set; }
        public decimal AvgJobQuality { get; set; }
        public decimal AvgPrice { get; set; }
        public decimal AvgContactQuality { get; set; }
        public int UserId { get; set; }
        public int? DocumentId { get; set; }
        public int ContactInfo { get; set; }

        public virtual ContactInfo ContactInfoNavigation { get; set; }
        public virtual Document Document { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<EmployeeRequest> EmployeeRequests { get; set; }
    }
}
