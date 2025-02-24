using ApiRest.Dto;
using Microsoft.AspNetCore.Mvc;

namespace ApiRest.Controllers;

[ApiController]
[Route("[controller]")]
public class ExampleController(IWebHostEnvironment hostingEnvironment): Controller
{
  private readonly IWebHostEnvironment _hostingEnvironment = hostingEnvironment;

  // ##############################   GET   ############################## //
  [HttpGet]
  [Route("/api/example")]
  public GenericResponseDto GetMethod()
  {
    // Add Header
    HttpContext.Response.Headers.Append("Autor", "www.cesarcancino.com");
    // Get Headers
    Request.Headers.TryGetValue("Authorization", out var headerValue);

    // return Content("GET Method from my api rest");
    return new GenericResponseDto {
      Status = "OK",
      Mesage = $"GET Method | Heder = {headerValue}"
    };
  }

  [HttpGet]
  [Route("/api/example-querystring")]
  public IActionResult GetMethodQuerystring([FromQuery(Name = "id")] string id, [FromQuery(Name = "slug")] string slug)
  {
    return Content($"GET Method | ID = {id} | Slug = {slug}");
  }
  // ##############################   POST   ############################## //
  [HttpPost]
  [Route("/api/example")]
  public GenericResponseDto PostMethod(CategoryDto dto)
  {
    // return Content($"POST method | Name: {dto.Name}");
    return new GenericResponseDto {
      Status = "OK",
      Mesage = $"POST method | Name: {dto.Name}"
    };
  }

  [HttpPost]
  [Route("/api/example-upload")]
  public GenericResponseDto UploadMethod()
  {
    string mainPath = _hostingEnvironment.WebRootPath;
    var files = HttpContext.Request.Form.Files;
    string fileName = Guid.NewGuid().ToString();
    var uploads = Path.Combine(mainPath, @"uploads/example");
    var extension = Path.GetExtension(files[0].FileName);
    var filePath = Path.Combine(uploads, fileName + extension);
    using var fileStream = new FileStream(filePath, FileMode.Create);
    files[0].CopyTo(fileStream);

    return new GenericResponseDto
    {
      Status = "OK",
      Mesage = $"File created successfully | path = {filePath} | files = {files + extension}"
    };
  }

  // ##############################   PUT   ############################## //
  [HttpPut]
  [Route("/api/example/{id}")]
  public IActionResult PutMethod(int Id)
  {
    return Content($"PUT Method | ID = {Id}");
  }

  [HttpDelete]
  [Route("/api/example/{id}")]
  public IActionResult DeleteMethod(int Id)
  {
    return Content($"DELETE method | ID = {Id}");
  }
}