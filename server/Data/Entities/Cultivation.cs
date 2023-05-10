using server.Data.Entities.BaseEntity;

namespace server.Data.Entities
{
    public partial class Cultivation : Entity
    {
        public Cultivation()
        {
            Requests = new HashSet<Request>();
        }
        public string Name { get; set; }

        public virtual ICollection<Request> Requests { get; set; }
    }
}
