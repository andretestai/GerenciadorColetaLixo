using APIColetaDeLixo.Models;

namespace APIColetaDeLixo.Interfaces.Services
{
    public interface IColetaService
    {
        Task<ColetaModel> AddColeta(ColetaModel coleta);
        Task<List<ColetaModel>> GetAllColetas();
        Task<ColetaModel> GetColeta(int id); 
        Task<ColetaModel> UpdateColeta(ColetaModel coleta);
        Task<bool> DeleteColeta(int id); 
        Task<List<ColetaModel>> GetColetaByUsuario(int id);
    }
}
