using pinguinera_final_module.Models.DataTransferObjects;
using pinguinera_final_module.Models.Persistence;

namespace pinguinera_final_module.Services.Interfaces;

public interface IUserService
{
    Task<List<UserResponseDTO>> GetAllUser();
    Task<UserResponseDTO> GetUserById(Guid id);
    Task DeleteUserById(Guid id);
    Task<UserResponseDTO> AddUser(UserRequestDTO userRequest);
    Task<UserResponseDTO> UpdateUser(Guid id, UserRequestDTO userRequest);
    Task<Supplier> GetSupplierById(Guid id);
}