using ApiRest.Data;
using ApiRest.Models;
using ApiRest.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ApiRest.Controllers;

[ApiController]
[Route("[controller]")]
public class PlayerController(ApplicationDbContext context): Controller
{
  private readonly PlayerRepository _playerRepository = new(context);
  private readonly TeamRepository _teamRepository = new(context);

  [HttpGet]
  [Route("/api/players")]
  public IEnumerable<Jugador> GetMethod()
  {
    var players = _playerRepository.GetAll();
    return players;
  }
}