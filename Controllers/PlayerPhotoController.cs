using ApiRest.Data;
using ApiRest.Dto;
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
  [Route("/api/players-photos/{id}")]
  public List<PlayerPhotoDto> GetMethod(int Id)
  {
    List<JugadorFoto> players = _playerPhotoRepository.GetPhotosByPlayer(Id);
    List<PlayerPhotoDto> photos = [];

    foreach(JugadorFoto photo in players)
    {
      photos.Add(new PlayerPhotoDto(photo.Id, photo.Nombre));
    }

    return photos;
  }

  [HttpPost]
  [Route("/api/players-photos/{id}")]
  public GenericResponseDto PostMethod(int Id)
  {
    // File Upload
    string mainPath = _hostingEnvironment.WebRootPath;
    var files = HttpContext.Request.Form.Files;
    string fileName = Guid.NewGuid().ToString();
    var extension = Path.GetExtension(files[0].FileName);
    var uploads = Path.Combine(mainPath, @"uploads/players");
    var filePath = Path.Combine(uploads, fileName + extension);
    var fileStream = new FileStream(filePath, FileMode.Create);
    files[0].CopyTo(fileStream);
    // Create record
    JugadorFoto photo = new() {
      Nombre = fileName + extension,
      Jugador = _playerRepository.GetById(Id)
    };
    _playerPhotoRepository.Add(photo);

    return new GenericResponseDto {
      Status = "OK",
      Message = "Record created successfully"
    };
  }

}
