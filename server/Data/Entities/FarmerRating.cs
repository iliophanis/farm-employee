using System;
using System.Collections.Generic;

namespace server.Data.Entities
{
    public partial class FarmerRating
    {
        public int Id { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Description { get; set; }
        public decimal Stars { get; set; }
        public decimal WorkPlaceRate { get; set; }
        public decimal PaymentConsequence { get; set; }
        public int EmployeeRequestId { get; set; }

        public virtual EmployeeRequest EmployeeRequest { get; set; }
    }
}
