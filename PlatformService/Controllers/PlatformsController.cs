using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.SyncDataServices.Http;

namespace PlatformService.Controllers
{
  [ApiController]
  [Route("api/platforms")]
  public class PlatformsController : ControllerBase
  {
    public readonly IPlatformRepo _repo;
    public readonly IMapper _mapper;
    private readonly ICommandDataClient _commandDataClient;
    public PlatformsController(IPlatformRepo repo, IMapper mapper, ICommandDataClient commandDataClient)
    {
      _commandDataClient = commandDataClient;
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
    public async Task<ActionResult<PlatformReadDto>> Create(PlatformCreateDto dto)
    {
      Platform platform = _mapper.Map<Platform>(dto);
      _repo.CreatePlatform(platform);
      _repo.SaveChanges();

      PlatformReadDto readDto = _mapper.Map<PlatformReadDto>(platform);
      try
      {
        await _commandDataClient.SendPlatformToCommand(readDto);
      } 
      catch(Exception ex)
      {
        Console.WriteLine($"--> Could not send synchronusly [{ex.Message}]");
      }

      return CreatedAtRoute(nameof(GetById), new
      {
        id = readDto.Id
      }, readDto);
    }
  }
}