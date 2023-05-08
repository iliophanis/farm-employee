using server.Data.Entities;
using server.Data.Mappings;

namespace server.Data
{
    public partial class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
            base.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            Database.SetCommandTimeout(9000);
        }

        public virtual DbSet<ContactInfo> ContactInfos { get; set; }
        public virtual DbSet<Cultivation> Cultivations { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeRating> EmployeeRatings { get; set; }
        public virtual DbSet<SubEmployee> SubEmployees { get; set; }
        public virtual DbSet<EmployeeRequest> EmployeeRequests { get; set; }
        public virtual DbSet<Farmer> Farmers { get; set; }
        public virtual DbSet<FarmerLocation> FarmerLocations { get; set; }
        public virtual DbSet<FarmerRating> FarmerRatings { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Package> Packages { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_general_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.ApplyConfiguration(new ContactInfoMap());

            modelBuilder.ApplyConfiguration(new CultivationMap());

            modelBuilder.ApplyConfiguration(new DocumentMap());

            modelBuilder.ApplyConfiguration(new EmployeeMap());

            modelBuilder.ApplyConfiguration(new EmployeeRatingMap());

            modelBuilder.ApplyConfiguration(new EmployeeRequestMap());

            modelBuilder.ApplyConfiguration(new SubEmployeeMap());

            modelBuilder.ApplyConfiguration(new FarmerMap());

            modelBuilder.ApplyConfiguration(new FarmerLocationMap());

            modelBuilder.ApplyConfiguration(new FarmerRatingMap());

            modelBuilder.ApplyConfiguration(new LocationMap());

            modelBuilder.ApplyConfiguration(new PackageMap());

            modelBuilder.ApplyConfiguration(new RequestMap());

            modelBuilder.ApplyConfiguration(new RoleMap());

            modelBuilder.ApplyConfiguration(new UserMap());

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
