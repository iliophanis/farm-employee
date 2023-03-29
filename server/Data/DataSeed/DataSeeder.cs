using Newtonsoft.Json;
using server.Data.Entities;

namespace server.Data.DataSeed;
public class DataSeeder
{
    private readonly DataContext _context;
    public DataSeeder(DataContext context)
    {
        _context = context;
    }

    private void SeedTable<T>(DbContext context, DbSet<T> dbSet, string dataFilePath, bool clearFirst = false) where T : class
    {
        if (clearFirst)
        {
            dbSet.RemoveRange(dbSet);
            context.SaveChanges();
        }

        var countData = dbSet.Count();
        if (countData == 0)
        {
            var dataJson = File.ReadAllText(dataFilePath);
            var dataList = JsonConvert.DeserializeObject<List<T>>(dataJson);
            dbSet.AddRange(dataList!);
            context.SaveChanges();
            countData = dbSet.Count();
            Console.WriteLine($"[{this.GetType().Name}] {(countData > 0 ? "OK: " : "ERROR: ")} inserted {countData} rows in table {typeof(T)}");
        }
        else
        {
            Console.WriteLine($"[{this.GetType().Name}] {typeof(T)} entries count = {countData}");
        }
    }

    public void Seed()
    {
        // HAF structure data
        // SeedTable<Cultivation>(_context, _context.Cultivations, @"DataSeed\cultivations.json");
    }
}
