using System.Text.Json.Serialization;

namespace CollectionItems.Models;

public class LoginCredentials{
   [JsonPropertyName("username")]
   public string Username{get;set;}

   [JsonPropertyName("password")]
   public string Password{get;set;}
}