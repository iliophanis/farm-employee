using server.Data.Entities.BaseEntity;

namespace server.Data.Entities
{
    public partial class ContactInfo : Entity
    {
        public ContactInfo()
        {
            Employees = new HashSet<Employee>();
            Farmers = new HashSet<Farmer>();
        }
        public string Address { get; set; }
        public string City { get; set; }
        public string Tk { get; set; }
        public string PhoneNo { get; set; }
        public string MobilePhoneNo { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Farmer> Farmers { get; set; }
        public virtual ICollection<SubEmployee> SubEmployees { get; set; }
    }
}
