using pinguinera_final_module.Models.DataTransferObjects;
using pinguinera_final_module.Models.Persistence;

namespace pinguinera_final_module.Services.Mapper;

public static class UserMapper {

    public static UserResponseDTO MapToUserResponseDTO(User user) {
        return new UserResponseDTO {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role
        };
    }
}