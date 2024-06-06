using Microsoft.EntityFrameworkCore;
using pinguinera_final_module.Database.Interfaces;
using pinguinera_final_module.Models.DataTransferObjects;
using pinguinera_final_module.Models.Persistence;
using pinguinera_final_module.Services.Helpers;
using pinguinera_final_module.Services.Interfaces;
using pinguinera_final_module.Services.Mapper;
using pinguinera_final_module.Shared.Enums;

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

    public async Task<Supplier> GetSupplierById(Guid id) {
        var supplier = await _userRepository.Suppliers.FirstOrDefaultAsync(u => u.SupplierId == id);

        if (supplier == null) throw new AggregateException("User not found");

        return supplier;
    }

    public async Task DeleteUserById(Guid id) {
        var user = await _userRepository.Users.FirstOrDefaultAsync(u => u.UserId == id);
        Console.WriteLine(user);

        if (user == null) throw new AggregateException("User not found");

        _userRepository.Users.Remove(user);
        await _userRepository.SaveChangesAsync();

    }

    public async Task<UserResponseDTO> AddUser(UserRequestDTO userRequest) {

        var userCheck = await _userRepository.Users.FirstOrDefaultAsync(u => u.Email == userRequest.Email);
        if (userCheck != null) throw new AggregateException("Email already exists");

        byte[] salt = Hash.GenerateSalt();
        var hashedPassword = Hash.GenerateHash(userRequest.Password, salt);
        var base64Salt = Convert.ToBase64String(salt);
        byte[] retrievedSalt = Convert.FromBase64String(base64Salt);

        var user = new User {
            UserId = Guid.NewGuid(),
            Username = userRequest.Username,
            Email = userRequest.Email,
            Password = hashedPassword,
            Salt = retrievedSalt,
            RegisterAt = DateOnly.FromDateTime(DateTime.Today),
            Role = userRequest.Role
        };

        var supplier = new Supplier();

        if (userRequest.Role == RoleType.SUPPLIER) {
            supplier.SupplierId = user.UserId;
            await _userRepository.Suppliers.AddAsync(supplier);
        }

        await _userRepository.Users.AddAsync(user);
        await _userRepository.SaveChangesAsync();

        return UserMapper.MapToUserResponseDTO(user);
    }

    public async Task<UserResponseDTO> UpdateUser(Guid id, UserUpdateDTO userUpdate) {

        var user = await _userRepository.Users.FirstOrDefaultAsync(u => u.UserId == id);

        if (user == null) throw new AggregateException("User not found");

        user.Username = userUpdate.Username;
        user.Email = userUpdate.Email;
        user.Role = userUpdate.Role;

        await _userRepository.SaveChangesAsync();

        return UserMapper.MapToUserResponseDTO(user);

    }
}


