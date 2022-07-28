using CollectionItems.Models;
using CollectionItems.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Microsoft.AspNetCore.Authorization;

namespace CollectionItems.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemController :ControllerBase{
   private readonly ItemsService _itemService;

   public ItemController(ItemsService itemsService)=>_itemService=itemsService;

   //Http get: Gets all entries
   [HttpGet]
   public async Task<List<Item>> Get()=>await _itemService.GetAllItems();
   [HttpGet("{id}")]
   public async Task<Item> Get(string id)=>await _itemService.GetItem(id);
   [HttpGet("Anime")]
   public async Task<List<Item>> GetAnime()=>await _itemService.GetItemsByType("anime");
   [HttpGet("Figure")]
   public async Task<List<Item>> GetFigure()=>await _itemService.GetItemsByType("figure");
   [HttpGet("Manga")]
   public async Task<List<Item>> GetManga()=>await _itemService.GetItemsByType("manga");
   [HttpGet("VideoGame")]
   public async Task<List<Item>> GetVideoGame()=>await _itemService.GetItemsByType("videogame");

   [HttpGet("search/{name}")]
   public async Task<List<Item>> GetSearch(string name)=>await _itemService.GetItemsWithNameContains(name);

   [HttpPost]
   [Authorize]
   public async Task<IActionResult> Post(Item newItem){
      newItem.Id=ObjectId.GenerateNewId().ToString();
      await _itemService.CreateAsync(newItem);
      return CreatedAtAction(nameof(Get),new {id=newItem.Id},newItem);
   }

   [HttpPut("{id}")]
   [Authorize]
   public async Task<IActionResult> Put(string id,Item updatedItem){
      var item=await _itemService.GetItem(id);
      if(item ==null)return NotFound();
      updatedItem.Id=item.Id;
      await _itemService.UpdateAsync(id,updatedItem);
      return NoContent();
   }

   [HttpDelete("{id}")]
   [Authorize]
   public async Task<IActionResult> Delete(string id){
      Console.WriteLine(id);
      Console.WriteLine("Before");
      string idd=id;
      var item=await _itemService.GetItem(idd);
      Console.WriteLine("After");
      if(item == null) return NotFound();
      await _itemService.RemoveAsync(id);
      return NoContent();
   }
}