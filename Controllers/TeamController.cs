using ApiRest.Data;
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


}