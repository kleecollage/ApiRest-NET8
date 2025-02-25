using ApiRest.Models;

namespace ApiRest.Dto;

public class PlayerDto
{
  public int Id { get; set; }

  public string Name { get; set; }

  public string Position { get; set; }

  public int TeamId { get; set; }

  public virtual Equipo Team { get; set; }
}