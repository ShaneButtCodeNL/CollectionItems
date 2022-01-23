using System.Text.Json.Serialization;
using System.Text.Json;

using MongoDB.Bson.Serialization.Attributes;

namespace CollectionItems.Models;
public class ItemDetails{
   //Used in all Items
   [BsonElement("name")]
   [JsonPropertyName("name")]
   public string? Name{get;set;}

   [BsonElement("condition")]
   [JsonPropertyName("condition")]

   public string? Condition{get;set;}
   //End of global
   
   //Manga
   [BsonIgnoreIfNull]
   [JsonIgnore(Condition =JsonIgnoreCondition.WhenWritingNull)]
   [BsonElement("volume")]
   [JsonPropertyName("volume")]

   public int? Volume{get;set;}
   
   //VideoGames
   [BsonIgnoreIfNull]
   [JsonIgnore(Condition =JsonIgnoreCondition.WhenWritingNull)]
   [BsonElement("sealed")]
   [JsonPropertyName("sealed")]

   public bool? Sealed{get;set;}
   
   //
   [BsonIgnoreIfNull]
   [JsonIgnore(Condition =JsonIgnoreCondition.WhenWritingNull)]
   [BsonElement("hasCase")]
   [JsonPropertyName("hasCase")]

   public bool? HasCase{get;set;}
   
   //
   [BsonIgnoreIfNull]
   [JsonIgnore(Condition =JsonIgnoreCondition.WhenWritingNull)]
   [BsonElement("platform")]
   [JsonPropertyName("platform")]

   public string? Platform{get;set;}
   
   //Videogames Anime
   [BsonIgnoreIfNull]
   [JsonIgnore(Condition =JsonIgnoreCondition.WhenWritingNull)]
   [BsonElement("genres")]
   [JsonPropertyName("genres")]

   public List<string?>? Genres{get;set;}

   //
   [BsonIgnoreIfNull]
   [JsonIgnore(Condition =JsonIgnoreCondition.WhenWritingNull)]
   [BsonElement("publisher")]
   [JsonPropertyName("publisher")]

   public string? Publisher{get;set;}
   //
   [BsonIgnoreIfNull]
   [JsonIgnore(Condition =JsonIgnoreCondition.WhenWritingNull)]
   [BsonElement("author")]
   [JsonPropertyName("author")]

   public string? Author{get;set;}

   //
   [BsonIgnoreIfNull]
   [JsonIgnore(Condition =JsonIgnoreCondition.WhenWritingNull)]
   [BsonElement("releaseDate")]
   [JsonPropertyName("releaseDate")]

   public string? ReleaseDate{get;set;}

   //
   [BsonIgnoreIfNull]
   [JsonIgnore(Condition =JsonIgnoreCondition.WhenWritingNull)]
   [BsonElement("mediaType")]
   [JsonPropertyName("mediaType")]

   public string? MediaType{get;set;}

   //
   [BsonIgnoreIfNull]
   [JsonIgnore(Condition =JsonIgnoreCondition.WhenWritingNull)]
   [BsonElement("limitedEdition")]
   [JsonPropertyName("limitedEdition")]

   public bool? LimitedEdition{get;set;}

   //
   [BsonIgnoreIfNull]
   [JsonIgnore(Condition =JsonIgnoreCondition.WhenWritingNull)]
   [BsonElement("type")]
   [JsonPropertyName("type")]

   public string? Type{get;set;}

   //
   [BsonIgnoreIfNull]
   [JsonIgnore(Condition =JsonIgnoreCondition.WhenWritingNull)]
   [BsonElement("from")]
   [JsonPropertyName("from")]

   public string? From{get;set;}

   //
   [BsonIgnoreIfNull]
   [JsonIgnore(Condition =JsonIgnoreCondition.WhenWritingNull)]
   [BsonElement("ageRestricted")]
   [JsonPropertyName("ageRestricted")]

   public bool? AgeRestricted{get;set;}

   [BsonIgnore]
   [JsonIgnore]
   public string? toString{
      get{
         string s="{";
         if(Name!=null)s+=$"\n\"name\":\"{Name}\", ";
         if(Condition!=null)s+=$"\n\"condition\":\"{Condition}\", ";
         if(Volume!=null)s+=$"\n\"volume\":\"{Volume}\", ";
         if(Sealed!=null)s+=$"\n\"sealed\":\"{Sealed}\", ";
         if(HasCase!=null)s+=$"\n\"hasCase\":\"{HasCase}\", ";
         if(Platform!=null)s+=$"\n\"platform\":\"{Platform}\", ";
         if(Genres!=null)s+=$"\n\"genres\":\"{GenresToString(Genres)}\", ";
         if(Publisher!=null)s+=$"\n\"publisher\":\"{Publisher}\", ";
         if(Author!=null)s+=$"\n\"author\":\"{Author}\", ";
         if(ReleaseDate!=null)s+=$"\n\"releaseDate\":\"{ReleaseDate}\", ";
         if(MediaType!=null)s+=$"\n\"mediaType\":\"{MediaType}\", ";
         if(LimitedEdition!=null)s+=$"\n\"limitedEdition\":\"{LimitedEdition}\", ";
         if(Type!=null)s+=$"\n\"type\":\"{Type}\", ";
         if(From!=null)s+=$"\n\"from\":\"{From}\", ";
         if(AgeRestricted!=null)s+=$"\n\"ageRestricted\":\"{AgeRestricted}\", ";
         return s+"\n}";
      }
   }

   private static string? GenresToString(List<string?> list){
      string s="[";
      for(var i=0;i<list.Count();i++){
         if(i==list.Count()-1)s+=list[i];
         else s+=$"{list[i]},";
      }
      return $"{s}]";
   }
   
}