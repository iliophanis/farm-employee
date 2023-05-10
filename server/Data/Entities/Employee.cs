using server.Data.Entities.BaseEntity;

namespace server.Data.Entities
{
    public partial class Employee : Entity
    {
        public Employee()
        {
            EmployeeRequests = new HashSet<EmployeeRequest>();
        }
        public decimal AvgRate { get; set; }
        public decimal AvgJobQuality { get; set; }
        public decimal AvgPrice { get; set; }
        public decimal AvgContactQuality { get; set; }
        public int UserId { get; set; }
        public int? DocumentId { get; set; }
        public int? ContactInfoId { get; set; }

        public virtual ContactInfo ContactInfo { get; set; }
        public virtual Document Document { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<EmployeeRequest> EmployeeRequests { get; set; }
    }
}
