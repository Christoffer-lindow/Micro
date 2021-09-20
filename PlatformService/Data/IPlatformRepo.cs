using System.Collections.Generic;
using PlatformService.Models;

namespace PlatformService.Data
{
  public interface IPlatformRepo
  {
    IEnumerable<Platform> GetAll();
    Platform GetById(int id);
    int CreatePlatform(Platform plat);
    bool SaveChanges();

  }
}