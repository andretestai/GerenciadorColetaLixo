using APIColetaDeLixo.Interfaces.Repository;
using APIColetaDeLixo.Interfaces.Services;
using APIColetaDeLixo.Models;

namespace APIColetaDeLixo.Services
{
    public class ColetaService : IColetaService
    {
        private readonly IColetaRepository _repository;

        public ColetaService(IColetaRepository repository)
        {
            _repository = repository;
        }
        public async Task<ColetaModel> AddColeta(ColetaModel coleta)
        {
            return await _repository.AddColeta(coleta);
        }

        public async Task<bool> DeleteColeta(int id)
        {
            return await _repository.DeleteColeta(id);
        }

        public async Task<List<ColetaModel>> GetAllColetas()
        {
            return await _repository.GetAllColetas();
        }

        public async Task<ColetaModel> GetColeta(int id)
        {
            return await _repository.GetColeta(id);
        }

        public async Task<List<ColetaModel>> GetColetaByUsuario(int id)
        {
            return await _repository.GetColetaByUsuario(id);
        }

        public async Task<ColetaModel> UpdateColeta(ColetaModel coleta)
        {
            return await _repository.UpdateColeta(coleta);
        }
    }
}
