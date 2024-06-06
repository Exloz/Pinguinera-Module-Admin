using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using pinguinera_final_module.Models.DataTransferObjects;
using pinguinera_final_module.Services.Interfaces;

namespace pinguinera_final_module.Controllers {
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase {

        private readonly IUserService _userService;
        private readonly IValidator<UserRequestDTO> _validatorUserRequest;
        private readonly IValidator<UserUpdateDTO> _validatorUserUpdate;

        public UserController(IUserService userService, IValidator<UserRequestDTO> validatorUserRequest,
            IValidator<UserUpdateDTO> validatorUserUpdate) {
            _userService = userService;
            _validatorUserRequest = validatorUserRequest;
            _validatorUserUpdate = validatorUserUpdate;
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
            var validate = await _validatorUserRequest.ValidateAsync(userRequest);
            if (!validate.IsValid) return StatusCode(StatusCodes.Status400BadRequest, validate.Errors);

            try {
                var user = await _userService.AddUser(userRequest);
                return StatusCode(StatusCodes.Status200OK, user);
            }
            catch (Exception e) {
                return StatusCode(StatusCodes.Status400BadRequest, validate.Errors);
            }
        }

        [HttpPut("UpdateUser/{id}")]
        public async Task<ActionResult> UpdateUser([FromBody] UserUpdateDTO userUpdateDto, Guid id) {
            var validate = await _validatorUserUpdate.ValidateAsync(userUpdateDto);
            if (!validate.IsValid) return StatusCode(StatusCodes.Status400BadRequest, validate.Errors);

            try {
                var user = await _userService.UpdateUser(id, userUpdateDto);
                return StatusCode(StatusCodes.Status200OK, user);
            }
            catch (Exception e) {
                return StatusCode(StatusCodes.Status400BadRequest, validate.Errors);
            }
        }
    }
}
