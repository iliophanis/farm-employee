namespace server.Data.Entities
{
    public partial class Farmer
    {
        public Farmer()
        {
            FarmerLocations = new HashSet<FarmerLocation>();
            Requests = new HashSet<Request>();
        }

        public int Id { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Description { get; set; }
        public decimal AvgRate { get; set; }
        public decimal AvgWorkPlaceRate { get; set; }
        public decimal AvgPaymentConsequenceRate { get; set; }
        public string PaymentStatus { get; set; } //add enum PaymentStatus
        public string PaymentMethod { get; set; } //add enum PaymentMethod
        public int UserId { get; set; }
        public int? ContactInfoId { get; set; }

        public virtual ContactInfo ContactInfo { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<FarmerLocation> FarmerLocations { get; set; }
        public virtual ICollection<Request> Requests { get; set; }

    }
}
