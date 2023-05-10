using server.Data.Entities.BaseEntity;

namespace server.Data.Entities
{
    public partial class Role : Entity
    {
        public Role()
        {
            Users = new HashSet<User>();
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
