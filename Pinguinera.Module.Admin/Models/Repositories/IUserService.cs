using pinguinera_final_module.Models.Persistence;

namespace pinguinera_final_module.Models.Repositories;

public interface IUserService {
    Task<List<User>> GetAllUser();
}