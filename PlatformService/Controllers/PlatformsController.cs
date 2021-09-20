using System.Collections;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Controllers
{
  [ApiController]
  [Route("api/platforms")]
  public class PlatformsController : ControllerBase
  {
    public readonly IPlatformRepo _repo;
    public readonly IMapper _mapper;
    public PlatformsController(IPlatformRepo repo, IMapper mapper)
    {
      _repo = repo;
      _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<PlatformReadDto>> GetAll()
    {
      IEnumerable<Platform> platforms = _repo.GetAll();
      return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platforms));
    }

    [HttpGet("{id}", Name = "GetById")]
    public ActionResult<PlatformReadDto> GetById(int id)
    {
      Platform platform = _repo.GetById(id);
      if (platform is not null)
      {
        return Ok(_mapper.Map<PlatformReadDto>(platform));
      }
      return NotFound();
    }

    [HttpPost]
    public ActionResult<PlatformReadDto> Create(PlatformCreateDto dto)
    {
      Platform platform = _mapper.Map<Platform>(dto);
      _repo.CreatePlatform(platform);
      _repo.SaveChanges();

      PlatformReadDto readDto = _mapper.Map<PlatformReadDto>(platform);
      return CreatedAtRoute(nameof(GetById), new
      {
        id = readDto.Id
      }, readDto);
    }
  }
}