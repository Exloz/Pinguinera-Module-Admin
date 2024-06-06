using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pinguinera_final_module.Models.DataTransferObjects;
using pinguinera_final_module.Services.Interfaces;

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

            try {
                return StatusCode(StatusCodes.Status200OK, users);
            }
            catch (Exception e) {
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
        }

        [HttpGet("GetUserById/{id}")]
        public async Task<IActionResult> GetUserById(Guid id) {
            var user = await _userService.GetUserById(id);

            try {
                return StatusCode(StatusCodes.Status200OK, user);
            }
            catch (Exception e) {
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
        }

        [HttpDelete("DeleteUserById/{id}")]
        public async Task<IActionResult> DeleteUserById(Guid id) {

            try {
                await _userService.DeleteUserById(id);
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception e) {
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] UserRequestDTO userRequest) {
            var user = await _userService.AddUser(userRequest);
            try {
                return StatusCode(StatusCodes.Status200OK, user);
            }
            catch (Exception e) {
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
        }

        [HttpPut("UpdateUser/{id}")]
        public async Task<ActionResult> UpdateUser([FromBody] UserRequestDTO userRequestDto, Guid id) {
            var user = await _userService.UpdateUser(id, userRequestDto);
            try {
                return StatusCode(StatusCodes.Status200OK, user);

            }
            catch (Exception e) {
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            };
        }
    }
}
