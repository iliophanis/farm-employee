using server.Data.Entities.BaseEntity;

namespace server.Data.Entities
{
    public partial class EmployeeRating : Entity
    {
        public string Description { get; set; }
        public decimal Stars { get; set; }
        public decimal JobQualityRate { get; set; }
        public decimal ContactQualityRate { get; set; }
        public int EmployeeRequestId { get; set; }

        public virtual EmployeeRequest EmployeeRequest { get; set; }
    }
}
