using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TokoSayaAPI.Dto;
using TokoSayaAPI.Interfaces;
using TokoSayaAPI.Models;

namespace TokoSayaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepo;

        public UserController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetUsers()
        {
            var users = _userRepo.GetUsers();
            var usersMap = new List<UserDto>();
            foreach (var user in users)
            {
                var userToAdd = new UserDto()
                {
                    Id = user.Id,
                    Username = user.Username,
                    Password = user.Password
                };
                usersMap.Add(userToAdd);
            }

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(new {message = "Sukses Mengambil Data", Data = usersMap});
        }

        [HttpGet("{userId")]
        [ProducesResponseType(200, Type = typeof(UserDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetUser(int userId)
        {
            if (!_userRepo.IsUserExist(userId))
                return NotFound("User Tidak Ada");

            var user = _userRepo.GetUser(userId);
            var userMap = new UserDto()
            {
                Id = user.Id,
                Username = user.Username,
                Password = user.Password
            };

            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            return Ok(new {message = "Sukses Mengambil Data", Data = userMap});
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult CreateUser([FromQuery] UserDto userCreate, [FromQuery] CashierDto cashierCreate)
        {
            if (userCreate == null || cashierCreate == null)
                return BadRequest(ModelState);

            var user = _userRepo.GetUsers()
                .Where(x => x.Username == userCreate.Username)
                .FirstOrDefault();

            if (user != null)
            {
                ModelState.AddModelError("", "Username Sudah Terpakai");
                return StatusCode(422, ModelState);
            }

            var userMap = new User()
            {
                Id = userCreate.Id,
                Username = userCreate.Username,
                Password = userCreate.Password,
                Cashier =
                {
                    Id = cashierCreate.Id,
                    Name = cashierCreate.Name
                }
            };

            if(!ModelState.IsValid) 
                return BadRequest(ModelState);

            if (!_userRepo.CreateUser(userMap))
            {
                ModelState.AddModelError("", "Ada Kesalahan Saat Menyimpan");
                return StatusCode(500, ModelState);
            }

            return Ok(new {message = "Sukses Membuat", data = userMap});
        }

        [HttpPut("{userId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUser(int userId, [FromQuery] UserDto updatedUser, [FromQuery] CashierDto updatedCashier)
        {
            if (updatedUser == null || userId != updatedUser.Id)
                return BadRequest(ModelState);

            if (!_userRepo.IsUserExist(userId))
                return NotFound("User Tidak Ada");

            var userMap = new User()
            {
                Id = userId,
                Username = updatedUser.Username,
                Password = updatedUser.Password,
                Cashier =
                {
                    Id = updatedCashier.Id,
                    Name = updatedCashier.Name
                }
            };

            if (!_userRepo.UpdateUser(userMap))
            {
                ModelState.AddModelError("", "Ada Kesalahan Saat Menyimpan");
                return StatusCode(500, ModelState);
            }

            return Ok(new {message = "Berhasil Mengubah", Data = userMap});
        }

        [HttpDelete("{userId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult DeleteUser(int userId)
        {
            if (!_userRepo.IsUserExist(userId))
                return NotFound("User Tidak Ada");

            var user = _userRepo.GetUser(userId);

            if (!_userRepo.DeleteUser(userId))
            {
                ModelState.AddModelError("", "Ada Kesalahan Saat Menghapus");
                return StatusCode(500, ModelState);
            }

            return Ok(new {message = "Berhasil Menghapus", data = user});
        }
    }
}
