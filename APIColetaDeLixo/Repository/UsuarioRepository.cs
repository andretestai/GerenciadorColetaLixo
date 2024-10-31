using APIColetaDeLixo.DataContext;
using APIColetaDeLixo.Interfaces.Repository;
using APIColetaDeLixo.Models;
using Microsoft.EntityFrameworkCore;

namespace APIColetaDeLixo.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly Context _context;

        public UsuarioRepository(Context context)
        {
            _context = context;
        }
        public async Task<UsuarioModel> AddUsuario(UsuarioModel usuario)
        {
            try
            {
                await _context.Usuario.AddAsync(usuario);
                await _context.SaveChangesAsync(); 
                return usuario; 
            }
            catch (Exception)
            {
                throw new Exception("Erro ao adicionar Usuário");
            }
        }

        public async Task<bool> DeleteUsuario(int id)
        {
            try
            {
                var validar = await _context.Usuario.Where(at => at.Id == id).FirstOrDefaultAsync();

                if(validar != null)
                {
                    return true;
                }
                else
                {
                    throw new Exception("Usuario não encontrado");
                }
            }catch (Exception)
            {
                throw new Exception("Erro ao exlcuir usuario");
            }
        }

        public async Task<List<UsuarioModel>> GetAllUsuarios()
        {
            try
            {
                return await _context.Usuario.ToListAsync();
            }
            catch (Exception)
            {
                throw new Exception("Erro ao encontrar todos usuarios");
            }
        }

        public async Task<UsuarioModel> GetUsuario(int id)
        {
            try
            {
                var validar = await _context.Usuario.Where(at => at.Id == id).FirstOrDefaultAsync();

                if (validar != null)
                {
                    return validar;
                }
                else
                {
                    throw new Exception("Usuario não encontrado");
                }
            }
            catch (Exception)
            {
                throw new Exception("Erro ao exlcuir usuario");
            }
        }

        public async Task<UsuarioModel> UpdateUsuario(UsuarioModel usuario)
        {
            try
            {
                var validar = await _context.Usuario.Where(at => at.Id == usuario.Id).FirstOrDefaultAsync();

                if (validar != null)
                {
                    validar.Nome = usuario.Nome;
                    validar.Email = usuario.Email;
                    validar.Telefone = usuario.Telefone;
                    _context.Update(validar);
                    await _context.SaveChangesAsync();
                    return validar;
                }
                else
                {
                    throw new Exception("Usuario não encontrado");
                }
            }
            catch (Exception)
            {
                throw new Exception("Erro ao exlcuir usuario");
            }
        }
    }
}
