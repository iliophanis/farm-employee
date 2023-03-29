using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
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
        public virtual DbSet<EmployeeRequest> EmployeeRequests { get; set; }
        public virtual DbSet<Farmer> Farmers { get; set; }
        public virtual DbSet<FarmerLocation> FarmerLocations { get; set; }
        public virtual DbSet<Farmerrating> Farmerratings { get; set; }
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


            modelBuilder.Entity<Cultivation>(entity =>
            {
                entity.ToTable("cultivation");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.InsertDate)
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("insertDate")
                    .HasDefaultValueSql("current_timestamp()");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("timestamp")
                    .HasColumnName("updateDate")
                    .HasDefaultValueSql("'0000-00-00 00:00:00'");
            });

            modelBuilder.Entity<Document>(entity =>
            {
                entity.ToTable("document");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Data)
                    .IsRequired()
                    .HasColumnName("data");

                entity.Property(e => e.InsertDate)
                    .HasColumnType("timestamp")
                    .HasColumnName("insertDate")
                    .HasDefaultValueSql("current_timestamp()");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("type");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("updateDate")
                    .HasDefaultValueSql("current_timestamp()");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("employee");

                entity.HasIndex(e => e.ContactInfo, "contactInfo");

                entity.HasIndex(e => e.DocumentId, "documentId");

                entity.HasIndex(e => e.UserId, "userId");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.AvgContactQuality)
                    .HasPrecision(10)
                    .HasColumnName("avgContactQuality");

                entity.Property(e => e.AvgJobQuality)
                    .HasPrecision(10)
                    .HasColumnName("avgJobQuality");

                entity.Property(e => e.AvgPrice)
                    .HasPrecision(10)
                    .HasColumnName("avgPrice");

                entity.Property(e => e.AvgRate)
                    .HasPrecision(10)
                    .HasColumnName("avgRate");

                entity.Property(e => e.ContactInfo)
                    .HasColumnType("int(11)")
                    .HasColumnName("contactInfo");

                entity.Property(e => e.DocumentId)
                    .HasColumnType("int(11)")
                    .HasColumnName("documentId");

                entity.Property(e => e.InsertDate)
                    .HasColumnType("timestamp")
                    .HasColumnName("insertDate")
                    .HasDefaultValueSql("current_timestamp()");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("updateDate")
                    .HasDefaultValueSql("current_timestamp()");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("userId");

                entity.HasOne(d => d.ContactInfoNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.ContactInfo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("employee_ibfk_3");

                entity.HasOne(d => d.Document)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DocumentId)
                    .HasConstraintName("employee_ibfk_2");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("employee_ibfk_1");
            });

            modelBuilder.Entity<EmployeeRating>(entity =>
            {
                entity.ToTable("employee_rating");

                entity.HasIndex(e => e.EmployeeRequestId, "employeeRequestId");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.ContactQualityRate).HasPrecision(10);

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.EmployeeRequestId)
                    .HasColumnType("int(11)")
                    .HasColumnName("employeeRequestId");

                entity.Property(e => e.InsertDate)
                    .HasColumnType("timestamp")
                    .HasColumnName("insertDate")
                    .HasDefaultValueSql("current_timestamp()");

                entity.Property(e => e.JobQualityRate)
                    .HasPrecision(10)
                    .HasColumnName("jobQualityRate");

                entity.Property(e => e.Stars)
                    .HasPrecision(10)
                    .HasColumnName("stars");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("updateDate")
                    .HasDefaultValueSql("current_timestamp()");

                entity.HasOne(d => d.EmployeeRequest)
                    .WithMany(p => p.EmployeeRatings)
                    .HasForeignKey(d => d.EmployeeRequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("employee_rating_ibfk_1");
            });

            modelBuilder.Entity<EmployeeRequest>(entity =>
            {
                entity.ToTable("employee_request");

                entity.HasIndex(e => e.EmployeeId, "employeeId");

                entity.HasIndex(e => e.PackageId, "packageId");

                entity.HasIndex(e => e.RequestId, "requestId");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.EmployeeId)
                    .HasColumnType("int(11)")
                    .HasColumnName("employeeId");

                entity.Property(e => e.InsertDate)
                    .HasColumnType("timestamp")
                    .HasColumnName("insertDate")
                    .HasDefaultValueSql("current_timestamp()");

                entity.Property(e => e.MessageSent)
                    .HasColumnName("messageSent")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.PackageId)
                    .HasColumnType("int(11)")
                    .HasColumnName("packageId");

                entity.Property(e => e.PaymentMethod)
                    .IsRequired()
                    .HasColumnType("enum('bankTransfer','paypal','ebanking')")
                    .HasColumnName("paymentMethod");

                entity.Property(e => e.PaymentStatus)
                    .IsRequired()
                    .HasColumnType("enum('pendingPayment','processing','onHold','completed','canceled','refunded','failed')")
                    .HasColumnName("paymentStatus");

                entity.Property(e => e.RequestId)
                    .HasColumnType("int(11)")
                    .HasColumnName("requestId");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("updateDate")
                    .HasDefaultValueSql("current_timestamp()");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeRequests)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("employee_request_ibfk_1");

                entity.HasOne(d => d.Package)
                    .WithMany(p => p.EmployeeRequests)
                    .HasForeignKey(d => d.PackageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("employee_request_ibfk_3");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.EmployeeRequests)
                    .HasForeignKey(d => d.RequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("employee_request_ibfk_2");
            });

            modelBuilder.Entity<Farmer>(entity =>
            {
                entity.ToTable("farmer");

                entity.HasIndex(e => e.ContactInfo, "contactInfo");

                entity.HasIndex(e => e.UserId, "userId");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.AvgPaymentConsequenceRate)
                    .HasPrecision(10)
                    .HasColumnName("avgPaymentConsequenceRate");

                entity.Property(e => e.AvgRate)
                    .HasPrecision(10)
                    .HasColumnName("avgRate");

                entity.Property(e => e.AvgWorkPlaceRate)
                    .HasPrecision(10)
                    .HasColumnName("avgWorkPlaceRate");

                entity.Property(e => e.ContactInfo)
                    .HasColumnType("int(11)")
                    .HasColumnName("contactInfo");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.InsertDate)
                    .HasColumnType("timestamp")
                    .HasColumnName("insertDate")
                    .HasDefaultValueSql("current_timestamp()");

                entity.Property(e => e.PaymentMethod)
                    .IsRequired()
                    .HasColumnType("enum('bankTransfer','paypal','ebanking')")
                    .HasColumnName("paymentMethod");

                entity.Property(e => e.PaymentStatus)
                    .IsRequired()
                    .HasColumnType("enum('pendingPayment','processing','onHold','completed','canceled','refunded','failed')")
                    .HasColumnName("paymentStatus");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("updateDate")
                    .HasDefaultValueSql("current_timestamp()");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("userId");

                entity.HasOne(d => d.ContactInfoNavigation)
                    .WithMany(p => p.Farmers)
                    .HasForeignKey(d => d.ContactInfo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("farmer_ibfk_2");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Farmers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("farmer_ibfk_1");
            });

            modelBuilder.Entity<FarmerLocation>(entity =>
            {
                entity.ToTable("farmer_location");

                entity.HasIndex(e => e.FarmerId, "farmerId");

                entity.HasIndex(e => e.LocationId, "locationId");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Data)
                    .IsRequired()
                    .HasColumnName("data");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.FarmerId)
                    .HasColumnType("int(11)")
                    .HasColumnName("farmerId");

                entity.Property(e => e.InsertDate)
                    .HasColumnType("timestamp")
                    .HasColumnName("insertDate")
                    .HasDefaultValueSql("current_timestamp()");

                entity.Property(e => e.LocationId)
                    .HasColumnType("int(11)")
                    .HasColumnName("locationId");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("updateDate")
                    .HasDefaultValueSql("current_timestamp()");

                entity.HasOne(d => d.Farmer)
                    .WithMany(p => p.FarmerLocations)
                    .HasForeignKey(d => d.FarmerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("farmer_location_ibfk_2");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.FarmerLocations)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("farmer_location_ibfk_1");
            });

            modelBuilder.Entity<Farmerrating>(entity =>
            {
                entity.ToTable("farmerrating");

                entity.HasIndex(e => e.EmployeeRequestId, "employeeRequestId");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.EmployeeRequestId)
                    .HasColumnType("int(11)")
                    .HasColumnName("employeeRequestId");

                entity.Property(e => e.InsertDate)
                    .HasColumnType("timestamp")
                    .HasColumnName("insertDate")
                    .HasDefaultValueSql("current_timestamp()");

                entity.Property(e => e.PaymentConsequence)
                    .HasPrecision(10)
                    .HasColumnName("paymentConsequence");

                entity.Property(e => e.Stars)
                    .HasPrecision(10)
                    .HasColumnName("stars");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("updateDate")
                    .HasDefaultValueSql("current_timestamp()");

                entity.Property(e => e.WorkPlaceRate)
                    .HasPrecision(10)
                    .HasColumnName("workPlaceRate");

                entity.HasOne(d => d.EmployeeRequest)
                    .WithMany(p => p.Farmerratings)
                    .HasForeignKey(d => d.EmployeeRequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("farmerrating_ibfk_1");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("location");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("city");

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("country");

                entity.Property(e => e.InsertDate)
                    .HasColumnType("timestamp")
                    .HasColumnName("insertDate")
                    .HasDefaultValueSql("current_timestamp()");

                entity.Property(e => e.Latitude)
                    .HasPrecision(10)
                    .HasColumnName("latitude");

                entity.Property(e => e.Longtitude)
                    .HasPrecision(10)
                    .HasColumnName("longtitude");

                entity.Property(e => e.PostCode)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("postCode");

                entity.Property(e => e.Prefecture)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("prefecture");

                entity.Property(e => e.Region)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("region");

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("street");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("updateDate")
                    .HasDefaultValueSql("current_timestamp()");
            });

            modelBuilder.Entity<Package>(entity =>
            {
                entity.ToTable("package");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Discount)
                    .HasPrecision(10)
                    .HasColumnName("discount");

                entity.Property(e => e.InsertDate)
                    .HasColumnType("timestamp")
                    .HasColumnName("insertDate")
                    .HasDefaultValueSql("current_timestamp()");

                entity.Property(e => e.MaxRequests)
                    .HasColumnType("int(11)")
                    .HasColumnName("maxRequests");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Price)
                    .HasPrecision(10)
                    .HasColumnName("price");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("updateDate")
                    .HasDefaultValueSql("current_timestamp()");
            });

            modelBuilder.Entity<Request>(entity =>
            {
                entity.ToTable("request");

                entity.HasIndex(e => e.CultivationId, "cultivationId");

                entity.HasIndex(e => e.FarmerId, "farmerId");

                entity.HasIndex(e => e.LocationId, "locationId");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.CultivationId)
                    .HasColumnType("int(11)")
                    .HasColumnName("cultivationId");

                entity.Property(e => e.EstimatedDuration)
                    .HasColumnType("int(11)")
                    .HasColumnName("estimatedDuration");

                entity.Property(e => e.FarmerId)
                    .HasColumnType("int(11)")
                    .HasColumnName("farmerId");

                entity.Property(e => e.FoodAmount)
                    .HasPrecision(10)
                    .HasColumnName("foodAmount");

                entity.Property(e => e.InsertDate)
                    .HasColumnType("timestamp")
                    .HasColumnName("insertDate")
                    .HasDefaultValueSql("current_timestamp()");

                entity.Property(e => e.JobType)
                    .HasMaxLength(255)
                    .HasColumnName("jobType");

                entity.Property(e => e.LocationId)
                    .HasColumnType("int(11)")
                    .HasColumnName("locationId");

                entity.Property(e => e.Price)
                    .HasPrecision(10)
                    .HasColumnName("price");

                entity.Property(e => e.StartJobDate).HasColumnName("startJobDate");

                entity.Property(e => e.StayAmount)
                    .HasPrecision(10)
                    .HasColumnName("stayAmount");

                entity.Property(e => e.TravelAmount)
                    .HasPrecision(10)
                    .HasColumnName("travelAmount");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("updateDate")
                    .HasDefaultValueSql("current_timestamp()");

                entity.HasOne(d => d.Cultivation)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.CultivationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("request_ibfk_2");

                entity.HasOne(d => d.Farmer)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.FarmerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("request_ibfk_3");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("request_ibfk_1");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("roles");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.InsertDate)
                    .HasColumnType("timestamp")
                    .HasColumnName("insertDate")
                    .HasDefaultValueSql("current_timestamp()");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("updateDate")
                    .HasDefaultValueSql("current_timestamp()");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasIndex(e => e.RoleId, "roleId");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.EmailConformed).HasColumnName("emailConformed");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("firstName");

                entity.Property(e => e.InsertDate)
                    .HasColumnType("timestamp")
                    .HasColumnName("insertDate")
                    .HasDefaultValueSql("current_timestamp()");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.LastLoginDate)
                    .HasColumnType("datetime")
                    .HasColumnName("lastLoginDate");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("lastName");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("password");

                entity.Property(e => e.RoleId)
                    .HasColumnType("int(11)")
                    .HasColumnName("roleId");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("updateDate")
                    .HasDefaultValueSql("current_timestamp()");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_ibfk_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
