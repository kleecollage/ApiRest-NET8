using ApiRest.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiRest.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
  public DbSet<Equipo> Equipos { get; set; }
  public DbSet<Jugador> Jugadores { get; set; }
  public DbSet<JugadorFoto> JugadorFotos { get; set; }
  public DbSet<VariableGlobal> VariablesGlobales { get; set; }
  public DbSet<Usuario> Usuarios { get; set; }
}