using server.Data.Entities.BaseEntity;

namespace server.Data.Entities
{
    public partial class Package : Entity
    {
        public Package()
        {
            EmployeeRequests = new HashSet<EmployeeRequest>();
        }

        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public int MaxRequests { get; set; }

        public virtual ICollection<EmployeeRequest> EmployeeRequests { get; set; }
    }
}
