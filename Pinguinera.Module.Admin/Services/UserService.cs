using Microsoft.EntityFrameworkCore;
using pinguinera_final_module.Database.Interfaces;
using pinguinera_final_module.Models.Persistence;
using pinguinera_final_module.Models.Repositories;

namespace pinguinera_final_module.Services;

public class UserService : IUserService {

    private readonly IDatabase _userRepository;

    public UserService(IDatabase userRepository) {
        _userRepository = userRepository;
    }

    public async Task<List<User>> GetAllUser() {
        return await _userRepository.Users.ToListAsync();
    }
}