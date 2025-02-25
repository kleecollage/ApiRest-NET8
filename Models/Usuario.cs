namespace ApiRest.Models;
public class Usuario
{
  public int Id { get; set; }

  public string Nombre { get; set; }
  public string Correo { get; set; }
  public string Password { get; set; }
  public byte Estado { get; set; }
  public string Token { get; set; }
}