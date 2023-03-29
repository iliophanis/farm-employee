using System;
using System.Collections.Generic;

namespace server.Data.Entities
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
