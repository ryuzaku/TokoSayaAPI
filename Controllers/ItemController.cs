using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TokoSayaAPI.Dto;
using TokoSayaAPI.Interfaces;
using TokoSayaAPI.Models;

namespace TokoSayaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemRepository _itemRepo;

        public ItemController(IItemRepository itemRepo)
        {
            _itemRepo = itemRepo;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ItemDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetItems()
        {
            var items = _itemRepo.GetItems();
            var itemsMap = new List<ItemDto>();

            foreach (var item in items)
            {
                var itemToAdd = new ItemDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    Price = item.Price,
                };
                itemsMap.Add(itemToAdd);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(new { message = "Sukses Mengambil Data", data = itemsMap });
        }

        [HttpGet("{itemId}")]
        [ProducesResponseType(200, Type = typeof(ItemDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetItem(int itemId)
        {
            if (!_itemRepo.IsItemExist(itemId))
                return NotFound("Item Tidak Ada");

            var item = _itemRepo.GetItem(itemId);
            var itemMap = new ItemDto()
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price
            };

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(new {message = "Sukses Mengambil Data", data = itemMap});
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult CreateItem([FromBody] ItemDto itemCreate)
        {
            if (itemCreate == null)
                return BadRequest(ModelState);

            var item = _itemRepo.GetItems()
                .Where(x => x.Name.ToLower() == itemCreate.Name.ToLower())
                .FirstOrDefault();

            if (item != null)
            {
                ModelState.AddModelError("", "Item sudah terdaftar");
                return StatusCode(422, ModelState);
            }

            var itemMap = new Item()
            {
                Id = itemCreate.Id,
                Name = itemCreate.Name,
                Price = itemCreate.Price
            };

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_itemRepo.CreateItem(itemMap))
            {
                ModelState.AddModelError("", "Ada Kesalahan Saat Menyimpan");
                return StatusCode(500, ModelState);
            }
            return Ok(new {message = "Sukses Membuat", Data = itemMap});
        }

        [HttpPut("{itemId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateItem(int itemId, [FromBody] ItemDto itemUpdate)
        {
            if (itemUpdate == null || itemUpdate.Id != itemId)
                return BadRequest(ModelState);

            if (!_itemRepo.IsItemExist(itemUpdate.Id))
                return NotFound("Item tidak ada");

            var itemMap = new Item()
            {
                Id = itemUpdate.Id,
                Name = itemUpdate.Name,
                Price = itemUpdate.Price
            };

            if (!_itemRepo.UpdateItem(itemMap))
            {
                ModelState.AddModelError("", "Ada Kesalahan Saat Menyimpan");
                return StatusCode(500, ModelState);
            }
            return Ok(new {message = "Berhasil Mengubah", data = itemMap});
        }

        [HttpDelete("{itemId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult DeleteItem(int itemId)
        {
            if (_itemRepo.IsItemExist(itemId))
                return NotFound("Item tidak ada");

            var item = _itemRepo.GetItem(itemId);

            if (!_itemRepo.DeleteItem(itemId))
            {
                ModelState.AddModelError("", "Ada Kesalahan Saat Menghapus");
                return StatusCode(500, ModelState);
            }

            return Ok(new { message = "Berhasil Menghapus", data = item });
        }
    }
}
