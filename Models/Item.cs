using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json;

namespace CollectionItems.Models;

[BsonIgnoreExtraElements]
public class Item{
   
   [BsonId]
   [BsonRepresentation(BsonType.ObjectId)]
   public string? Id{get;set;}

   [BsonElement("type")]
   public string? Type{get;set;}

   [BsonElement("imgPath")]
   public string? ImgPath{get;set;}

   [BsonElement("details")]
   private ItemDetails? MyDetails{
      get;set;
   }
   [BsonIgnore]
   public string? Details{
      get=>BsonExtensionMethods.ToJson(MyDetails);//JsonSerializer.Serialize(MyDetails);
      set=>MyDetails=JsonSerializer.Deserialize<ItemDetails>(value);
   }
   
}