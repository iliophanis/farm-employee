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
            .RuleFor(b => b.User, f => f.PickRandom(farmersUsers))
            .RuleFor(b => b.AvgRate, f => f.Random.Decimal(0, 5))
            .RuleFor(b => b.AvgWorkPlaceRate, f => f.Random.Decimal(0, 5))
            .RuleFor(b => b.AvgPaymentConsequenceRate, f => f.Random.Decimal(0, 5));
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
            .RuleFor(b => b.User, f => f.PickRandom(employeeUsers))
            .RuleFor(b => b.AvgRate, f => f.Random.Decimal(0, 5))
            .RuleFor(b => b.AvgPrice, f => f.Random.Decimal(0, 5))
            .RuleFor(b => b.AvgJobQuality, f => f.Random.Decimal(0, 5))
            .RuleFor(b => b.AvgContactQuality, f => f.Random.Decimal(0, 5));
        employees = employeesFaker.Generate(count);
        _context.Employees.AddRange(employees);
    }

    private void SeedCultivations()
    {
        cultivations = _context.Cultivations.ToList();
        if (cultivations.Count() > 0) return;

        var dataJson = File.ReadAllText(@"Data\DataSeed\cultivations.json");
        cultivations = JsonConvert.DeserializeObject<List<Cultivation>>(dataJson);
        _context.Cultivations.AddRange(cultivations);
    }
    private record BoundingBox(string City, string Region, string Prefecture, string PostCode, double MaxLon, double MinLon, double MaxLat, double MinLat);
    private void SeedLocations(int count = 1000)
    {

        locations = _context.Locations.ToList();
        if (locations.Count() > 0) return;

        var boundingBoxList = new List<BoundingBox>()
        {
            new BoundingBox("Λάρισα","Περιφέρεια Θεσσαλίας","Δήμος Λαρισαίων","42222", 22.5760706, 22.2560706, 39.7983092, 39.4783092),
            new BoundingBox("Αθήνα","Περιφέρεια Αττικής","10431","Δήμος Αθηναίων", 23.8883052, 23.5683052, 38.1439412, 37.8239412),
            new BoundingBox("Κρήτη","Περιφέρεια Κρήτης","71409", "Δήμος Κρήτης",26.3189698, 23.5144812, 35.6957793, 34.9212109),
            new BoundingBox("Δράμα","Περιφέρεια Ανατολικής Μακεδονίας και Θράκης","Δήμος Δράμας","66100", 24.3068286, 23.9868286, 41.3099443,  40.9899443),
            new BoundingBox("Θεσσαλονίκη","Περιφέρεια Κεντρικής Μακεδονίας","54626","Δήμος Θεσσαλονίκης", 23.0952716, 22.7752716, 40.8003167,  40.4803167),
            new BoundingBox("Αγρίνιο","Περιφέρεια Δυτικής Ελλάδας","30100","Δήμος Αγρινίου", 21.5694206, 21.2494206, 38.7848275,  38.4648275),
        };

        var dataCount = (int)(count / boundingBoxList.Count());

        foreach (var b in boundingBoxList)
        {
            var locationFaker = new Faker<Location>()
                        .RuleFor(b => b.Longitude, f => Convert.ToDecimal(f.Address.Longitude(b.MinLon, b.MaxLon)))
                        .RuleFor(b => b.Latitude, f => Convert.ToDecimal(f.Address.Latitude(b.MinLat, b.MaxLat)))
                        .RuleFor(b => b.City, f => b.City)
                        .RuleFor(b => b.Country, f => "Ελλάς")
                        .RuleFor(b => b.Region, f => b.Region)
                        .RuleFor(b => b.PostCode, f => b.PostCode)
                        .RuleFor(b => b.Street, f => f.Address.StreetName())
                        .RuleFor(b => b.Prefecture, f => b.Prefecture);
            var items = locationFaker.Generate(dataCount);
            locations = locations.Union(items).ToList();
        }

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
            .RuleFor(b => b.StartJobDate, f => f.Date.Soon())
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
