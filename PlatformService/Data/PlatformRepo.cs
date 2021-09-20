using System;
using System.Collections.Generic;
using System.Linq;
using PlatformService.Models;

namespace PlatformService.Data
{
  public class PlatformRepo : IPlatformRepo
  {
    private readonly AppDbContext _context;
    public PlatformRepo(AppDbContext context)
    {
        _context = context;
    }
    public int CreatePlatform(Platform plat)
    {
      if(plat is null)
      {
        throw new ArgumentNullException(nameof(plat));
      }

      var createdPlat = _context.Platforms.Add(plat);
      return createdPlat.Entity.Id;
    }

    public IEnumerable<Platform> GetAll()
    {
      return _context.Platforms.ToList();
    }

    public Platform GetById(int id)
    {
      return _context.Platforms.FirstOrDefault(p => p.Id.Equals(id));
    }

    public bool SaveChanges()
    {
      return (_context.SaveChanges() >= 0);
    }
  }
}