using ApiRest.Data;
using ApiRest.Dto;
using ApiRest.Models;
using ApiRest.Repository;
using Microsoft.AspNetCore.Mvc;

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
  [Route("/api/temas")]
  public GenericResponseDto PostMethod(Equipo model)
  {

    return new GenericResponseDto {
      Status = "OK",
      Message = "OK"
    };
  }
}