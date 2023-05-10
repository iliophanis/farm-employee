using server.Data.Entities.BaseEntity;

namespace server.Data.Entities
{
    public enum AuthProvider { Google, Facebook }
    public partial class User : Entity
    {
        public User()
        {
            Employees = new HashSet<Employee>();
            Farmers = new HashSet<Farmer>();
            IsActive = true;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool EmailConformed { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public int? RoleId { get; set; }
        public AuthProvider? AuthProvider { get; set; }
        public virtual Role Role { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Farmer> Farmers { get; set; }

    }

}
