using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TokoSayaAPI.Dto;
using TokoSayaAPI.Interfaces;
using TokoSayaAPI.Models;

namespace TokoSayaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CashierController : ControllerBase
    {
        private readonly ICashierRepository _cashierRepo;
        private readonly IItemRepository _itemRepo;
        private readonly IUserRepository _userRepo;
        private readonly ITransactionRepository _transRepo;

        public CashierController(ICashierRepository cashierRepo, 
            IItemRepository itemRepo, IUserRepository userRepo,
            ITransactionRepository transRepo)
        {
            _cashierRepo = cashierRepo;
            _itemRepo = itemRepo;
            _userRepo = userRepo;
            _transRepo = transRepo;
        }

        [HttpPost("{uId}/transaction")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult MakeTransaction(int uId, [FromQuery] List<int> itemIds)
        {
            
            var user = _userRepo.GetUser(uId);
            var items = new List<Item>();

            foreach (var id in itemIds)
            {
                var item = _itemRepo.GetItem(id);
                items.Add(item);
            }

            var transactionMap = new Transaction()
            {
                CreatedAt = DateTime.Now,
                TotalPrice = _transRepo.TotalItemPrice(items),
                Cashier = user.Cashier,
                TransactionItems = items.Select(x => new TransactionItem
                {
                    Item = x
                }).ToList()
            };

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_cashierRepo.MakeTransaction(transactionMap))
            {
                ModelState.AddModelError("", "Ada Kesalahan Saat Menyimpan");
                return StatusCode(500, ModelState);
            }
            return Ok(new { message = "Sukses Membuat", Data = transactionMap });
        }
    }
}
