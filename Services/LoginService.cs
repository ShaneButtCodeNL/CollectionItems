using CollectionItems.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Text.Json;
using System.Text.Json.Serialization;


 namespace CollectionItems.Services;
public class LoginService{
   private readonly IMongoCollection<Login> _logins;

   public LoginService(IOptions<DatabaseSettings> itemDatabaseSettings){
      var client = new MongoClient(itemDatabaseSettings.Value.MongoDBConnectionString);
      var db = client.GetDatabase(itemDatabaseSettings.Value.MongoDBName);
      _logins = db.GetCollection<Login>(itemDatabaseSettings.Value.MongoDBLoginsName);
   }

   public async Task UpdateLogin(string? loginId){
      if(loginId is null)return;
      var login =await  _logins.Find(x=>x.Id==loginId).FirstOrDefaultAsync();
      if(login ==null)return;
      login.LastAccess=DateTime.Now;
      await _logins.ReplaceOneAsync(x=>x.Id==loginId,login);
   }

   public async Task<Login?> LoginAsync(string? Username,string? Password){
      if(Username is null || Password is null)return null;
      var login = await  _logins.Find<Login>(item=>item.Name==Username && item.Password==Password).FirstOrDefaultAsync();
      return login;
   }
}