using LabProject.Data;
using LabProject.Models;

namespace LabProject.Helpers
{
    public static class Seeder
    {
    public static void SeedData(SchoolDbContext context)
{
    // Mevcut verileri silmeden sadece eksikse ekle
    var existingCount = context.Classes.Count();
    var toAdd = 100 - existingCount;

    if (toAdd > 0)
    {
        var classes = new List<Class>();
        for (int i = 1; i <= toAdd; i++)
        {
            classes.Add(new Class
            {
                Name = $"Demo Class {existingCount + i}",
                Description = $"Auto-generated class #{existingCount + i}",
                StudentCount = 10 + (i % 30),
                IsActive = true
            });
        }

        context.Classes.AddRange(classes);
        context.SaveChanges();
    }
}

    }
}
