using CollectionItems.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace CollectionItems.Services;

public class LoginService{
   private readonly IMongoCollection<Login> _logins;

   public LoginService(IOptions<DatabaseSettings> itemsDatabaseSettings){
      var client=new MongoClient(itemsDatabaseSettings.Value.MongoDBConnectionString);
      var db=client.GetDatabase(itemsDatabaseSettings.Value.MongoDBName);
      _logins=db.GetCollection<Login>(itemsDatabaseSettings.Value.MongoDBLogins);
   }

   public async Task<Login?> LoginAsync(string username,string password)=>await _logins.Find(x=>x.Name==username&&x.Password==password).FirstOrDefaultAsync();

   public async void UpdateLogin(string loginId){
      var login=await _logins.Find(x=>x.Id==loginId).FirstOrDefaultAsync();
      if(login==null)return;
      DateTime date=DateTime.Now;
      date=new DateTime(date.Year,date.Month,date.Day,date.Hour,date.Minute,0);
      login.LastAccess=date;
      await _logins.ReplaceOneAsync(x=>x.Id==loginId,login);
   }
}