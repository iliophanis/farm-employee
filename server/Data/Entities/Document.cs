using System;
using System.Collections.Generic;

namespace server.Data.Entities
{
    public partial class Document
    {
        public Document()
        {
            Employees = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public byte[] Data { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
