using pinguinera_final_module.Shared.Enums;

namespace pinguinera_final_module.Models.DataTransferObjects;

public class UserRequestDTO {

    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public RoleType Role { get; set; }
}