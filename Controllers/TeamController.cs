using ApiRest.Data;
using ApiRest.Dto;
using ApiRest.Models;
using ApiRest.Repository;
using Microsoft.AspNetCore.Mvc;
using Slugify;

namespace ApiRest.Controllers;

[ApiController]
[Route("[controller]")]
public class TeamController(ApplicationDbContext context): Controller
{
  private readonly ApplicationDbContext _context = context;
  private readonly TeamRepository _teamRepository = new(context);

  [HttpGet]
  [Route("/api/teams")]
  public IEnumerable<Equipo> GetMethod()
  {
    return _teamRepository.GetAll();
  }

  [HttpGet]
  [Route("/api/teams/{id}")]
  public Equipo GetMethod(int id)
  {
    return _teamRepository.GetById(id);
  }

  [HttpPost]
  [Route("/api/teams")]
  public GenericResponseDto PostMethod(TeamDto dto)
  {
    _teamRepository.Add(new Equipo
      {
        Nombre = dto.Name,
        Slug = new SlugHelper().GenerateSlug(dto.Name)
      }
    );

    return new GenericResponseDto {
      Status = "OK",
      Message = $"Team added successfully | {dto}"
    };
  }
}