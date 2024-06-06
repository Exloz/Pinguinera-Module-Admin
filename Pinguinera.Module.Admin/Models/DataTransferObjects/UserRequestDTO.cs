using FluentValidation;
using pinguinera_final_module.Shared.Enums;

namespace pinguinera_final_module.Models.DataTransferObjects;

public class UserRequestDTO {
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public RoleType Role { get; set; }
}

public class UserRequestDTOValidator : AbstractValidator<UserRequestDTO> {
    public UserRequestDTOValidator() {
        RuleFor(x => x.Username).NotEmpty().Length(3, 50);
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty().Length(6, 50);
        RuleFor(x => x.Role).NotEmpty();
    }
}