using Microsoft.AspNetCore.Mvc;
using TokoSayaAPI.Dto;
using TokoSayaAPI.Interfaces;
using TokoSayaAPI.Models;

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
            var tokoMap = new List<TokoDto>();

            foreach (var t in toko)
            {
                var d = new TokoDto()
                {
                    Id = t.Id,
                    Nama = t.Nama,
                    Alamat = t.Alamat,
                    NoTlp = t.NoTlp
                };
                tokoMap.Add(d);
            }

            return Ok(new {message = "Data Berhasil Diterima!", data = tokoMap});
        }

        [HttpGet("{tokoId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetToko(int tokoId)
        {
            if (!_tokoRepo.TokoExists(tokoId))
                return NotFound();

            var toko = _tokoRepo.GetToko(tokoId);
            var tokoMap = new TokoDto()
            {
                Id = toko.Id,
                Nama = toko.Nama,
                Alamat = toko.Alamat,
                NoTlp = toko.NoTlp
            };


            return Ok(new {message="Data Berhasil Diterima!", data = tokoMap});
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        public IActionResult CreateToko([FromBody] TokoDto tokoCreate)
        {
            if (tokoCreate == null)
                return BadRequest(ModelState);

            var toko = _tokoRepo.GetToko()
                .Where(t => t.Nama.ToLower() == tokoCreate.Nama.ToLower())
                .FirstOrDefault();

            if (toko != null)
            {
                var respons = new ObjectResult(new { message = "Toko sudah Ada!" })
                {
                    StatusCode = 409
                };
                return respons;
            }

            var tokoMap = new Toko()
            {
                Id = tokoCreate.Id,
                Nama = tokoCreate.Nama,
                Alamat = tokoCreate.Alamat,
                NoTlp = tokoCreate.NoTlp
            };

            if (!_tokoRepo.CreateToko(tokoMap))
            {
                ModelState.AddModelError("", "Ada kesalahan saat menyimpan");
                return StatusCode(500, ModelState);
            }

            var response = new ObjectResult(new
            {
                message = "Berhasil membuat toko",
                data_toko = tokoMap
            })
            {
                StatusCode = 201
            };

            return response;
        }

        [HttpPut("{tokoId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult UpdateToko(int tokoId, [FromBody] TokoDto updatedToko)
        {
            if (!_tokoRepo.TokoExists(tokoId))
                return NotFound();

            if (updatedToko == null || tokoId != updatedToko.Id) 
                return BadRequest(ModelState);

            var tokoMap = new Toko()
            {
                Id = updatedToko.Id,
                Nama = updatedToko.Nama,
                Alamat = updatedToko.Alamat,
                NoTlp = updatedToko.NoTlp
            };

            if (!_tokoRepo.UpdateToko(tokoMap))
            {
                ModelState.AddModelError("", "Ada kesalahan saat menyimpan");
                return StatusCode(500, ModelState);
            }

            return Ok("Berhasil Mengubah");
        }

        [HttpDelete("{tokoId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult DeleteToko(int tokoId)
        {
            if (!_tokoRepo.TokoExists(tokoId))
                return NotFound();

            var toko = _tokoRepo.GetToko(tokoId);

            if (!_tokoRepo.DeleteToko(toko))
            {
                ModelState.AddModelError("", "Ada kesalahan saat menghapus");
                return StatusCode(500, ModelState);
            }

            return Ok(new {message = "Berhasil Menghapus", data = toko});
        }
    }
}
