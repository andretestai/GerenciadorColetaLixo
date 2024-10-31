using APIColetaDeLixo.Interfaces.Repository;
using APIColetaDeLixo.Interfaces.Services;
using APIColetaDeLixo.Models;

namespace APIColetaDeLixo.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;
        public UsuarioService(IUsuarioRepository repo)
        {
            _repository = repo;
        }

        public async Task<UsuarioModel> AddUsuario(UsuarioModel usuario)
        {
            return await _repository.AddUsuario(usuario);
        }

        public async Task<bool> DeleteUsuario(int id)
        {
            return await _repository.DeleteUsuario(id);
        }

        public async Task<List<UsuarioModel>> GetAllUsuarios()
        {
            return await _repository.GetAllUsuarios();
        }

        public async Task<UsuarioModel> GetUsuario(int id)
        {
            return await _repository.GetUsuario(id);
        }

        public async Task<UsuarioModel> UpdateUsuario(UsuarioModel usuario)
        {
            return await _repository.UpdateUsuario(usuario);
        }
    }
}
