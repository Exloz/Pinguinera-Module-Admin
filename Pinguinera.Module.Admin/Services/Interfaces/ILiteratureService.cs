namespace pinguinera_final_module.Services.Interfaces;

public interface ILiteratureService
{
    Task<List<LiteratureDTOToUi>> GetItemLiteratures();
}