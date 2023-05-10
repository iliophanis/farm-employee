using server.Data.Entities.BaseEntity;

namespace server.Data.Entities
{
    public enum PaymentMethod { bankTransfer, paypal, ebanking }

    public enum PaymentStatus { pemdingPayment, processing, onHold, completed, canceled, refunded, failed }

    public partial class EmployeeRequest : Entity
    {
        public EmployeeRequest()
        {
            EmployeeRatings = new HashSet<EmployeeRating>();
            FarmerRatings = new HashSet<FarmerRating>();
        }


        public bool? MessageSent { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public int EmployeeId { get; set; }
        public int RequestId { get; set; }
        public int PackageId { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Package Package { get; set; }
        public virtual Request Request { get; set; }
        public virtual ICollection<EmployeeRating> EmployeeRatings { get; set; }
        public virtual ICollection<FarmerRating> FarmerRatings { get; set; }
        public virtual ICollection<SubEmployee> SubEmployees { get; set; }
    }

}
