namespace server.Data.Entities
{
    public partial class SubEmployee
    {
        public int Id { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int EmployeeRequestId { get; set; }
        public int ContactInfoId { get; set; }

        public virtual ContactInfo ContactInfo { get; set; }
        public virtual EmployeeRequest EmployeeRequest { get; set; }
    }
}
