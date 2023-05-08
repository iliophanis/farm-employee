using Bogus;
using server.Data.Entities;

namespace server.Data.DataSeed;
public class DataSeeder
{
    private readonly DataContext _context;
    private static List<User> users;
    private static List<Role> roles;
    private static List<ContactInfo> contactInfos;

    public DataSeeder(DataContext context)
    {
        _context = context;
    }

    public void SeedContactInfo(int count = 10000)
    {
        contactInfos = _context.ContactInfos.ToList();
        if (contactInfos.Count() > 0) return;
        var contactInfoFaker = new Faker<ContactInfo>()
            .RuleFor(b => b.Address, f => f.Person.Address.Street)
            .RuleFor(b => b.City, f => f.Person.Address.City)
            .RuleFor(b => b.Tk, f => f.Person.Address.ZipCode)
            .RuleFor(b => b.PhoneNo, f => f.Person.Phone)
            .RuleFor(b => b.MobilePhoneNo, f => f.Person.Phone);
        contactInfos = contactInfoFaker.Generate(count);
        _context.ContactInfos.AddRange(contactInfos);
    }
    private void SeedRoles()
    {
        roles = _context.Roles.ToList();
        if (roles.Count() > 0) return;
        roles = new List<Role>()
        {
          new Role()
          {
            Name = "Farmer",
            Description = "Αγρότης"
          },
         new Role()
          {
            Name = "Employee",
            Description = "Υπάλληλος"
          },
        new Role()
          {
            Name = "Admin",
            Description = "Διαχειριστής"
          }
        };
        _context.Roles.AddRange(roles);
    }
    private void SeedUsers(int count = 10000)
    {
        users = _context.Users.Include(x => x.Role).ToList();
        if (users.Count() > 0) return;
        var userFaker = new Faker<User>()
            .RuleFor(b => b.Email, f => f.Person.Email)
            .RuleFor(b => b.Password, "")
            .RuleFor(b => b.LastLoginDate, f => f.Date.Past())
            .RuleFor(b => b.IsActive, f => f.Random.Bool())
            .RuleFor(b => b.FirstName, f => f.Person.FirstName)
            .RuleFor(b => b.LastName, f => f.Person.LastName)
            .RuleFor(b => b.AuthProvider, f => f.PickRandom<AuthProvider>())
            .RuleFor(b => b.Role, f => f.PickRandom(roles));
        users = userFaker.Generate(count);
        _context.Users.AddRange(users);
    }

    public void SeedFarmers(int count = 5000)
    {
        var farmers = _context.Farmers.ToList();
        if (farmers.Count() > 0) return;
        var farmersUsers = users.Where(x => x.Role.Name == "Farmer").ToList();
        var farmersFaker = new Faker<Farmer>()
            .RuleFor(b => b.ContactInfo, f => f.PickRandom(contactInfos))
            .RuleFor(b => b.User, f => f.PickRandom(farmersUsers));
        farmers = farmersFaker.Generate(count);
        _context.Farmers.AddRange(farmers);
    }

    public void SeedEmployees(int count = 5000)
    {
        var employees = _context.Employees.ToList();
        if (employees.Count() > 0) return;
        var employeeUsers = users.Where(x => x.Role.Name == "Employee").ToList();
        var employeesFaker = new Faker<Employee>()
            .RuleFor(b => b.ContactInfo, f => f.PickRandom(contactInfos))
            .RuleFor(b => b.User, f => f.PickRandom(employeeUsers));
        employees = employeesFaker.Generate(count);
        _context.Employees.AddRange(employees);
    }

    public void Seed()
    {
        SeedRoles();
        SeedContactInfo();
        SeedUsers();
        SeedFarmers();
        SeedEmployees();
        _context.SaveChanges();
    }
}
