namespace server.Data.Entities
{
    public partial class User
    {
        public User()
        {
            Employees = new HashSet<Employee>();
            Farmers = new HashSet<Farmer>();
            IsActive = true;
        }

        public int Id { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool EmailConformed { get; set; }
        public bool IsActive { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public int RoleId { get; set; }

        //TODO public string AuthProvider {get; set;} UPDATE IN ER DIAGRAM , ER PNG , MODEL AND UserMap 
        public string AuthProvider { get; set; }
        public virtual Role Role { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Farmer> Farmers { get; set; }
    }
}
