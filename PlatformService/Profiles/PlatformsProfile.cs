using AutoMapper;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Profiles
{
  public class PlatformsProfile : Profile
  {
    public PlatformsProfile()
    {
      CreateMap<PlatformCreateDto,Platform>();
      CreateMap<Platform,PlatformReadDto>();
    }
  }
}