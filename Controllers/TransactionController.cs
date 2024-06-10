using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TokoSayaAPI.Dto;
using TokoSayaAPI.Interfaces;
using TokoSayaAPI.Models;

namespace TokoSayaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _transRepo;

        public TransactionController(ITransactionRepository transRepo)
        {
            _transRepo = transRepo;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TransactionDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetAllTransactions()
        {
            var transaction = _transRepo.GetAllTransactions();
            var transactionMap = new List<TransactionDto>();

            foreach (var item in transaction)
            {
                var itemToAdd = new TransactionDto()
                {
                    Id = item.Id,
                    CreatedAt = item.CreatedAt,
                    TotalPrice = item.TotalPrice
                };
                transactionMap.Add(itemToAdd);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(new { message = "Sukses Mengambil Data", data = transactionMap });
        }

        [HttpGet("{tId}")]
        [ProducesResponseType(200, Type = typeof(TransactionDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetTransactions(int tId)
        {
            if (!_transRepo.IsTransactionExist(tId))
                return NotFound("Transaksi tidak ada");

            var transaction = _transRepo.GetTransaction(tId);
            var transactionMap = new TransactionDto()
            {
                Id = transaction.Id,
                CreatedAt = transaction.CreatedAt,
                TotalPrice = transaction.TotalPrice
            };

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(new { message = "Sukses Mengambil Data", data = transactionMap });
        }

        [HttpGet("items/{tId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ItemDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetTransactionItem(int tId)
        {
            if (!_transRepo.IsTransactionExist(tId))
                return NotFound("Transaksi tidak ada");

            var items = _transRepo.GetTransactionItems(tId);
            var itemsMap = new List<ItemDto>();

            foreach ( var item in items)
            {
                var itemToAdd = new ItemDto()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Price = item.Price
                };
                itemsMap.Add(itemToAdd);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(new { message = "Sukses Mengambil Data", data = itemsMap });
        }

        [HttpGet("total/{tId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetTotalItemPriceOfTransaction(int tId)
        {
            if (!_transRepo.IsTransactionExist(tId))
                return NotFound("Transaksi tidak ada");

            var transactionItems = _transRepo.GetTransactionItems(tId).ToList();
            var totalItemsPrice = _transRepo.TotalItemPrice(transactionItems);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(totalItemsPrice);
        }
    }
}
