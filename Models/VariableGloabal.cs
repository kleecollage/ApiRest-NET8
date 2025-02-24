using System.ComponentModel.DataAnnotations;

namespace ApiRest.Models;
public class VariableGlobal
{
  [Key]
  public int Id { get; set; }

  public string Nombre { get; set; }
  public string Valor { get; set; }

}