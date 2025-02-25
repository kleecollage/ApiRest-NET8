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
  private readonly IWebHostEnvironment _hostingEnvironment = hostingEnvironment;
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
      photos.Add(new PlayerPhotoDto(photo.Id, $"{_globalVariablesRepository.GetById(1).Valor}uploads/players/{photo.Nombre}"));
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
    // Create Record
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

  [HttpDelete]
  [Route("/api/players-photos/{id}")]
  public GenericResponseDto DeleteMethod(int Id)
  {
    var data = _playerPhotoRepository.GetById(Id);
    var photo = data.Nombre;
    _playerPhotoRepository.Delete(Id); // Delete record from Db
     // Delete image from system
     string mainPath = _hostingEnvironment.WebRootPath;
     var imagePath = Path.Combine(mainPath, @"uploads/players/" + photo);
     if (System.IO.File.Exists(imagePath)) System.IO.File.Delete(imagePath);

    return new GenericResponseDto {
      Status = "OK",
      Message = "Photo removed successfully"
    };
  }

}
