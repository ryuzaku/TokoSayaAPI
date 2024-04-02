using Microsoft.AspNetCore.Mvc;
using TokoSayaAPI.Interfaces;

namespace TokoSayaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokoController : Controller
    {
        private readonly ITokoRepository _tokoRepo;

        public TokoController(ITokoRepository tokoRepo)
        {
            _tokoRepo = tokoRepo;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult GetToko()
        {
            var toko = _tokoRepo.GetToko();

            return Ok(new {message = "Data Berhasil Diterima!", data = toko});
        }
    }
}
