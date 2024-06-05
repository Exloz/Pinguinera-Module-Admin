using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pinguinera_final_module.Models.Repositories;

namespace pinguinera_final_module.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase {

        private readonly IUserService _userService;

        public UserController(IUserService userService) {
            _userService = userService;
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers() {
            var users = await _userService.GetAllUser();
            return StatusCode(StatusCodes.Status200OK, users);
        }
    }
}
