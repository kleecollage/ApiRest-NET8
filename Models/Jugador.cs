namespace ApiRest.Models;
public class Jugador
{
  public int Id { get; set; }

  public string Nombre { get; set; }

  public string Slug { get; set; }

  public string Posicon { get; set; }

  public DateTime Fecha { get; set; }

  public int EquipoId { get; set; }

  public virtual Equipo Equipo { get; set; }
}