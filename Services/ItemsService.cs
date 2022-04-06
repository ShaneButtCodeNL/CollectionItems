using CollectionItems.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace CollectionItems.Services;

public class ItemsService{
   private readonly  IMongoCollection<Item> _itemsCollection;

   public ItemsService(IOptions<DatabaseSettings> itemsDatabaseSettings){
      var client=new MongoClient(itemsDatabaseSettings.Value.MongoDBConnectionString);
      var db=client.GetDatabase(itemsDatabaseSettings.Value.MongoDBName);
      _itemsCollection=db.GetCollection<Item>(itemsDatabaseSettings.Value.MongoDBItemCollection);
   }

   //Http get // Gets all items
   public async Task<List<Item>> GetAllItems()=> await _itemsCollection.Find(_=>true).ToListAsync();
   //Http get type // gets all items of a type
   public async Task<List<Item>> GetItemsByType(string type){
      var list=await _itemsCollection.Find(item=>item.Type.ToLower()==type.ToLower()).ToListAsync();
      return list;
   }
   public async Task<List<Item>> GetItemsWithNameContains(String name){
      var list=await _itemsCollection.Find(_=>true).ToListAsync();
      for(var i=list.Count()-1;i>=0;i--){
         if(!checkName(list[i],name))list.RemoveAt(i);
      }
      return list;
    }

   

   public async Task<Item?> GetItem(string id){
      Console.WriteLine("INHERE");
      Console.WriteLine(id);

      return await _itemsCollection.Find(i=>i.Id==id).FirstOrDefaultAsync();
   }
   
   public async Task CreateAsync(Item newItem)=>await _itemsCollection.InsertOneAsync(newItem);
   
   public async Task UpdateAsync(string id,Item updatedItem)=> await _itemsCollection.ReplaceOneAsync(x=>x.Id==id,updatedItem);

   public async Task RemoveAsync(string id)=> await _itemsCollection.DeleteOneAsync(x=>x.Id==id);

   private static bool checkName(Item item,string name){
      string? s= item.Details;
      ItemDetails? idet=JsonSerializer.Deserialize<ItemDetails>(s);
      return idet.Name.ToLower().Contains(name.ToLower());
   }
   
}