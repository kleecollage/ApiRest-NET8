using System.Security.Claims;
using System.Text;
using ApiRest.Data;
using ApiRest.Dto;
using ApiRest.Helpers;
using ApiRest.Models;
using ApiRest.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace ApiRest.Controllers;

[ApiController]
[Route("[controller]")]
public class AccessController(ApplicationDbContext context, IConfiguration configuration): Controller
{
  private readonly UserRepository _userRepository = new(context);
  private readonly GlobalVariablesRepository _globalVariablesRepository = new(context);
  private readonly IConfiguration _configuration = configuration;

  // ##############################   REGISTER   ############################## //
  [HttpPost]
  [AllowAnonymous]
  [Route("/api/access/register")]
  public async Task<IResult> RegisterMethod(RegisterDto dto)
  {
    Usuario userExist = await _userRepository.GetUserByEmail(dto.Email);
    if (userExist != null ) {
      return Results.BadRequest( new {
        Message = "This email already have an account with us"
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
    Utils.SendEmail(dto.Email, "Verify your email", "<h1>Verify your email</h1> Hi. Please click on the link bellow to " +
      $"access:<br/><a href='{url}'>Click Here!</a><br/> Or you can copy and paste the next link into your favorite " +
      $"browser:<br/>{url}");

    return Results.Ok(new {
      Status = "OK",
      Message = "Verification email sent"
    });
  }

  [HttpGet]
  [AllowAnonymous]
  [Route("/api/access/verify/{token}")]
  public async Task<IResult> AccessVerification(string token)
  {
    if (token == null)
    {
      return Results.BadRequest(new {
        Message = "Resource not available"
      });
    }

    Usuario user = await _userRepository.GetUserByToken(token);
    if (user == null)
    {
      return Results.BadRequest(new {
        Message = "Resource not available"
      });
    }

    user.Token = "";
    user.Estado = 1;
    _userRepository.Update(user);

    return Results.Ok(new {
      Status = "OK",
      Message = "Verification Success"
    });
  }
  // ##############################   LOGIN   ############################## //
  [HttpPost]
  [AllowAnonymous]
  [Route("/api/access/login")]
  public async Task<IResult> LoginMethod(LoginDto dto)
  {
    Usuario user = await _userRepository.GetUser(dto.Email, Utils.CreatePassword(dto.Password));
    if (user == null)
    {
      return Results.BadRequest(new {
        Message = "Invalid Credentials"
      });
    }

    var issuer = _configuration["Jwt:Issuer"];
    var audience = _configuration["Jwt:Audience"];
    var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
    // JWT Payload
    var tokenDescriptor = new SecurityTokenDescriptor
    {
      Subject = new ClaimsIdentity(
      [
        new Claim("Id", user.Id.ToString()),
        new Claim(JwtRegisteredClaimNames.Sub, dto.Email),
        new Claim(JwtRegisteredClaimNames.Email, dto.Email),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
      ]),
      Expires = DateTime.UtcNow.AddMinutes(90),
      Issuer = issuer,
      Audience = audience,
      SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
    };
    var tokenHandler = new JwtSecurityTokenHandler();
    var token = tokenHandler.CreateToken(tokenDescriptor);
    var jwtToken = tokenHandler.WriteToken(token);
    /// JWT PAYLOAD ///

    return Results.Ok(new {
      Name = user.Nombre,
      Token = jwtToken
    });
  }
}