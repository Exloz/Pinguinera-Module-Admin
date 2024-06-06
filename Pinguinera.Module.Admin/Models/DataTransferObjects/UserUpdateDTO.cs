using FluentValidation;
using pinguinera_final_module.Shared.Enums;

namespace pinguinera_final_module.Models.DataTransferObjects;

public class UserUpdateDTO {
    public string Username { get; set; }
    public string Email { get; set; }
    public RoleType Role { get; set; }
}

public class UserUpdateDTOValidator : AbstractValidator<UserUpdateDTO> {
    public UserUpdateDTOValidator() {
        RuleFor(x => x.Username).NotEmpty().Length(3, 50);
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Role).NotEmpty();
    }
}