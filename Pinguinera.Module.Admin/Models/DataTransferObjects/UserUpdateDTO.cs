using pinguinera_final_module.Shared.Enums;

namespace pinguinera_final_module.Models.DataTransferObjects;

public class UserUpdateDTO {
    public string Username { get; set; }
    public string Email { get; set; }
    public RoleType Role { get; set; }
}