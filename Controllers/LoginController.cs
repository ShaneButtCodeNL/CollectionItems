using CollectionItems.Models;
using CollectionItems.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using MongoDB.Bson;
using System.Text.Json;


namespace CollectionItems.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController:ControllerBase{
   private readonly LoginService _loginService;
   private readonly IConfiguration _config;

   public LoginController(LoginService loginService,IConfiguration config){
      _loginService=loginService;
      _config=config;
   }

   [HttpPost]
   public async Task<IActionResult> GetLogin([FromBody] LoginCredentials lc){
      Console.WriteLine("\n\n"+lc.Username+" "+lc.Password+"\n\n");
      var login=await AuthenticateLogin(lc);
      if(login==null)return NotFound("User Not Found.");
      await _loginService.UpdateLogin(login.Id);
      var token = Generate(login);
      return Ok(token);
   }

   private async Task<Login?> AuthenticateLogin(LoginCredentials loginCredentials){
      var currentLogin = await _loginService.LoginAsync(loginCredentials.Username,loginCredentials.Password);
      if(currentLogin == null) return null;
      return currentLogin;
   }

   private string Generate(Login login){
      var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("JWT").GetValue<string>("key")));
      var credentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);

      var claims= new[]
      {
         new Claim(ClaimTypes.NameIdentifier,login.Name),
      };

      var token = new JwtSecurityToken(
         _config.GetSection("JWT").GetValue<string>("Issuer"),
         _config.GetSection("JWT").GetValue<string>("Audience"),
         claims,
         expires:DateTime.Now.AddMinutes(30),
         signingCredentials:credentials
      );

      return new JwtSecurityTokenHandler().WriteToken(token);
   }
} 