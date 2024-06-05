namespace pinguinera_final_module.Models.Persistence;

public partial class User
{
    public Guid UserId { get; set; }

    public string Username { get; set; } = null!;

    public string? RefreshToken { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateOnly RegisterAt { get; set; }

    public virtual Admin? Admin { get; set; }

    public virtual Assistant? Assistant { get; set; }

    public virtual Reader? Reader { get; set; }

    public virtual Supplier? Supplier { get; set; }
}
