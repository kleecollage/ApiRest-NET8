using ApiRest.Data;
using ApiRest.Models;
using ApiRest.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ApiRest.Controllers;

[ApiController]
[Route("[controller]")]
public class PlayerPhotoController(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment): Controller
{
  private IWebHostEnvironment _hostingEnvironment = hostingEnvironment;
  private readonly PlayerPhotoRepository _playerPhotoRepository = new(context);
  private readonly PlayerRepository _playerRepository = new(context);
  private readonly GlobalVariablesRepository _globalVariablesRepository = new(context);


  [HttpGet]
  [Route("/api/player-photo/{id}")]
  public JugadorFoto GetMethod(int Id)
  {
    return _playerPhotoRepository.GetById(Id);
  }

}
