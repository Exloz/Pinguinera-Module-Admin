using cotizaciones.pinguinera.project.Models.DTOs;

namespace cotizaciones.pinguinera.project.Services.Interfaces;

public interface ILiteratureService
{
    Task<List<LiteratureDTOToUi>> GetItemLiteratures();
}