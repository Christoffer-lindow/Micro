using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PlatformService.Models;

namespace PlatformService.Data
{
  public static class Seeder
  {
    public static void Seed(IApplicationBuilder app)
    {
      using (var sc = app.ApplicationServices.CreateScope())
      {
        SeedData(sc.ServiceProvider.GetService<AppDbContext>());
      }
    }
    private static void SeedData(AppDbContext context)
    {
      if (!context.Platforms.Any())
      {
        Console.WriteLine("--> Seeding Data...");
        context.Platforms.AddRange(
          new Platform() { Name = "Dot Net", Publisher = "Microsoft", Cost = "Free" },
          new Platform() { Name = "PostgreSql", Publisher = "Postgres", Cost = "Free" },
          new Platform() { Name = "Kubernetes", Publisher = "Cloud Native Computing Foundation", Cost = "Free" }
        );
        context.SaveChanges();
      }
      else
      {
        Console.WriteLine("--> Data already present in Database Context");
      }
    }
  }
}