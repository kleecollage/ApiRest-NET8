using ApiRest.Data;
using ApiRest.Dto;
using ApiRest.Helpers;
using ApiRest.Models;
using ApiRest.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiRest.Controllers;

[ApiController]
[Route("[controller]")]
public class AccessController(ApplicationDbContext context): Controller
{
  private readonly UserRepository _userRepository = new(context);
  private readonly GlobalVariablesRepository _globalVariablesRepository = new(context);


  [HttpPost]
  [AllowAnonymous]
  [Route("/api/access/register")]
  public async Task<IResult> RegisterMethod(RegisterDto dto)
  {
    Usuario userExist = await _userRepository.GetUserByEmail(dto.Email);
    if (userExist != null ) {
      return Results.BadRequest( new {
        Message = "This email does not have an account"
      });
    }

    string code = Utils.GenerateToken();
    Usuario user = new() {
      Nombre = dto.Name,
      Correo = dto.Email,
      Password = Utils.CreatePassword(dto.Password),
      Token = code,
      Estado = 0
    };
    _userRepository.Add(user);
    // Send verification mail
    string url = $"{_globalVariablesRepository.GetById(1).Valor}api/access/verify/{code}";
    Utils.SendEmail(dto.Email, "Verify your email", "<h1>Verify your email</h1> Hi. Please click on the link bellow so" +
      $"to access<br /><a href='{url}>Click Here!</a> or you can copy and paste this link into your favorite browser: {url}");

    return Results.Ok(new {
      Status = "OK",
      Message = "Verification email sent"
    });
  }

}