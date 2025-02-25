using ApiRest.Data;
using ApiRest.Dto;
using ApiRest.Models;
using ApiRest.Repository;
using Microsoft.AspNetCore.Mvc;
using Slugify;

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
    return _playerRepository.GetAll();
  }

  [HttpGet]
  [Route("/api/players/{id}")]
  public Jugador GetMethod(int Id)
  {
    return _playerRepository.GetById(Id);
  }

  [HttpPost]
  [Route("/api/players")]
  public GenericResponseDto PostMethod(PlayerDto dto)
  {
    if (dto == null) throw new ArgumentNullException(nameof(dto), "Player data cannot be null.");
    if (string.IsNullOrEmpty(dto.Name)) throw new ArgumentException("Name cannot be null or empty.");

    Equipo team = _teamRepository.GetById(dto.TeamId);
    Jugador player = new() {
      Nombre = dto.Name,
      Slug = new SlugHelper().GenerateSlug(dto.Name),
      Posicion = dto.Position,
      Fecha = DateTime.Now,
      Equipo = team,
    };

    _playerRepository.Add(player);

    return new GenericResponseDto {
      Status = "OK",
      Message = "Player added successfully"
    };
  }

  [HttpPut]
  [Route("/api/players/{id}")]
  public GenericResponseDto PutMethod(int Id, PlayerDto dto){
    Equipo team = _teamRepository.GetById(dto.TeamId);
    Jugador update = _playerRepository.GetById(Id);
    update.Nombre = dto.Name;
    update.Slug = new SlugHelper().GenerateSlug(dto.Name);
    update.Posicion = dto.Position;
    update.Equipo = team;

    _playerRepository.Update(update);

    return new GenericResponseDto {
      Status = "OK",
      Message = $"Player updated successfully"
    };
  }

  [HttpDelete]
  [Route("/api/players/{id}")]
  public GenericResponseDto DeleteMethod(int Id)
  {
    _playerRepository.Delete(Id);
    return new GenericResponseDto {
      Status = "OK",
      Message = $"Player removed successfully"
    };
  }


}