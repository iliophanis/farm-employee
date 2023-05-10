using server.Data.Entities.BaseEntity;

namespace server.Data.Entities
{
    public partial class SubEmployee : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int EmployeeRequestId { get; set; }
        public int ContactInfoId { get; set; }

        public virtual ContactInfo ContactInfo { get; set; }
        public virtual EmployeeRequest EmployeeRequest { get; set; }
    }
}
