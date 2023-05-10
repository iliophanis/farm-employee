using server.Data.Entities.BaseEntity;

namespace server.Data.Entities
{
    public partial class Document : Entity
    {
        public Document()
        {
            Employees = new HashSet<Employee>();
        }

        public string Type { get; set; }
        public string Name { get; set; }
        public byte[] Data { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
