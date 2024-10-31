using APIColetaDeLixo.Models;

namespace APIColetaDeLixo.Interfaces.Services
{
    public interface IUsuarioService
    {
        Task<UsuarioModel> AddUsuario(UsuarioModel usuario);
        Task<List<UsuarioModel>> GetAllUsuarios();
        Task<UsuarioModel> GetUsuario(int id);
        Task<UsuarioModel> UpdateUsuario(UsuarioModel usuario);
        Task<bool> DeleteUsuario(int id);

    }
}
