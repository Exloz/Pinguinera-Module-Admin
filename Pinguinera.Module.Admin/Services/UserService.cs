using Microsoft.EntityFrameworkCore;
using pinguinera_final_module.Database.Interfaces;
using pinguinera_final_module.Models.DataTransferObjects;
using pinguinera_final_module.Models.Persistence;
using pinguinera_final_module.Services.Interfaces;
using pinguinera_final_module.Services.Mapper;

namespace pinguinera_final_module.Services;

public class UserService : IUserService {

    private readonly IDatabase _userRepository;

    public UserService(IDatabase userRepository) {
        _userRepository = userRepository;
    }

    public async Task<List<UserResponseDTO>> GetAllUser() {
        var users = await _userRepository.Users.ToListAsync();
        return users.Select((u => UserMapper.MapToUserResponseDTO(u))).ToList();
    }

    public async Task<UserResponseDTO> GetUserById(Guid id) {
        var user = await _userRepository.Users.FirstOrDefaultAsync(u => u.UserId == id);

        if (user == null) throw new AggregateException("User not found");

        return UserMapper.MapToUserResponseDTO(user);
    }

    public async Task DeleteUserById(Guid id) {
        var user = await _userRepository.Users.FirstOrDefaultAsync(u => u.UserId == id);
        Console.WriteLine(user);

        if (user == null) throw new AggregateException("User not found");

        _userRepository.Users.Remove(user);
        await _userRepository.SaveChangesAsync();

    }

    public async Task<UserResponseDTO> AddUser(UserRequestDTO userRequest) {
        var user = new User() {
            UserId = Guid.NewGuid(),
            Username = userRequest.Username,
            Email = userRequest.Email,
            Password = userRequest.Password,
            RegisterAt = DateOnly.FromDateTime(DateTime.Today),
            Role = userRequest.Role
        };

        await _userRepository.Users.AddAsync(user);
        await _userRepository.SaveChangesAsync();

        return UserMapper.MapToUserResponseDTO(user);
    }

    public async Task<UserResponseDTO> UpdateUser(Guid id, UserRequestDTO userRequest) {

        var user = await _userRepository.Users.FirstOrDefaultAsync(u => u.UserId == id);

        if (user == null) throw new AggregateException("User not found");

        user.Username = userRequest.Username;
        user.Email = userRequest.Email;
        user.Password = userRequest.Password;
        user.Role = userRequest.Role;

        await _userRepository.SaveChangesAsync();

        return UserMapper.MapToUserResponseDTO(user);

    }
}


