using Bogus;
using Newtonsoft.Json;
using server.Data.Entities;

namespace server.Data.DataSeed;
public class DataSeeder
{
    private readonly DataContext _context;
    private static List<User> users;
    private static List<Role> roles;
    private static List<ContactInfo> contactInfos;
    private static List<Farmer> farmers;
    private static List<Employee> employees;
    private static List<Cultivation> cultivations;
    private static List<Location> locations;
    private static List<Request> requests;
    private static string[] jobTypes = { "Fruit Tree Pruner", "Crop Picker", "Tree Trimmer", "Irrigation Technician", "Agricultural Mechanic" };
    public DataSeeder(DataContext context)
    {
        _context = context;
    }

    private void SeedContactInfo(int count = 10000)
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

    private void SeedFarmers(int count = 5000)
    {
        farmers = _context.Farmers.ToList();
        if (farmers.Count() > 0) return;
        var farmersUsers = users.Where(x => x.Role.Name == "Farmer").ToList();
        var farmersFaker = new Faker<Farmer>()
            .RuleFor(b => b.ContactInfo, f => f.PickRandom(contactInfos))
            .RuleFor(b => b.User, f => f.PickRandom(farmersUsers));
        farmers = farmersFaker.Generate(count);
        _context.Farmers.AddRange(farmers);
    }

    private void SeedEmployees(int count = 5000)
    {
        employees = _context.Employees.ToList();
        if (employees.Count() > 0) return;
        var employeeUsers = users.Where(x => x.Role.Name == "Employee").ToList();
        var employeesFaker = new Faker<Employee>()
            .RuleFor(b => b.ContactInfo, f => f.PickRandom(contactInfos))
            .RuleFor(b => b.User, f => f.PickRandom(employeeUsers));
        employees = employeesFaker.Generate(count);
        _context.Employees.AddRange(employees);
    }

    private void SeedCultivations()
    {
        cultivations = _context.Cultivations.ToList();
        if (cultivations.Count() > 0) return;

        var dataJson = File.ReadAllText(@"Data\DataSeed\cultivations.json");
        cultivations = JsonConvert.DeserializeObject<List<Cultivation>>(dataJson);
        // cultivations.ForEach(cult =>
        // {
        //     cult.InsertDate = DateTime.Now;
        //     cult.UpdateDate = DateTime.Now;
        // });
        _context.Cultivations.AddRange(cultivations);
    }

    private void SeedLocations(int count = 1000)
    {
        locations = _context.Locations.ToList();
        if (locations.Count() > 0) return;

        var locationFaker = new Faker<Location>()
            .RuleFor(b => b.Longtitude, f => Convert.ToDecimal(f.Address.Longitude(19.91975, 28.2225)))
            .RuleFor(b => b.Latitude, f => Convert.ToDecimal(f.Address.Latitude(35.01186, 41.50306)))
            .RuleFor(b => b.City, f => f.Address.City())
            .RuleFor(b => b.Country, f => f.Address.Country())
            .RuleFor(b => b.Region, f => f.Address.City())
            .RuleFor(b => b.PostCode, f => f.Address.ZipCode())
            .RuleFor(b => b.Street, f => f.Address.StreetName())
            .RuleFor(b => b.Prefecture, f => f.Address.City());            

        locations = locationFaker.Generate(count);

        _context.Locations.AddRange(locations);
    }

    private void SeedRequests(int count = 1000)
    {
        requests = _context.Requests.ToList();
        if (requests.Count() > 0) return;
        var requestFaker = new Faker<Request>()
            .RuleFor(b => b.JobType, f => f.PickRandom(jobTypes))
            .RuleFor(b => b.Cultivation, f => f.PickRandom(cultivations))
            .RuleFor(b => b.Location, f => f.PickRandom(locations))
            .RuleFor(b => b.Farmer, f => f.PickRandom(farmers))
            .RuleFor(b => b.StartJobDate, f => f.Date.SoonDateOnly())
            .RuleFor(b => b.EstimatedDuration, f => f.Random.Int(2, 100))
            .RuleFor(b => b.Price, f => f.Random.Decimal(200, 5000))
            .RuleFor(b => b.StayAmount, f => f.Random.Decimal(100, 1000))
            .RuleFor(b => b.TravelAmount, f => f.Random.Decimal(20, 400))
            .RuleFor(b => b.FoodAmount, f => f.Random.Decimal(50, 400));

        requests = requestFaker.Generate(count);

        _context.Requests.AddRange(requests);
    }

    public void Seed()
    {
        SeedRoles();
        SeedContactInfo();
        SeedUsers();
        SeedFarmers();
        SeedEmployees();
        SeedCultivations();
        SeedLocations();
        SeedRequests();
        _context.SaveChanges();
    }
}
