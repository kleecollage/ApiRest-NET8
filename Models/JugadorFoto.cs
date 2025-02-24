namespace ApiRest.Models;
public class JugadorFoto
{
  public int Id { get; set; }

  public string Nombre { get; set; }

  public int JugadorId { get; set; }

  public virtual Jugador Jugador { get; set; }
}