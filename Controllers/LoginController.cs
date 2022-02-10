using CollectionItems.Models;
using CollectionItems.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Text.Json;


namespace CollectionItems.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController:ControllerBase{
   private readonly LoginService _loginService;

   public LoginController(LoginService loginService)=>_loginService=loginService;

   [HttpPost]
   public async Task<IActionResult> GetLogin([FromBody] LoginCredentials lc){
      Console.WriteLine("\n\n"+lc.Username+" "+lc.Password+"\n\n");
      var login=await _loginService.LoginAsync(lc.Username,lc.Password);
      if(login==null)return NotFound();
      _loginService.UpdateLogin(login.Id);
      return NoContent();
   }
}