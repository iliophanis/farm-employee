using server.Data.Entities.BaseEntity;

namespace server.Data.Entities
{
    public partial class FarmerRating : Entity
    {
        public string Description { get; set; }
        public decimal Stars { get; set; }
        public decimal WorkPlaceRate { get; set; }
        public decimal PaymentConsequence { get; set; }
        public int EmployeeRequestId { get; set; }

        public virtual EmployeeRequest EmployeeRequest { get; set; }
    }
}
