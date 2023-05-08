namespace server.Data.Entities
{
    public partial class EmployeeRequest
    {
        public EmployeeRequest()
        {
            EmployeeRatings = new HashSet<EmployeeRating>();
            FarmerRatings = new HashSet<FarmerRating>();
        }

        public enum paymentMethod{ bankTransfer, paypal, ebanking }

        public enum paymentStatus{ pemdingPayment, processing, onHold, completed , canceled, refunded, failed }

        public int Id { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool? MessageSent { get; set; }
        public paymentStatus PaymentStatus { get; set; }  
        public paymentMethod PaymentMethod { get; set; }  
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
