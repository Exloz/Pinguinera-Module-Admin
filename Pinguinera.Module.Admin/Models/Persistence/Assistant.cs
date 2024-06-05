namespace pinguinera_final_module.Models.Persistence;

public partial class Assistant
{
    public Guid AssistantId { get; set; }

    public virtual User AssistantNavigation { get; set; } = null!;
}
