using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json;



namespace CollectionItems.Models;

[BsonIgnoreExtraElements]

public class LoginCredentials{
   [BsonElement("Name")]

   public string? Username {get;set;}=null;
   [BsonElement("lastAccess")]

   public string? Password {get;set;}=null;
}