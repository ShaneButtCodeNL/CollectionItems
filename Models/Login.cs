using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json;

namespace CollectionItems.Models;

[BsonIgnoreExtraElements]
public class Login{
   [BsonId]
   [BsonRepresentation(BsonType.ObjectId)]
   public string Id{get;set;}="";

   [BsonElement("userName")]
   public string Name {get;set;}="";

   [BsonElement("password")]

   public string Password{get;set;}="";

   [BsonElement("lastAccess")]

   public BsonDateTime? LastAccess{get;set;}=null;

}